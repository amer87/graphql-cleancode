using System.Threading.Tasks;

namespace Com.Application.Common.Interfaces;

public interface ISecuredPasswordProvider
{
    Task<string> GetHashedPassword(string password);
}
