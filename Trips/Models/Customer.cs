using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Trips.Models;
public class Customer {

    [DisplayName("Personal code")]
    [Required]
    public int PersonalCode { get; set; }

    [DisplayName("First name")]
    [Required]
    public string FirstName { get; set; }

    [DisplayName("Last name")]
    [Required]
    public string LastName { get; set; }

    [DisplayName("Birth date")]
    [DataType(DataType.Date)]
    //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    [Required]
    public DateTime BirthDate { get; set; }

    [DisplayName("Phone number")]
    [Required]
    public string PhoneNumber { get; set; }

    [DisplayName("Email")]
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [DisplayName("Street")]
    [Required]
    public string Street { get; set; }

    [DisplayName("Town")]
    [Required]
    public string Town { get; set; }

    [DisplayName("Postal code")]
    [Required]
    public string PostalCode { get; set; }

 

}
