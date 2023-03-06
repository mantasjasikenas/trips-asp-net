using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trips.Models;

public class Customer
{
    [DisplayName("Personal code")]
    [Column("personal_id")]
    [Required]
    public int PersonalCode { get; set; }

    [DisplayName("First name")]
    [Column("first_name")]
    [Required]
    public string FirstName { get; set; }

    [DisplayName("Last name")]
    [Column("last_name")]
    [Required]
    public string LastName { get; set; }

    [DisplayName("Birth date")]
    [Column("birth_date")]
    [DataType(DataType.Date)]
    [Required]
    public DateTime BirthDate { get; set; }

    [DisplayName("Phone number")]
    [Column("phone")]
    [Required]
    public string PhoneNumber { get; set; }

    [DisplayName("Email")]
    [Column("email")]
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [DisplayName("Street")]
    [Column("street")]
    [Required]
    public string Street { get; set; }

    [DisplayName("Town")]
    [Column("town")]
    [Required]
    public string Town { get; set; }

    [DisplayName("Postal code")]
    [Column("postcode")]
    [Required]
    public string PostalCode { get; set; }
}