using Contracts.Request;
using Contracts.Result;
using System.Threading.Tasks;

namespace Services.AreaPredial.Interfaces
{
    public interface IApartamentoService
    {
        Task<ApartamentoResult> RetornarApartamento(int numeroAp, int andar, int condiminio, int bloco);
        Task<ApartamentoResult> Adicionar(ApartamentoRequest request);
        Task<ApartamentoResult> Alterar(int id, ApartamentoRequest request);
        Task Remover(int id);
    }
}
