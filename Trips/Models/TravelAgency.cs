using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trips.Models; 
public class TravelAgency {

    [DisplayName("Id")]
    [Column("id")]
    [Required]
    public int Id { get; set; }

    [DisplayName("Title")]
    [Column("title")]
    [Required]
    public string Title { get; set; }

    [DisplayName("Phone number")]
    [Column("phone")]
    [Required]
    public string Phone { get; set; }


    [DisplayName("Manager")]
    [Column("manager")]
    [Required]
    public string Manager { get; set; }

    [DisplayName("Street")]
    [Column("street")]
    [Required]
    public string Street { get; set; }

    [DisplayName("Town")]
    [Column("town")]
    [Required]
    public string Town { get; set; }

    [DisplayName("Post code")]
    [Column("postcode")]
    [Required]
    public string PostCode { get; set; }

    [DisplayName("Country")]
    [Column("country")]
    [Required]
    public string Country { get; set; }

}
