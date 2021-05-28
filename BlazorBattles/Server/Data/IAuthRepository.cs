using BlazorBattles.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBattles.Server.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password, int startUnitId);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}
