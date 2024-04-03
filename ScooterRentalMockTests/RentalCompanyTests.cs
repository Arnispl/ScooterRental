using FluentAssertions;
using Moq;
using Moq.AutoMock;
using ScooterRental.Interfaces;

namespace ScooterRental.Moq.Tests
{
    [TestClass]
    public class RentalCompanyTests
    {
        private AutoMocker _mocker;
        private RentalCompany _company;
        private Mock<IScooterService> _scooterServiceMock;
        private Mock<IRentedScooterArchive> _rentedScooterArchiveMock;
        private Mock<IRentalCaclulatorService> _rentalCaclulatorMock;
        private const string _defaultCompanyName = "tests";

        [TestInitialize]
        public void Setup()
        {
            _mocker = new AutoMocker();
            _scooterServiceMock = _mocker.GetMock<IScooterService>();
            _rentedScooterArchiveMock = _mocker.GetMock<IRentedScooterArchive>();
            _rentalCaclulatorMock = _mocker.GetMock<IRentalCaclulatorService>();
            _company = new RentalCompany(
            _defaultCompanyName, 
            _scooterServiceMock.Object, 
            _rentedScooterArchiveMock.Object,
            _rentalCaclulatorMock.Object);
        }

        [TestMethod]

        public void StartRent_ExistingScooterIsRented()
        {
            // Arrange
            var scooter = new Scooter("1", 0.2m);
            _scooterServiceMock.Setup(s => s.GetScooterById("1")).Returns(scooter);

            _company.StartRent("1");
            
            // Assert 
            scooter.IsRented.Should().BeTrue();
        }
        [TestMethod]

        public void EndRent_StopRenting_ExistingScooter_ScooterRentStopped()
        {
            // Arrange
            var scooter = new Scooter("1", 0.2m) {IsRented = true};
            var now = DateTime.Now;
            var rentalRecord = new RentedScooter(scooter.Id, now.AddMinutes(-20), scooter.PricePerMinute) {RentEnd = now};
            _scooterServiceMock.Setup(s => s.GetScooterById("1")).Returns(scooter);
            _rentedScooterArchiveMock.Setup(archive=>archive.EndRental(scooter.Id, It.IsAny<DateTime>() )).Returns(rentalRecord);
            _rentalCaclulatorMock.Setup(calculator => calculator.CalculateRent(rentalRecord)).Returns(4);
            _ = _company.EndRent("1");

            // Assert 
            scooter.IsRented.Should().BeFalse();
        }

        [TestMethod]
        public void CalculateIncome_ReturnsExpectedIncome()
        {
            // Arrange
            int testYear = 2024;
            bool includeNotCompletedRentals = true;
            decimal expectedIncome = 50; 

            _rentalCaclulatorMock.Setup(calculator => calculator.CalculateIncome(testYear, includeNotCompletedRentals))
                                .Returns(expectedIncome);

            // Act
            decimal income = _company.CalculateIncome(testYear, includeNotCompletedRentals);

            // Assert 
            income.Should().Be(expectedIncome);
        }

    }
}
