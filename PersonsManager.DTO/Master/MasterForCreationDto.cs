namespace PersonsManager.DTO.Master
{
    public class MasterForCreationDto
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid AddressId { get; set; }
        public Guid UserId { get; set; }
    }
}
