using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace Trips.Models;

public class Order
{
    [DisplayName("Id")]
    [Column("id")]
    [Required]
    public int Id { get; set; }

    [DisplayName("Adults count")]
    [Column("adults_count")]
    [Required]
    public int AdultsCount { get; set; }

    [DisplayName("Children count")]
    [Column("children_count")]
    [Required]
    public int ChildrenCount { get; set; }

    [DisplayName("Total price")]
    [Column("final_price")]
    [Required]
    public decimal FinalPrice { get; set; }

    [DisplayName("Status")]
    [Column("status")]
    [Required]
    public int StatusId { get; set; }

    [DisplayName("Luggage size")]
    [Column("luggage_size")]
    [Required]
    public int LuggageSizeId { get; set; }

    [DisplayName("Agent")]
    [Column("fk_agent")]
    [Required]
    public int FkAgentId { get; set; }

    [DisplayName("Customer")]
    [Column("fk_customer")]
    [Required]
    public int FkCustomerId { get; set; }

    [DisplayName("Trip")]
    [Column("fk_trip")]
    [Required]
    public int FkTripId { get; set; }
}