using System;
using System.Collections.Generic;
using System.Text;

namespace EcoNest.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }

    public class NotFoundException : DomainException
    {
        public NotFoundException(string entity, object key)
            : base($"{entity} with id {key}  was not found.") { }
    }

    public class BusinessRuleException : DomainException
    {
        public BusinessRuleException(string message) : base(message) { }
    }
}
