using FluentAssertions;
using ScooterRental.Exceptions;
using ScooterRental.Services;

namespace ScooterRental.Tests
{
    [TestClass]
    public class RentalCalculatorServiceTests
    {
        private RentalCalculatorService _calculatorService;
        private RentedScooterArchive _archive;

        [TestInitialize]
        public void Setup()
        {
            _archive = new RentedScooterArchive();
            _calculatorService = new RentalCalculatorService(_archive);

            var currentDate = new DateTime(2024, 2, 2, 12, 0, 0);

            _archive.AddRentedScooter(CreateRental("1", currentDate.AddHours(-2), 0.1m, 30));
            _archive.AddRentedScooter(CreateRental("2", currentDate.AddMonths(-1), 0.1m, 60)); 

            _archive.AddRentedScooter(CreateRental("3", currentDate.AddMonths(-2), 0.1m, 0));

            _archive.AddRentedScooter(CreateRental("4", new DateTime(2023, 1, 1), 0.1m, 60));

        }

        private RentedScooter CreateRental(string scooterId, DateTime rentStart, decimal pricePerMinute, int minutes)
        {
            return new RentedScooter(scooterId, rentStart, pricePerMinute)
            {
                RentEnd = rentStart.AddMinutes(minutes)
            };
        }

        [TestMethod]
        public void CalculateRent_ThirtyMinuteRental_ReturnsCorrectAmount()
        {
            // Arrange
            var rentedScooter = CreateRental("test-scooter", new DateTime(2024, 2, 2, 11, 30, 0), 0.1m, 30);

            // Act
            decimal rent = _calculatorService.CalculateRent(rentedScooter);

            // Assert
            rent.Should().Be(3);
        }

        [TestMethod]
        public void CalculateRent_ThreeHourRental_ReturnsCorrectAmount()
        {
            // Arrang
            var rentedScooter = CreateRental("test-scooter-3hr", new DateTime(2024, 2, 2, 9, 0, 0), 0.1m, 180); 

            // Act
            decimal rent = _calculatorService.CalculateRent(rentedScooter);

            // Assert
            rent.Should().Be(18);
        }

        [TestMethod]
        public void CalculateRent_OneDayRental_ReturnsCorrectAmount()
        {
            // Arrange
            var rentalStart = new DateTime(2024, 2, 2, 12, 0, 0);
            var rentalEnd = new DateTime(2024, 2, 2, 23, 59, 0);
            var rentedScooter = CreateRental("test-scooter", rentalStart, 0.1m, 719); 

            // Act
            decimal rent = _calculatorService.CalculateRent(rentedScooter);

            // Assert
            rent.Should().Be(20); 
        }

        [TestMethod]
        public void CalculateRent_TwoDayRental_ReturnsCorrectAmount()
        {
            // Arrange
            var rentalStart = new DateTime(2024, 2, 2, 12, 0, 0);
            var rentalEnd = new DateTime(2024, 2, 3, 3, 21, 0);
            var rentedScooter = CreateRental("test-scooter", rentalStart, 0.1m, 1221); 

            // Act
            decimal rent = _calculatorService.CalculateRent(rentedScooter);

            // Assert
            rent.Should().Be(40);
        }

        [TestMethod]
        public void CalculateRent_ZeroMinuteRental_ReturnsZero()
        {
            // Arrange
            var rentedScooter = CreateRental("zero-minute-scooter", new DateTime(2024, 2, 2, 12, 0, 0), 0.1m, 0);

            // Act
            decimal rent = _calculatorService.CalculateRent(rentedScooter);

            // Assert
            rent.Should().Be(0);
        }

        [TestMethod]
        public void CalculateRent_StartDateAfterEndDate_StartDateAfterEndDateException()
        {
            // Arrange
            var rentalStart = new DateTime(2024, 2, 3, 12, 0, 0); 
            var rentalEnd = new DateTime(2024, 2, 2, 12, 0, 0);   
            var rentedScooter = new RentedScooter("invalid-rental", rentalStart, 0.1m)
            {
                RentEnd = rentalEnd
            };

            // Act
            Action act = () => _calculatorService.CalculateRent(rentedScooter);

            // Assert
            act.Should().Throw<StartDateAfterEndDateException>()
                .WithMessage("Start date can not be after end date");
        }

        [TestMethod]
        public void CalculateRent_UnrealisticDates_UnrealisticDatesException()
        {
            // Arrange
            var rentalStart = new DateTime(1, 1, 1); 
            var rentalEnd = new DateTime(9999, 12, 31); 
            var rentedScooter = new RentedScooter("unrealistic-rental", rentalStart, 0.1m)
            {
                RentEnd = rentalEnd
            };

            // Act
            Action act = () => _calculatorService.CalculateRent(rentedScooter);

            // Assert
            act.Should().Throw<UnrealisticDatesException>()
                .WithMessage("Such dates do not exist");
        }

        [TestMethod]
        public void CalculateRent_NegativePricePerMinute_InvalidPriceException()
        {
            // Arrange
            var rentalStart = new DateTime(2024, 2, 2, 12, 0, 0);
            var rentalEnd = new DateTime(2024, 2, 2, 15, 0, 0); 
            var rentedScooter = new RentedScooter("negative-price", rentalStart, -0.1m)
            {
                RentEnd = rentalEnd
            };

            // Act
            Action act = () => _calculatorService.CalculateRent(rentedScooter);

            // Assert
            act.Should().Throw<InvalidPriceException>()
                .WithMessage("provided price is not valid");
        }

        [TestMethod]
        public void CalculateIncome_ForSpecificYear_CalculatesCorrectIncome()
        {
            // Arrange
            int testYear = 2024;
            decimal expectedIncome = 9;

            // Act
            decimal income = _calculatorService.CalculateIncome(testYear, false);

            // Assert
            income.Should().Be(expectedIncome);
        }
    }
}



