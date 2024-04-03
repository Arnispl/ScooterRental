using FluentAssertions;

namespace ScooterRental.Tests
{
    [TestClass]
    public class RentedScooterTests
    {
        [TestMethod]
        public void Constructor_InitializeProperties_ReturnsExpectedValues()
        {
            // Arrange
            string expectedScooterId = "scooter-1";
            DateTime expectedRentStart = DateTime.Now.AddMinutes(-30);
            decimal expectedPricePerMinute = 0.2m;
            DateTime expectedRentEnd = DateTime.Now;

            // Act
            RentedScooter rentedScooter = new RentedScooter(expectedScooterId, expectedRentStart, expectedPricePerMinute)
            {
                RentEnd = expectedRentEnd
            };

            // Assert 
            rentedScooter.ScooterId.Should().Be(expectedScooterId);
            rentedScooter.RentStart.Should().Be(expectedRentStart);
            rentedScooter.PricePerMinute.Should().Be(expectedPricePerMinute);
            rentedScooter.RentEnd.Should().Be(expectedRentEnd);
        }
        [TestMethod]
        public void Getters_ReturnExpectedValues()
        {
            // Arrange
            string expectedScooterId = "scooter-1";
            DateTime expectedRentStart = DateTime.Now.AddMinutes(-30);
            decimal expectedPricePerMinute = 0.2m;
            DateTime expectedRentEnd = DateTime.Now; 
            RentedScooter rentedScooter = new RentedScooter(expectedScooterId, expectedRentStart, expectedPricePerMinute)
            {
                RentEnd = expectedRentEnd
            };

            // Act
            string actualScooterId = rentedScooter.ScooterId;
            DateTime actualRentStart = rentedScooter.RentStart;
            DateTime actualRentEnd = rentedScooter.RentEnd;
            decimal actualPricePerMinute = rentedScooter.PricePerMinute;

            // Assert 
            actualScooterId.Should().Be(expectedScooterId);
            actualRentStart.Should().Be(expectedRentStart);
            actualRentEnd.Should().Be(expectedRentEnd);
            actualPricePerMinute.Should().Be(expectedPricePerMinute);
        }

    }
}
