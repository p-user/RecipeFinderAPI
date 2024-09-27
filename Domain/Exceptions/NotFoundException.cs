using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class NotFoundException(string entityType, string entityId) : Exception($"{entityType} with id: {entityId} doesn't exist") { }
    
}
