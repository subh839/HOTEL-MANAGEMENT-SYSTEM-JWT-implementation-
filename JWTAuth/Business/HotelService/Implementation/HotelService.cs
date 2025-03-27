using JWTAuth.Business.HotelService.Interface;
using JWTAuth.Models;
using JWTAuth.Business;
using Microsoft.EntityFrameworkCore;

namespace JWTAuth.Business.HotelService.Implementation
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationDbContext _context;

        public HotelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hotel>> GetHotelsAsync()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            return await _context.Hotels.FindAsync(id);
        }

        public async Task<Hotel> CreateHotelAsync(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<Hotel> UpdateHotelAsync(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<bool> DeleteHotelAsync(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null) return false;

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
