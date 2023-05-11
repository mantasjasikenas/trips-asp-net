using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trips.Models;

public class TripDetails
{
    [DisplayName("Travel agency")]
    [Column("travel_agency")]
    [Required]
    public string TravelAgency { get; set; }

    [DisplayName("Trip")]
    [Column("trip")]
    [Required]
    public string Trip { get; set; }
    
    [DisplayName("Trip type")]
    [Column("trip_type")]
    [Required]
    public string TripType { get; set; }
    
    [DisplayName("Trip duration")]
    [Column("trip_duration")]
    [Required]
    public int TripDuration { get; set; }
    
    [DisplayName("Hotel name")]
    [Column("hotel_name")]
    [Required]
    public string HotelName { get; set; }

    [DisplayName("Orders count")]
    [Column("orders_count")]
    [Required]
    public long OrdersCount { get; set; }
    
    [DisplayName("Total children")]
    [Column("total_children")]
    [DisplayFormat(DataFormatString = "{0}")]
    [Required]
    public decimal TotalChildren { get; set; }

    [DisplayName("Total adults")]
    [Column("total_adults")]
    [DisplayFormat(DataFormatString = "{0}")]
    [Required]
    public decimal TotalAdults { get; set; }

    [DisplayName("Total price")]
    [Column("total_price")]
    [Required]
    public decimal TotalPrice { get; set; }
    
    [DisplayName("Group total orders")]
    [Column("group_total_orders")]
    [Required]
    public long GroupTotalOrders { get; set; }
    
    [DisplayName("Group total children")]
    [Column("group_total_children")]
    [DisplayFormat(DataFormatString = "{0}")]
    [Required]
    public decimal GroupTotalChildren { get; set; }
    
    [DisplayName("Group total adults")]
    [Column("group_total_adults")]
    [DisplayFormat(DataFormatString = "{0}")]
    [Required]
    public decimal GroupTotalAdults { get; set; }

    [DisplayName("Group total price")]
    [Column("group_total_price")]
    [Required]
    public decimal GroupTotalPrice { get; set; }
}