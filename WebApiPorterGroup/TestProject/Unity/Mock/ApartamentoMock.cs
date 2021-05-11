using Entities.AreaPredial;
using Infrastructure.ObjectsDao.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Unity.Mock
{
    public class ApartamentoMock : IApartamentoDAO
    {
        private readonly List<Apartamento> _apartamentoDao = new List<Apartamento>();

        public async Task Add<TEntity>([NotNull] TEntity entity) where TEntity : class
        {
            (entity as Apartamento).Id = _apartamentoDao.Count + 1;
            await Task.Run(() => _apartamentoDao.Add(entity as Apartamento));
        }

        public async Task<List<Apartamento>> ListASync()
        {
            return await Task.Run(() => _apartamentoDao.FindAll(x => x.Id > 0));
        }

        public async Task Remove<TEntity>([NotNull] TEntity entity) where TEntity : class
        {
            var condominio = _apartamentoDao.Where(x => x.Id == (entity as Apartamento).Id).FirstOrDefault();
            await Task.Run(() => _apartamentoDao.Remove(condominio));
        }

        public async Task Update<TEntity>([NotNull] TEntity entity) where TEntity : class
        {
            var condominio = _apartamentoDao.Where(x => x.Id == (entity as Apartamento).Id).FirstOrDefault();

            if (entity != null)
            {
                _apartamentoDao.Remove(condominio);
                await Task.Run(() => _apartamentoDao.Add(condominio));
            }
        }

        public async Task<Apartamento> Get(int id)
        {
            return await Task.Run(() => _apartamentoDao.Where(x => x.Id == id).FirstOrDefault());
        }

        public async Task<Apartamento> BuscarPorBlocosCondominios(int idCondominio, int idBloco)
        {
            return await Task.Run(() => _apartamentoDao.Where(x => x.CondominioId == idCondominio && x.Id == idBloco).FirstOrDefault());
        }

        public async Task<Apartamento> BuscarApartamentoPorCondominio(int numeroAp, int andar, int condominio, int bloco)
        {
            return await Task.Run(() => _apartamentoDao.Where(c => c.Numero == numeroAp && c.Andar == andar && c.CondominioId == condominio && c.BlocoId == bloco).FirstOrDefault());
        }
    }
}
