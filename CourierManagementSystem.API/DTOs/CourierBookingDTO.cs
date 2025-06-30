namespace CourierManagementSystem.API.DTOs
{
    public class CourierBookingDTO
    {
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string DeliveryType { get; set; }
        public double WeightKg { get; set; }
        public string PaymentMode { get; set; }
    }
}
