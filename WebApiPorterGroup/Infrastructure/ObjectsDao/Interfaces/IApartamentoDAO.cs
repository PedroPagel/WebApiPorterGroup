using Entities.AreaPredial;
using Infrastructure.ObjectsDao.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.ObjectsDao.Interfaces
{
    public interface IApartamentoDAO : IBaseDAO
    {
        Task<Apartamento> BuscarPorBlocosCondominios(int idCondominio, int idBloco);
        Task<Apartamento> Get(int id);
        Task<Apartamento> BuscarApartamentoPorCondominio(int numeroAp, int andar, int condominio, int bloco);
        Task<List<Apartamento>> ListASync();
    }
}
