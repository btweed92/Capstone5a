using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone5API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capstone5API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarsDbContext _context;
        public CarController(CarsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            var carList = await _context.Car.ToListAsync();
            return carList;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> GetCarId(int id)
        {
            var found = await _context.Car.FindAsync(id);
            if (found == null)
            {
                return NotFound();
            }
            return found;
        }
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Car.Add(car);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCarId), new { id = car.CarId }, car);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Car>> PutCar(int id, Car car)
        {
            if (id != car.CarId)
            {
                return BadRequest();
            }
            _context.Entry(car).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            _context.Car.Remove(car);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}