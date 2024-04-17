using Microsoft.AspNetCore.Mvc;
using Moq;
using OTP_server.Controllers;
using OTP_server.Models;
using OTP_server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTP_tests
{
    public class OTPGeneratorControllerTest
    {
        private readonly Mock<IOTPGeneratorService> _otpGeneratorMock;
        private readonly OTPGeneratorController _otpController;

        public OTPGeneratorControllerTest()
        {
            _otpGeneratorMock = new Mock<IOTPGeneratorService>();
            _otpController = new OTPGeneratorController(_otpGeneratorMock.Object);
        }

        [Fact]
        public void GenerateOTP_ValidEmailAndMinutesActive_ReturnsOkResult()
        {
            // Arrange
            string email = "test@example.com";
            int minutesActive = 5;
            var otpEntity = new OTPEntity();
            _otpGeneratorMock.Setup(x => x.GenerateOTP(email, minutesActive)).Returns(otpEntity);

            // Act
            var result = _otpController.GenerateOTP(email, minutesActive);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(otpEntity, okResult.Value);
        }

        [Fact]
        public void GenerateOTP_ExceptionThrown_ReturnsBadRequestResult()
        {
            // Arrange
            string email = "test@example.com";
            int minutesActive = 5;
            _otpGeneratorMock.Setup(x => x.GenerateOTP(email, minutesActive)).Throws(new Exception("Test exception"));

            // Act
            var result = _otpController.GenerateOTP(email, minutesActive);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Test exception", badRequestResult.Value);
        }
    }
}
