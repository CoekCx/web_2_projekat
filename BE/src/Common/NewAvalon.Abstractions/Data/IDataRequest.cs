using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Abstractions.Data
{
    public interface IDataRequest<in TRequest, TResponse>
    {
        Task<TResponse> GetAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}
