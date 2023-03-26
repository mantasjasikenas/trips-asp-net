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
            T item = row.To<T>();
            data.Add(item);
        }

        return data;
    }
   
    public static T To<T>(this DataRow dataRow)
    {
        Type temp = typeof(T);
        T obj = Activator.CreateInstance<T>();

        foreach (DataColumn column in dataRow.Table.Columns)
        {
            foreach (PropertyInfo propertyInfo in temp.GetProperties())
            {
                var attribute = propertyInfo.GetCustomAttribute<ColumnAttribute>(true);

                if (attribute is not null)
                {
                    if (attribute.Name.Equals(column.ColumnName, StringComparison.CurrentCultureIgnoreCase))
                        propertyInfo.SetValue(obj, dataRow[column.ColumnName], null);
                }
            }
        }

        return obj;
    }
}
