using Entities.AreaPredial;
using Infrastructure.ObjectsDao.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ObjectsDao.Interfaces
{
    public interface IBlocoDAO : IBaseDAO
    {
        Task<Bloco> BuscarBlocoPorCondominio(int id);
        Task<Bloco> Get(int id);
        Task<Bloco> GetByName(string nome);
        Task<List<Bloco>> ListASync();
    }
}
