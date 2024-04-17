using Microsoft.AspNetCore.Mvc;
using Moq;
using OTP_server.Controllers;
using OTP_server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTP_tests
{
    public class OTPValidationControllerTest
    {
        private readonly Mock<IOTPValidatorService> _otpValidatorMock;
        private readonly OTPValidatorController _otpController;

        public OTPValidationControllerTest()
        {
            _otpValidatorMock = new Mock<IOTPValidatorService>();
            _otpController = new OTPValidatorController(_otpValidatorMock.Object);
        }

        [Fact]
        public void ValidateOTP_ValidOTP_ReturnsOkResult()
        {
            // Arrange
            string email = "test@example.com";
            string otp = "123456";
            _otpValidatorMock.Setup(x => x.ValidateOTP(email, otp)).Returns(true);

            // Act
            var result = _otpController.ValidateOTP(email, otp);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("OTP is valid.", okResult.Value);
        }

        [Fact]
        public void ValidateOTP_InvalidOTP_ReturnsBadRequestResult()
        {
            // Arrange
            string email = "test@example.com";
            string otp = "123456";
            _otpValidatorMock.Setup(x => x.ValidateOTP(email, otp)).Returns(false);

            // Act
            var result = _otpController.ValidateOTP(email, otp);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid OTP.", badRequestResult.Value);
        }
    }
}
