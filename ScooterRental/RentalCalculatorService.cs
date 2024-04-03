using ScooterRental.Exceptions;
using ScooterRental.Interfaces;

namespace ScooterRental.Services
{
    public class RentalCalculatorService : IRentalCaclulatorService
    {
        private readonly IRentedScooterArchive _archive;
        private const decimal DailyCap = 20m;

        public RentalCalculatorService(IRentedScooterArchive archive)
        {
            _archive = archive;
        }

        public decimal CalculateRent(RentedScooter rentalRecord)
        {
            decimal totalCost = 0m;

            var rentEnd = rentalRecord.RentEnd == DateTime.MinValue ? DateTime.Now : rentalRecord.RentEnd;

            if (rentalRecord.RentStart > rentEnd)
            {
                throw new StartDateAfterEndDateException();
            }

            if (rentalRecord.RentStart.Year < 2000 || rentalRecord.RentEnd.Year > DateTime.Now.Year + 5)
            {
                throw new UnrealisticDatesException();
            }

            if (rentalRecord.PricePerMinute < 0)
            {
                throw new InvalidPriceException();
            }

            var currentDay = rentalRecord.RentStart.Date;

            while (currentDay <= rentEnd.Date)
            {
                var dayStartTime = currentDay == rentalRecord.RentStart.Date ? rentalRecord.RentStart : currentDay;
                var dayEndTime = currentDay < rentEnd.Date ? currentDay.AddDays(1) : rentEnd;

                var dailyMinutes = (int)(dayEndTime - dayStartTime).TotalMinutes;
                decimal dailyCost = dailyMinutes * rentalRecord.PricePerMinute;

                totalCost += Math.Min(dailyCost, DailyCap);
                currentDay = currentDay.AddDays(1);
            }

            return totalCost;
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            return _archive.GetAllRentals()
                .Where(rental => (!year.HasValue || rental.RentStart.Year == year.Value) &&
                                 (rental.RentEnd != DateTime.MinValue || includeNotCompletedRentals))
                .Sum(rental => CalculateRent(new RentedScooter(rental.ScooterId, rental.RentStart, rental.PricePerMinute)
                {
                    RentEnd = rental.RentEnd != DateTime.MinValue ? rental.RentEnd : DateTime.Now
                }));
        }
    }
}

