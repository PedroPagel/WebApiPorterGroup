using Contracts.Request;
using Contracts.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.AreaPredial.Interfaces
{
    public interface ICondominioService
    {
        Task<CondominioResult> RetornarCondominio(string nome);
        Task<CondominioResult> Adicionar(CondominioRequest request);
        Task<CondominioResult> Alterar(int id, CondominioRequest request);
        Task Remover(int id);
    }
}
