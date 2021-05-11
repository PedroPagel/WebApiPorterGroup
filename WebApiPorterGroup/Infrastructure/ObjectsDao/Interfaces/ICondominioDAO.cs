using Entities.AreaPredial;
using Infrastructure.ObjectsDao.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.ObjectsDao.Interfaces
{
    public interface ICondominioDAO : IBaseDAO
    {
        Task<List<Condominio>> ListASync();
        Task<Condominio> Get(int id);
        Task<Condominio> GetByName(string nome);
    }
}
