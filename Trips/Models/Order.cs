using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trips.Models;

public class OrderE : IEquatable<OrderE>
{
    [DisplayName("Id")]
    [Column("id")]
    [Required]
    public int Id { get; set; }

    [DisplayName("Adults count")]
    [Column("adults_count")]
    [Range(1, 99)]
    [Required]
    public int? AdultsCount { get; set; }

    [DisplayName("Children count")]
    [Column("children_count")]
    [Range(0, 99)]
    [Required]
    public int? ChildrenCount { get; set; }

    [DisplayName("Total price")]
    [Column("final_price")]
    [Range(0.01, int.MaxValue)]
    [Required]
    public decimal? FinalPrice { get; set; }

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

    public bool Equals(OrderE? other)
    {
        if (other == null) return false;

        return Id == other.Id;
    }

    public int GetHashCode(OrderE obj)
    {
        return base.GetHashCode();
    }
}

public class OrderL
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
    public string Status { get; set; }

    [DisplayName("Luggage size")]
    [Column("luggage_size")]
    [Required]
    public string LuggageSize { get; set; }

    [DisplayName("Agent")]
    [Column("fk_agent")]
    [Required]
    public string FkAgent { get; set; }

    [DisplayName("Customer")]
    [Column("fk_customer")]
    [Required]
    public string FkCustomer { get; set; }

    [DisplayName("Trip")]
    [Column("fk_trip")]
    [Required]
    public string FkTripId { get; set; }
}