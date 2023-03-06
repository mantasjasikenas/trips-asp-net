using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace Trips.Models;

public class OrderE : IEquatable<OrderE>
{
    [DisplayName("Id")]
    [Column("id")]
    [Microsoft.Build.Framework.Required]
    public int Id { get; set; }

    [DisplayName("Adults count")]
    [Column("adults_count")]
    [Range(1, 99)]
    [Microsoft.Build.Framework.Required]
    public int AdultsCount { get; set; }

    [DisplayName("Children count")]
    [Column("children_count")]
    [Range(0, 99)]
    [Microsoft.Build.Framework.Required]
    public int ChildrenCount { get; set; }

    [DisplayName("Total price")]
    [Column("final_price")]
    [Range(0.01, int.MaxValue)]
    [Microsoft.Build.Framework.Required]
    public decimal FinalPrice { get; set; }

    [DisplayName("Status")]
    [Column("status")]
    [Microsoft.Build.Framework.Required]
    public int StatusId { get; set; }

    [DisplayName("Luggage size")]
    [Column("luggage_size")]
    [Microsoft.Build.Framework.Required]
    public int LuggageSizeId { get; set; }

    [DisplayName("Agent")]
    [Column("fk_agent")]
    [Microsoft.Build.Framework.Required]
    public int FkAgentId { get; set; }

    [DisplayName("Customer")]
    [Column("fk_customer")]
    [Microsoft.Build.Framework.Required]
    public int FkCustomerId { get; set; }

    [DisplayName("Trip")]
    [Column("fk_trip")]
    [Microsoft.Build.Framework.Required]
    public int FkTripId { get; set; }

    public int GetHashCode(OrderE obj)
    {
        return base.GetHashCode();
    }

    public bool Equals(OrderE? other)
    {
        if (other == null)
        {
            return false;
        }

        return Id == other.Id;
    }
}

public class OrderL
{
    [DisplayName("Id")]
    [Column("id")]
    [Microsoft.Build.Framework.Required]
    public int Id { get; set; }

    [DisplayName("Adults count")]
    [Column("adults_count")]
    [Microsoft.Build.Framework.Required]
    public int AdultsCount { get; set; }

    [DisplayName("Children count")]
    [Column("children_count")]
    [Microsoft.Build.Framework.Required]
    public int ChildrenCount { get; set; }

    [DisplayName("Total price")]
    [Column("final_price")]
    [Microsoft.Build.Framework.Required]
    public decimal FinalPrice { get; set; }

    [DisplayName("Status")]
    [Column("status")]
    [Microsoft.Build.Framework.Required]
    public string Status { get; set; }

    [DisplayName("Luggage size")]
    [Column("luggage_size")]
    [Microsoft.Build.Framework.Required]
    public string LuggageSize { get; set; }

    [DisplayName("Agent")]
    [Column("fk_agent")]
    [Microsoft.Build.Framework.Required]
    public string FkAgent { get; set; }

    [DisplayName("Customer")]
    [Column("fk_customer")]
    [Microsoft.Build.Framework.Required]
    public string FkCustomer { get; set; }

    [DisplayName("Trip")]
    [Column("fk_trip")]
    [Microsoft.Build.Framework.Required]
    public string FkTripId { get; set; }
}