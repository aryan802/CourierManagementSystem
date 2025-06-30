using CourierManagementSystem.API.Data;
using CourierManagementSystem.API.DTOs;
using CourierManagementSystem.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourierManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourierController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CourierController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("book")]
        public async Task<IActionResult> BookCourier(CourierBookingDTO dto)
        {
            var booking = new CourierBooking
            {
                SenderName = dto.SenderName,
                ReceiverName = dto.ReceiverName,
                ReceiverAddress = dto.ReceiverAddress,
                DeliveryType = dto.DeliveryType,
                WeightKg = dto.WeightKg,
                PaymentMode = dto.PaymentMode,
                TrackingNumber = GenerateTrackingNumber()
            };

            _context.CourierBookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Courier booked successfully",
                trackingNumber = booking.TrackingNumber
            });
        }

        private string GenerateTrackingNumber()
        {
            return $"TRK-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }
    }
}
