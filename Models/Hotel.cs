using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }

        [Required]
        public string? HotelName { get; set; }

        public string? HotelLocation { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        public virtual ICollection<Room>? Rooms { get; set; }
        public virtual ICollection<Staff>? Staffs { get; set; }
        public virtual ICollection<Customer>? Customers { get; set; }
    }
}
