namespace ScooterRental.Exceptions
{
    public class StartDateAfterEndDateException:Exception
    {
        public StartDateAfterEndDateException() : base("Start date can not be after end date")
        {

        }
    }
}
