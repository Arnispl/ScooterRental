namespace ScooterRental.Exceptions
{
    public class RentEndEarlierThanRentStartException:Exception
    {
        public RentEndEarlierThanRentStartException() : base("Rent can not end before start")
        {

        }
    }
}
