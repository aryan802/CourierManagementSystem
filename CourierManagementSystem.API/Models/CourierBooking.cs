using System.ComponentModel.DataAnnotations;

namespace CourierManagementSystem.API.Models
{
    public class CourierBooking
    {
        public int Id { get; set; }

        [Required]
        public string SenderName { get; set; }

        [Required]
        public string ReceiverName { get; set; }

        [Required]
        public string ReceiverAddress { get; set; }

        [Required]
        public string DeliveryType { get; set; } // Standard, Express

        [Required]
        public double WeightKg { get; set; }

        [Required]
        public string PaymentMode { get; set; } // Cash, Card, Online

        public string TrackingNumber { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    }
}

