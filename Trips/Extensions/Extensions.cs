using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

namespace TripsAgency.Extensions;

public static class Extensions
{
    public static List<T> ToList<T>(this DataTable dataTable) where T : new()
    {
        List<T> data = new();

        foreach (DataRow row in dataTable.Rows)
        {
            var item = row.To<T>();
            data.Add(item);
        }

        return data;
    }

    public static T To<T>(this DataRow dataRow)
    {
        var temp = typeof(T);
        var obj = Activator.CreateInstance<T>();

        foreach (DataColumn column in dataRow.Table.Columns)
        foreach (var propertyInfo in temp.GetProperties())
        {
            var attribute = propertyInfo.GetCustomAttribute<ColumnAttribute>(true);

            if (attribute is null) continue;

            if (attribute.Name.Equals(column.ColumnName, StringComparison.CurrentCultureIgnoreCase))
                propertyInfo.SetValue(obj, dataRow[column.ColumnName], null);
        }

        return obj;
    }
}