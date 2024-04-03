namespace ScooterRental
{
    public class InvalidPriceException:Exception
    {
        public InvalidPriceException() : base("provided price is not valid") 
        { 

        }
    }
}
