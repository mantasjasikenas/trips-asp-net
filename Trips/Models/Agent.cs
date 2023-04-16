using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trips.Models;

public class Agent
{
    [DisplayName("Id")]
    [Column("id")]
    [Required]
    public int Id { get; set; }

    [DisplayName("First name")]
    [Column("first_name")]
    [Required]
    public string FirstName { get; set; }

    [DisplayName("Last name")]
    [Column("last_name")]
    [Required]
    public string LastName { get; set; }

    [DisplayName("Phone number")]
    [Column("phone")]
    [Phone]
    [Required]
    public string Phone { get; set; }

    [DisplayName("Email")]
    [Column("email")]
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [DisplayName("Travel agency")]
    [Column("fk_travel_agency_title")]
    public string? FkTravelAgencyTitle { get; set; }


    [DisplayName("Travel agency")]
    [Column("fk_travel_agency_id")]
    [Required(ErrorMessage = "The Travel agency field is required.")]
    public int? FkTravelAgencyId { get; set; }
}