using FluentAssertions;
using ScooterRental.Exceptions;

namespace ScooterRental.Tests
{
    [TestClass]
    public class ScooterServiceTests
    {
        private ScooterService _scooterService;
        private List<Scooter> _scooters;
        private const string _defaultScooterId = "1";

        [TestInitialize]
        public void Setup()
        {
            _scooters = new List<Scooter>();
            _scooterService = new ScooterService(_scooters);
        }

        [TestMethod]
        public void AddScooter_Valid_data_Provided_scooterAdded()
        {
            //Arrage

            //Act
            _scooterService.AddScooter(_defaultScooterId, 0.1m);

            //Assert
            _scooters.Count.Should().Be(1);
        }

        [TestMethod]
        public void AddScooter_Invalid_Price_Provided_InvalidPriceException_Expected()
        {
            //Arrage

            //Act
            Action action = () => _scooterService.AddScooter(_defaultScooterId, 0.0m);
            
            //Assert
            action.Should().Throw<InvalidPriceException>();
        }

        [TestMethod]
        public void AddScooter_Invalid_Id_Provided_InvalidIdException_Expected()
        {
            //Arrage

            //Act
            Action action = () => _scooterService.AddScooter("", 0.1m);

            //Assert
            action.Should().Throw<InvalidIdException>();
        }

        [TestMethod]
        public void AddScooter_Add_Duplicate_Scooter_DuplicateScooterException_Expected()
        {
            //Arrage
            _scooters.Add(new Scooter(_defaultScooterId, 0.5m));
            //Act
            Action action = () => _scooterService.AddScooter(_defaultScooterId, 0.1m);

            //Assert
            action.Should().Throw<DuplicateScooterException>();
        }

        [TestMethod]
        public void RemoveScooter_Existing_ScooterId_Privided_Scooter_Removed()
        {
            //Arrage
            _scooters.Add(new Scooter(_defaultScooterId, 0.5m));
            //Act
            _scooterService.RemoveScooter(_defaultScooterId);

            //Assert
            _scooters.Count.Should().Be(0);
        }

        [TestMethod]
        public void RemoveScooter_NonExisting_ScooterId_Privided_ScooterNotFoundException_Expected()
        {

            //Act
            Action action =  () => _scooterService.RemoveScooter(_defaultScooterId);

            //Assert
            action.Should().Throw<ScooterNotFoundException>();
        }

        [TestMethod]
        public void GetScooters_AllScooters_ReturnsAllScooters()
        {
            // Arrange
            var scooter1 = new Scooter("1", 0.1m);
            var scooter2 = new Scooter("2", 0.2m);
            _scooters.Add(scooter1);
            _scooters.Add(scooter2);

            // Act
            var scooters = _scooterService.GetScooters();

            // Assert
            scooters.Should().HaveCount(2);
            scooters.Should().Contain(new[] { scooter1, scooter2 });
        }

        [TestMethod]
        public void GetScooterById_ValidId_ReturnsScooter()
        {
            // Arrange
            var scooterId = "1";
            var scooter = new Scooter(scooterId, 0.1m);
            _scooters.Add(scooter);

            // Act
            var result = _scooterService.GetScooterById(scooterId);

            // Assert
            result.Should().BeEquivalentTo(scooter);
        }

        [TestMethod]
        public void GetScooterById_InvalidId_ThrowsScooterNotFoundException()
        {
            // Arrange
            var scooterId = "non_existing_id";

            // Act
            Action act = () => _scooterService.GetScooterById(scooterId);

            // Assert
            act.Should().Throw<ScooterNotFoundException>();
        }
    }
}
