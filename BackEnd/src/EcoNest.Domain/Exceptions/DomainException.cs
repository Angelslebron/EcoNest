using System;
using System.Collections.Generic;
using System.Text;

namespace EcoNest.Domain.Exceptions
{
    public class DomainException(string message) : Exception(message)
    {
    }

    public class NotFoundException(string entity, object key) : DomainException($"{entity} with id {key}  was not found.")
    {
    }

    public class BusinessRuleException(string message) : DomainException(message)
    {
    }
}
