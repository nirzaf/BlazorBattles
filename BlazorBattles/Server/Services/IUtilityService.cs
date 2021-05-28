using System.Threading.Tasks;
using BlazorBattles.Shared;

namespace BlazorBattles.Server.Services
{
    public interface IUtilityService
    {
        Task<User> GetUser();
    }
}