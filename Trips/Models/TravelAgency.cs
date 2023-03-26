using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Trips.Models; 
public class TravelAgency {

    [DisplayName("Id")]
    [Required]
    public int Id { get; set; }

    [DisplayName("Title")]
    [Required]
    public string Title { get; set; }

    [DisplayName("Phone number")]
    [Required]
    public string Phone { get; set; }


    [DisplayName("Manager")]
    [Required]
    public string Manager { get; set; }

    [DisplayName("Street")]
    [Required]
    public string Street { get; set; }

    [DisplayName("Town")]
    [Required]
    public string Town { get; set; }

    [DisplayName("Post code")]
    [Required]
    public string PostCode { get; set; }

    [DisplayName("Country")]
    [Required]
    public string Country { get; set; }

}
