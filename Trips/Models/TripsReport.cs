using System.ComponentModel.DataAnnotations;

namespace Trips.Models;

public class TripsReport
{
    public IList<TripDetails> Trips { get; set; } = new List<TripDetails>();

    public string? TripType { get; set; }

    public int? MinPassengers { get; set; }

    public int? MinOrders { get; set; }

    public double? MinPrice { get; set; }
    
    [DisplayFormat(DataFormatString = "{0}")]
    public decimal TotalChildren { get; set; }
    
    [DisplayFormat(DataFormatString = "{0}")]
    public decimal TotalAdults { get; set; }
    
    public decimal TotalPrice { get; set; }
    
    [DisplayFormat(DataFormatString = "{0}")]
    public long TotalOrders { get; set; }
}