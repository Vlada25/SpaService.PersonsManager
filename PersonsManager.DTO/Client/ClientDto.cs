namespace PersonsManager.DTO.Client
{
    public class ClientDto
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsBanned { get; set; }
        public Guid UserId { get; set; }
    }
}
