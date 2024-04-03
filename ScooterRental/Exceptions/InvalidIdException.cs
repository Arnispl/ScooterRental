namespace ScooterRental
{
    public class InvalidIdException:Exception
    {
        public InvalidIdException(): base("provided id is not valid") 
        {
        
        }
    }
}
