using ScooterRental.Interfaces;
using FluentAssertions;
using ScooterRental.Exceptions;

namespace ScooterRental.Tests
{
    [TestClass]
    public class RentedScooterArchiveTests
    {
        private IRentedScooterArchive _archive;

        [TestInitialize]
        public void Setup()
        {
            _archive = new RentedScooterArchive();
        }

        private RentedScooter CreateRental(string scooterId, DateTime rentStart, decimal pricePerMinute, int minutes)
        {
            return new RentedScooter(scooterId, rentStart, pricePerMinute)
            {
                RentEnd = rentStart.AddMinutes(minutes)
            };
        }

        [TestMethod]
        public void AddRentedScooter_SingleRentalAdded_RentalInArchive()
        {
            // Arrange
            var rental = new RentedScooter("1", DateTime.Now.AddMinutes(-30), 0.1m);

            // Act
            _archive.AddRentedScooter(rental);

            // Assert
            var rentals = _archive.GetAllRentals();
            rentals.Should().Contain(rental)
                .And.HaveCount(1);
        }

        [TestMethod]
        public void AddRentedScooter_MultipleRentalsAdded_AllRentalsInArchive()
        {
            // Arrange
            var rental1 = new RentedScooter("1", DateTime.Now.AddMinutes(-30), 0.1m);
            var rental2 = new RentedScooter("2", DateTime.Now.AddMinutes(-45), 0.2m);

            // Act
            _archive.AddRentedScooter(rental1);
            _archive.AddRentedScooter(rental2);

            // Assert
            var rentals = _archive.GetAllRentals();
            rentals.Should().Contain(rental1)
                .And.Contain(rental2)
                .And.HaveCount(2);
        }

        [TestMethod]
        public void EndRental_ValidScooterIdAndRentEnd_RentalEnded()
        {
            // Arrange
            var scooterId = "1";
            var rentalStart = DateTime.Now.AddMinutes(-30);
            var rentalEnd = DateTime.Now;
            var rental = new RentedScooter(scooterId, rentalStart, 0.1m);
            _archive.AddRentedScooter(rental);

            // Act
            var endedRental = _archive.EndRental(scooterId, rentalEnd);

            // Assert
            endedRental.Should().NotBeNull();
            endedRental.RentEnd.Should().Be(rentalEnd);
        }

        [TestMethod]
        public void EndRental_InvalidScooterId_ScooterNotFoundException()
        {
            // Arrange
            var scooterId = "1";
            var rentalEnd = DateTime.Now;

            // Act 
            Action act = () => _archive.EndRental(scooterId, rentalEnd);

            // Assert
            act.Should().Throw<ScooterNotFoundException>();
        }

        [TestMethod]
        public void EndRental_RentEndEarlierThanRentStart_RentEndEarlierThanRentStartException()
        {
            // Arrange
            var scooterId = "1";
            var rentalStart = DateTime.Now.AddMinutes(-30);
            var rentalEnd = rentalStart.AddMinutes(-15);
            var rental = new RentedScooter(scooterId, rentalStart, 0.1m);
            _archive.AddRentedScooter(rental);

            // Act
            Action act = () => _archive.EndRental(scooterId, rentalEnd);

            //Assert
            act.Should().Throw<RentEndEarlierThanRentStartException>()
                .WithMessage("Rent can not end before start");
        }

        [TestMethod]
        public void GetAllRentals_ReturnsAllRentals()
        {
            // Arrange
            var rental1 = CreateRental("1", DateTime.Now.AddHours(-2), 0.1m, 30);
            var rental2 = CreateRental("2", DateTime.Now.AddMonths(-1), 0.1m, 60);

            _archive.AddRentedScooter(rental1);
            _archive.AddRentedScooter(rental2);

            // Act
            var rentals = _archive.GetAllRentals();

            // Assert
            rentals.Should().NotBeNull().And.HaveCount(2).And.Contain(rental1).And.Contain(rental2);
        }

        [TestMethod]
        public void GetAllRentals_EmptyArchive_ReturnsEmptyCollection()
        {
            // Act
            var rentals = _archive.GetAllRentals();

            // Assert
            rentals.Should().NotBeNull().And.BeEmpty();
        }
    }
}

