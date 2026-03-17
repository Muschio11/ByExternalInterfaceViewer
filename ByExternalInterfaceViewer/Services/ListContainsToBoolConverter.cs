using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace ByExternalInterfaceViewer.Services;

public class ListContainsToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var list = value as ObservableCollection<string>;
        var item = parameter as string;
        return list?.Contains(item) ?? false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var list = value as ObservableCollection<string>;
        var item = parameter as string;
        if (list == null || item == null) return null;

        bool isChecked = (bool)value;
        if (isChecked && !list.Contains(item))
            list.Add(item);
        else if (!isChecked && list.Contains(item))
            list.Remove(item);

        return list;
    }
}