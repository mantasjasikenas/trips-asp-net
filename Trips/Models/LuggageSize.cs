using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trips.Models;

public class LuggageSize
{
    [DisplayName("Id")]
    [Column("id")]
    [Required]
    public int Id { get; set; }

    [DisplayName("Size")]
    [Column("name")]
    [Required]
    public string Size { get; set; }
}