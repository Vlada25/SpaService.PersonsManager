﻿namespace PersonsManager.DTO.Client
{
    public class ClientForUpdateDto
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public bool IsBanned { get; set; }
        public string PhoneNumber { get; set; }
    }
}
