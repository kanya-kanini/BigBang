using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string? CustomerName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public int HotelId { get; set; }


        public Hotel? Hotel { get; set; }


    }
}
