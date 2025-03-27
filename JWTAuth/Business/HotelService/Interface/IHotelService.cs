using JWTAuth.Models;

namespace JWTAuth.Business.HotelService.Interface
{
    public interface IHotelService
    {
        Task<IEnumerable<Hotel>> GetHotelsAsync();
        Task<Hotel> GetHotelByIdAsync(int id);
        Task<Hotel> CreateHotelAsync(Hotel hotel);
        Task<Hotel> UpdateHotelAsync(Hotel hotel);
        Task<bool> DeleteHotelAsync(int id);
    }
}
