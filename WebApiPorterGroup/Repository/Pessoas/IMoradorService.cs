using Contracts.Request;
using Contracts.Result;
using System.Threading.Tasks;

namespace Services.Pessoas
{
    public interface IMoradorService
    {
        Task<MoradorResult> RetornarMorador(string nome, string cpf);
        Task<MoradorResult> Adicionar(MoradorRequest request);
        Task<MoradorResult> Alterar(int id, MoradorRequest request);
        Task Remover(int id);
    }
}
