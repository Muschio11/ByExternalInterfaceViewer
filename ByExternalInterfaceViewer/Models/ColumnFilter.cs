using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

public class ColumnFilter : ObservableObject
{
    public string PropertyName { get; set; }

    public ObservableCollection<string> AvailableValues { get; set; } = new();
    public ObservableCollection<string> SelectedValues { get; set; } = new();

    private string _searchText = string.Empty;
    public string SearchText
    {
        get => _searchText;
        set
        {
            SetProperty(ref _searchText, value);
            FilteredValues.Refresh();
        }
    }

    private bool _isAllSelected = true;
    public bool IsAllSelected
    {
        get => _isAllSelected;
        set
        {
            if (SetProperty(ref _isAllSelected, value))
            {
                if (_isAllSelected)
                    SelectedValues = new ObservableCollection<string>(AvailableValues);
                else
                    SelectedValues.Clear();
                OnPropertyChanged(nameof(SelectedValues));
            }
        }
    }

    public ICollectionView FilteredValues { get; }

    public ColumnFilter()
    {
        FilteredValues = CollectionViewSource.GetDefaultView(AvailableValues);
        FilteredValues.Filter = FilterPredicate;
    }

    private bool FilterPredicate(object obj)
    {
        if (obj is string s)
            return string.IsNullOrEmpty(SearchText) || s.ToLower().Contains(SearchText.ToLower());
        return false;
    }

    public bool IsActive => SelectedValues.Any();
}