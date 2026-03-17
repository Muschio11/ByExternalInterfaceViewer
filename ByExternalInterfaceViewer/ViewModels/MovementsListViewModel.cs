using ByExternalInterfaceViewer.Models.ExternalinterfaceDBModels;
using ByExternalInterfaceViewer.Services.ExternalInterfaceDB;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace ByExternalInterfaceViewer.ViewModels;

public partial class MovementsListViewModel: ObservableObject
{
   private readonly IDbContextFactory<AppDbContextExternalInterface> _dbContextFactory;
    [ObservableProperty]
    private ObservableCollection<MovementsListModel> _movements = new ();

    /// <summary>
    /// Timer for refresh window
    /// </summary>
    [ObservableProperty]
    private bool _isAutoRefreshEnabled = true; // Temporary default value, can be set via UI later
    [ObservableProperty]
    private bool _isLoading;
    private readonly DispatcherTimer _refreshTimer;

    /// <summary>
    /// Filters
    /// </summary>
    public ObservableCollection<ColumnFilter> ColumnFilters { get; } = new();
    public ICollectionView MovementsView { get;}

 

    public ColumnFilter OperationIdFilter => ColumnFilters.FirstOrDefault(f => f.PropertyName == nameof(MovementsListModel.OperationID));
    public ColumnFilter LocationIdFilter => ColumnFilters.FirstOrDefault(f => f.PropertyName == nameof(MovementsListModel.LocationID));

    public MovementsListViewModel(IDbContextFactory<AppDbContextExternalInterface> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
        MovementsView = CollectionViewSource.GetDefaultView(Movements);
        MovementsView.Filter = ApplyFilters;
        BuildFilters();

        _refreshTimer = new DispatcherTimer();
        _refreshTimer.Interval = TimeSpan.FromMinutes(1);
        _refreshTimer.Tick += async (s, e) => await GetMovementsAsync();
        StartAutoRefreshMovementList();
        

    }

    /// <summary>
    /// Search for movement list
    /// </summary>
    public async Task GetMovementsAsync()
    {
        try
        {
            IsLoading = true;
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            var query = await context.MovementsList
                .Select(m => new MovementsListModel 
                { 
                    OperationID = m.OperationID 
                
                }).OrderByDescending(m => m.OperationID)
                .AsNoTracking().Take(100)
                .ToListAsync();
            Movements.Clear();

            foreach(var item in query)
            {
                Movements.Add(item);
            }

            foreach (var filter in ColumnFilters)
            {
                filter.AvailableValues.Clear();
                var values = Movements
                    .Select(x => typeof(MovementsListModel).GetProperty(filter.PropertyName)?.GetValue(x)?.ToString())
                    .Where(v => v != null)
                    .Distinct()
                    .OrderBy(v => v);

                foreach (var val in values)
                    filter.AvailableValues.Add(val);

                filter.IsAllSelected = true; // Seleziona tutti
            }

            MovementsView.Refresh();

        }
        catch (Exception ex)
        {
           MessageBox.Show($"Error fetching movements: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsLoading = false;
        }
    }

    public void StopAutoRefreshMovementList()
    {
        _refreshTimer.Stop();
    }
     public void StartAutoRefreshMovementList()
    {
        if (IsAutoRefreshEnabled)
        {
            _refreshTimer.Start();
        }
        
    }
    private void BuildFilters()
    {
        ColumnFilters.Clear();
        ColumnFilters.Add(new ColumnFilter { PropertyName = nameof(MovementsListModel.OperationID) });
        ColumnFilters.Add(new ColumnFilter { PropertyName = nameof(MovementsListModel.LocationID) });
    }

    private bool ApplyFilters(object obj)
    {
        if (obj is not MovementsListModel item)
            return false;

        foreach (var filter in ColumnFilters)
        {
            if (!filter.IsActive)
                continue;

            var prop = typeof(MovementsListModel).GetProperty(filter.PropertyName);
            var value = prop?.GetValue(item)?.ToString();

            if (!filter.SelectedValues.Contains(value))
                return false;
        }

        return true;
    }


    // Command per Apply Filter
    public IRelayCommand ApplyFilterCommand => new RelayCommand(() =>
    {
        MovementsView.Refresh();
        
    });
}
