namespace PersonsManager.DTO.Client
{
    public class ClientForCreationDto
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid UserId { get; set; }
    }
}
