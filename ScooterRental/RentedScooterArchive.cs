using System.Collections.Generic;
using ScooterRental.Exceptions;
using ScooterRental.Interfaces;

namespace ScooterRental
{
    public class RentedScooterArchive : IRentedScooterArchive
    {
        private readonly List<RentedScooter> _rentals = new List<RentedScooter>();

        public void AddRentedScooter(RentedScooter scooter)
        {
            _rentals.Add(scooter);
        }

        public RentedScooter EndRental(string scooterId, DateTime rentEnd)
        {
            var scooter = _rentals.FirstOrDefault(s => s.ScooterId == scooterId);

            if (scooter == null)
            {
                throw new ScooterNotFoundException();
            }

            if (rentEnd < scooter.RentStart)
            {
                throw new RentEndEarlierThanRentStartException();
            }

            scooter.RentEnd = rentEnd;

            return scooter;
        }

        public IEnumerable<RentedScooter> GetAllRentals()
        {
            return _rentals.AsReadOnly();
        }
    }
}
