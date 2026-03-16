using ByExternalInterfaceViewer.Models.Database3DBModels;
using ByExternalInterfaceViewer.Models.ExternalinterfaceDBModels;
using ByExternalInterfaceViewer.Services.Database3DB;
using ByExternalInterfaceViewer.Services.ExternalInterfaceDB;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ByExternalInterfaceViewer.ViewModels;

public partial class MovementsListViewModel: ObservableObject
{
   private readonly IDbContextFactory<AppDbContextExternalInterface> _dbContextFactory;
    [ObservableProperty]
    private ObservableCollection<MovementsListModel> _movements = new ();

    public MovementsListViewModel(IDbContextFactory<AppDbContextExternalInterface> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
        _=GetMovementsAsync();
    }


    public async Task GetMovementsAsync()
    {
        try
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            //var movements = await context.Locations.AsNoTracking().ToListAsync();
            Movements.Clear();

            var query = await context.MovementsList.AsNoTracking().ToListAsync();

            Movements = new ObservableCollection<MovementsListModel>(query);


            


        }
        catch (Exception ex)
        {
           
        }
    }
}
