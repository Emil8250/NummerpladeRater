using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NummerpladeRater.Models;

public class Upvote
{
    [Key]
    public int Id { get; set; }
    public int PlateId { get; set; }
}