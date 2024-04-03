using FluentAssertions;

namespace ScooterRental.Tests
{
    [TestClass]
    public class ScooterTests
    {
        [TestMethod]
        public void Constructor_InitializeProperties_Success()
        {
            // Arrange
            string expectedId = "scooter-1";
            decimal expectedPricePerMinute = 0.2m;

            // Act
            Scooter scooter = new Scooter(expectedId, expectedPricePerMinute);

            // Assert
            scooter.Id.Should().Be(expectedId);
            scooter.PricePerMinute.Should().Be(expectedPricePerMinute);
        }
        
        [TestMethod]
        public void Getters_IdAndPricePerMinute_ReturnExpectedValues()
        {
            // Arrange
            string expectedId = "scooter-1";
            decimal expectedPricePerMinute = 0.2m;
            Scooter scooter = new Scooter(expectedId, expectedPricePerMinute);

            // Act
            string actualId = scooter.Id;
            decimal actualPricePerMinute = scooter.PricePerMinute;

            // Assert 
            actualId.Should().Be(expectedId);
            actualPricePerMinute.Should().Be(expectedPricePerMinute);
        }

        [TestMethod]
        public void IsRented_InitiallySetToFalse_UpdatesCorrectly()
        {
            // Arrange
            Scooter scooter = new Scooter("scooter-1", 0.2m);

            // Assert
            scooter.IsRented.Should().BeFalse();
        }

        [TestMethod]
        public void IsRented_SetIsRentedToTrue_UpdatesCorrectly()
        {
            // Arrange
            Scooter scooter = new Scooter("scooter-1", 0.2m);

            // Act
            scooter.IsRented = true;

            // Assert
            scooter.IsRented.Should().BeTrue();
        }

        [TestMethod]
        public void IsRented_SetIsRentedToFalse_UpdatesCorrectly()
        {
            // Arrange
            Scooter scooter = new Scooter("scooter-1", 0.2m);

            // Act
            scooter.IsRented = true;
            scooter.IsRented = false;

            // Assert
            scooter.IsRented.Should().BeFalse();

        }
    }
}

