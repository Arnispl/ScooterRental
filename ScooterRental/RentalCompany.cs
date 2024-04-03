using ScooterRental.Interfaces;

namespace ScooterRental
{
    public class RentalCompany : IRentalCompany
    {
        private readonly IScooterService _scooterService;
        private readonly IRentedScooterArchive _archive;
        private readonly IRentalCaclulatorService _caclulatorService;

        public RentalCompany(
            string name,
            IScooterService scooterService,
            IRentedScooterArchive archive,
            IRentalCaclulatorService caclulatorService) 
        {
            Name = name;
            _scooterService = scooterService;
            _archive = archive;
            _caclulatorService = caclulatorService;
        }

        public string Name {  get; }

        public void StartRent(string id)
        {
            var scooter = _scooterService.GetScooterById(id);
            _archive.AddRentedScooter(new RentedScooter(scooter.Id, DateTime.Now, scooter.PricePerMinute));
            scooter.IsRented = true;
        }

        public decimal EndRent(string id)
        {
            var scooter = _scooterService.GetScooterById(id);
            var rentalRecord = _archive.EndRental(scooter.Id, DateTime.Now);
            
            scooter.IsRented = false;

            return _caclulatorService.CalculateRent(rentalRecord);
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            return _caclulatorService.CalculateIncome(year, includeNotCompletedRentals);
        }
    }
}
