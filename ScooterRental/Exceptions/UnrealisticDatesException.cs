namespace ScooterRental.Exceptions
{
    public class UnrealisticDatesException:Exception
    {
        public UnrealisticDatesException() : base("Such dates do not exist")
        {

        }
    }
}
