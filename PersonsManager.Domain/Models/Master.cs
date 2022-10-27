namespace PersonsManager.Domain.Models
{
    public class Master : Person
    {
        public double Rating { get; set; }
        public Guid AddressId { get; set; }
    }
}
