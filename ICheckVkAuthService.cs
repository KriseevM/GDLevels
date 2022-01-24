using System.Threading.Tasks;

namespace GDLevels
{
    public interface ICheckVkAuthService
    {
         Task<bool> CheckVkAuthByFlowCodeAsync(string code);
    }
}