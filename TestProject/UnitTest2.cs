using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWTAuth.Business.HotelService.Interface;
using JWTAuth.Controllers;
using JWTAuth.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestProject
{
    public class HotelCrud
    {
        private readonly Mock<IHotelService> _mockHotelService;
        private readonly HotelController _mockHotelController;
        public HotelCrud()
        {
            _mockHotelService = new Mock<IHotelService>();
            _mockHotelController = new HotelController(_mockHotelService.Object);
        }


        [Fact]
        public async Task CreateHotel_ReturnsCreatedAtActionResult_WithHotel()
        {
            // Arrange
            var hotel = new Hotel { Id = 1, Name = "Hotel1" };
            _mockHotelService.Setup(service => service.CreateHotelAsync(hotel)).ReturnsAsync(hotel);

            // Act
            var result = await _mockHotelController.CreateHotel(hotel);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Hotel>(createdAtActionResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task UpdateHotel_Return_Result()
        {
            var hotel = new Hotel { Id = 1, Name = "Hotel1" };
            _mockHotelService.Setup(s => s.UpdateHotelAsync(hotel)).ReturnsAsync(hotel);
            var res = await _mockHotelController.UpdateHotel(hotel.Id, hotel);
            Assert.IsType<NoContentResult>(res);
        }

        [Fact]
        public async Task DeleteHotel_ReturnsOkResult()
        {
            // Arrange
            var hotelId = 1;
            _mockHotelService.Setup(service => service.DeleteHotelAsync(hotelId)).ReturnsAsync(true);
            // Act
            var result = await _mockHotelController.DeleteHotel(hotelId);
            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }

}
