﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsManager.Domain.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public Guid UserId { get; set; }
    }
}
