using NaszaGrupa.Models;
using System.Collections.Generic;

namespace NaszaGrupa.Services
{
    public interface IdUser
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
}
