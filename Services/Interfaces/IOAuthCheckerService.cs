using System.Threading.Tasks;

namespace GDLevels.Services.Interfaces
{
    public interface IOAuthCheckerService
    {
        Task<bool> CheckByTokenAsync(string token);
    }
}