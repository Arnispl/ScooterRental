namespace ScooterRental.Interfaces
{
    public interface IRentedScooterArchive
    {
        void AddRentedScooter(RentedScooter scooter);

        RentedScooter EndRental(string scooterId, DateTime rentEnd);

        IEnumerable<RentedScooter> GetAllRentals(); // Add this method
    }
}
