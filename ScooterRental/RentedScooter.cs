namespace ScooterRental
{
    public class RentedScooter
    {
        public RentedScooter(string scooterId, DateTime rentStart, decimal pricePerMinute)
        {
            ScooterId = scooterId;
            RentStart = rentStart;
            PricePerMinute = pricePerMinute;
        }

        public string ScooterId { get; }
        public DateTime RentStart { get; }
        public DateTime RentEnd { get; set; }
        public decimal PricePerMinute { get; }
    }
}
