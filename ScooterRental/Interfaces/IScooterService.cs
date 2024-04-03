namespace ScooterRental.Interfaces
{
    public interface IScooterService
    {
        public void AddScooter(string id, decimal pricePerMinute);

        public void RemoveScooter(string id);

        public IList<Scooter> GetScooters();

        public Scooter GetScooterById(string scooterId);
    }
}
