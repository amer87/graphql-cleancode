using System.Threading;
using System.Threading.Tasks;

namespace Com.Application.Common.Interfaces;

public interface IComDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
