using Contracts.Request;
using Contracts.Result;
using System.Threading.Tasks;

namespace Services.AreaPredial.Interfaces
{
    public interface IBlocoService
    {
        Task<BlocoResult> RetornarBloco(string nome);
        Task<BlocoResult> Adicionar(BlocoRequest request);
        Task<BlocoResult> Alterar(int id, BlocoRequest request);
        Task Remover(int id);
    }
}
