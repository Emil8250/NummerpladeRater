using System.ComponentModel.DataAnnotations;

namespace NummerpladeRater.Models;

public class LicensePlate
{
    [Key]
    public int Id { get; set; }
    public string Plate { get; set; }
}