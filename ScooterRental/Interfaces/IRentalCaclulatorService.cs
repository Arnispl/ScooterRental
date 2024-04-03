namespace ScooterRental.Interfaces
{
    public interface IRentalCaclulatorService
    {
        decimal CalculateRent(RentedScooter rentalRecord);
        decimal CalculateIncome(int? year, bool includeNotCompletedRentals);
    }
}
