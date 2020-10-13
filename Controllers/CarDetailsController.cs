using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarData.Models;

namespace CarData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarDetailsController : ControllerBase
    {
        private readonly CarContext _context;

        public CarDetailsController(CarContext context)
        {
            _context = context;
        }

        // GET: api/CarDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDetails>>> GetCarDetails()
        {
            return await _context.CarDetails.ToListAsync();
        }

 

        // POST: api/CarDetails
        [HttpPost]
        public async Task<ActionResult<CarDetails>> PostCarDetails(CarDetails carDetails)
        {
            _context.CarDetails.Add(carDetails);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CarDetailsExists(carDetails.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCarDetails", new { id = carDetails.Name }, carDetails);
        }

    }
}
