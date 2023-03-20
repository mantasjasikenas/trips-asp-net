using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Trips.Models;
public class Agent {

    [DisplayName("Id")]
    [Required]
    public int Id { get; set; }

    [DisplayName("First name")]
    [Required]
    public string FirstName { get; set; }

    [DisplayName("Last name")]
    [Required]
    public string LastName { get; set; }

    [DisplayName("Phone number")]
    [Required]
    public string PhoneNumber { get; set; }

    [DisplayName("Email")]
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [DisplayName("Travel agency")]
    [Required]
    public string FkTravelAgency { get; set; }

}
