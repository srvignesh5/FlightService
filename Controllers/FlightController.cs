using FlightService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlightService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FlightController : ControllerBase
    {
        private readonly FlightDbContext _context;

        public FlightController(FlightDbContext context)
        {
            _context = context;
        }
   

        // Retrieves all flights 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
            var flights = await _context.Flights.ToListAsync();
            return Ok(flights);
        }

        // Retrieves a specific flight by ID 
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound(new { message = "Flight not found." });
            }
            return Ok(flight);
        }

        // Create a new flight (admin-only)
        [HttpPost]
        public async Task<ActionResult<Flight>> CreateFlight(Flight flight)
        {
            if (!User.IsInRole("Admin"))
                return StatusCode(403, new { message = "You are not authorized to access this resource." });
            if (flight == null)
            {
                return BadRequest(new { message = "Invalid flight data." });
            }
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Flight Created successfully.", flight });
        }


        // Update flight details (Admin only, or user updating AvailableSeats )
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlight(int id, Flight flight)
        {

            var existingFlight = await _context.Flights.FindAsync(id);
            if (existingFlight == null)
            {
                return NotFound(new { message = "Flight not found." });
            }

            if (User.IsInRole("Admin"))
            {
            existingFlight.FlightNumber = flight.FlightNumber;
            existingFlight.Airline = flight.Airline;
            existingFlight.DepartureCity = flight.DepartureCity;
            existingFlight.ArrivalCity = flight.ArrivalCity;
            existingFlight.DepartureTime = flight.DepartureTime;
            existingFlight.ArrivalTime = flight.ArrivalTime;
            existingFlight.AvailableSeats = flight.AvailableSeats;
            existingFlight.TicketPrice = flight.TicketPrice;
            }
            else{
                existingFlight.AvailableSeats = flight.AvailableSeats;
            }
            await _context.SaveChangesAsync();

            return Ok(new { message = "Flight updated successfully." });
        }

        // Delete a flight (admin-only)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            if (!User.IsInRole("Admin"))
                return StatusCode(403, new { message = "You are not authorized to access this resource." });
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound(new { message = "Flight not found." });
            }
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Flight deleted successfully." });
        }
        
    }
}
