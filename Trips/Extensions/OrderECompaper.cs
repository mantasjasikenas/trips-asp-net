using Trips.Models;

namespace TripsAgency.Extensions;

public class OrderEComparer : IEqualityComparer<OrderE>
{
    public bool Equals(OrderE? x, OrderE? y)
    {
        if (x == null || y == null)
        {
            return false;
        }

        return x.Id == y.Id;
    }

    public int GetHashCode(OrderE obj)
    {
        return obj.Id.GetHashCode();
    }
}

    
