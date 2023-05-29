using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class Staff
    {
        [Key]
        public int? StaffId { get; set; }

        [Required]
        public string? StaffName { get; set; }

        public string? StaffRole { get; set; }

        public int HotelId { get; set; }

        public Hotel? Hotel { get; set; }


    }
}
