using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelBookingAPI.Models;
using HotelBookingAPI.Data;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        private readonly ApiContext _context;

        public HotelBookingController(ApiContext context)
        {
            _context = context;
        }

        // Create/Edit
        [HttpPost]
        public JsonResult CreateEdit(HotelBooking booking)
        {
            if(booking.Id == 0)
            {
                _context.Bookings.Add(booking);         // If zero, add
            } else
            {
                var bookingInDb = _context.Bookings.Find(booking.Id);

                if (bookingInDb == null)
                    return new JsonResult(NotFound());  // If not found, return "not found" result

                bookingInDb = booking;                  // If there is a booking, set incoming one
            }

            _context.SaveChanges();

            return new JsonResult(Ok(booking));

        }

        // Get
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Bookings.Find(id);    // Use Find() method

            if (result == null)                         // If not found
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));          // If found, return result.
        }

        // Delete
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Bookings.Find(id);    // Check for Id

            if (result == null)                         // If not found
                return new JsonResult(NotFound());

            _context.Bookings.Remove(result);           // If found Remove(result)
            _context.SaveChanges();

            return new JsonResult(NoContent());         // Json result
        }

        // Get all
        [HttpGet()]
        public JsonResult GetAll()
        {
            var result = _context.Bookings.ToList();    //  

            return new JsonResult(Ok(result));          // Return all booking results
        }
       
    }
}
