namespace ScooterRental
{
    public class DuplicateScooterException : Exception {
        public DuplicateScooterException() : base("Scooter with providen id exists.")
        {

        }
    } 
}


