using System.ComponentModel.DataAnnotations;

namespace VehiclePark.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public required string LicensePlate { get; set; }
        public required string Color { get; set; }
        public int BuildYear { get; set; }
        public string? LoanedTo { get; set; }
        public StatusOptions Status { get; set; }
        public string? Comments { get; set; }
    }

    public enum StatusOptions
    {
        [Display(Name = "Lent out")]
        Lentout,

        [Display(Name = "Available")]
        Available,

        [Display(Name = "In repair")]
        InRepair,

        [Display(Name = "Sold")]
        Sold,

        [Display(Name = "In order")]
        InOrder
    }
}
