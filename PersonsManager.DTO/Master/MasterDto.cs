namespace PersonsManager.DTO.Master
{
    public class MasterDto
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
    }
}
