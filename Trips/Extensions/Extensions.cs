using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

namespace TripsAgency.Extensions;

public static class Extensions
{
    public static List<T> ToList<T>(this DataTable dataTable) where T : new()
    {
        return (from DataRow row in dataTable.Rows select row.To<T>()).ToList();
    }

    public static T To<T>(this DataRow dataRow)
    {
        var type = typeof(T);
        var obj = Activator.CreateInstance<T>();

        foreach (DataColumn column in dataRow.Table.Columns)
        foreach (var propertyInfo in type.GetProperties())
        {
            var attribute = propertyInfo.GetCustomAttribute<ColumnAttribute>(true);

            if (attribute?.Name != null &&
                attribute.Name.Equals(column.ColumnName, StringComparison.CurrentCultureIgnoreCase))
                propertyInfo.SetValue(obj, dataRow[column.ColumnName], null);
        }

        return obj;
    }
}