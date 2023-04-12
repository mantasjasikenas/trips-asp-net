using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trips.Models;

public class OrderStatus
{
    [DisplayName("Id")]
    [Column("id")]
    [Required]
    public int Id { get; set; }

    [DisplayName("Status")]
    [Column("name")]
    [Required]
    public string Status { get; set; }
}