using System;
using System.Collections.Generic;
using System.Text;

namespace EcoNest.Domain.Core
{
    public abstract class Person : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
