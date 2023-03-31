using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trips.Models;

public class Trip
{
    [DisplayName("Nr")]
    [Column("nr")]
    [Required]
    public int Nr { get; set; }
    
    [DisplayName("Start date")]
    [Column("start_date")]
    [Required]
    public DateTime StartDate { get; set; }
    
    [DisplayName("End date")]
    [Column("end_date")]
    [Required]
    public DateTime EndDate { get; set; }
    
    [DisplayName("Duration")]
    [Column("duration")]
    [Required]
    public int Duration { get; set; }
    
    [DisplayName("Destination")]
    [Column("destination")]
    [Required]
    public string Destination { get; set; }
    
    [DisplayName("Final Price")]
    [Column("final_price")]
    [Required]
    public decimal FinalPrice { get; set; }
    
    [DisplayName("Transport")]
    [Column("transport")]
    [Required]
    public int TransportId { get; set; }
    
    [DisplayName("Type")]
    [Column("type")]
    [Required]
    public int TypeId { get; set; }
    
    [DisplayName("Travel Agency")]
    [Column("fk_travel_agency")]
    [Required]
    public int FkTravelAgencyId { get; set; }
    
    [DisplayName("Hotel Service")]
    [Column("fk_hotel_service")]
    [Required]
    public int FkHotelServiceId { get; set; }
}