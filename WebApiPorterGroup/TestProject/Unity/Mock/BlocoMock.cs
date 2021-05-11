using Entities.AreaPredial;
using Infrastructure.ObjectsDao.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Unity.Mock
{
    public class BlocoMock : IBlocoDAO
    {
        private readonly List<Bloco> _blocoDao = new List<Bloco>();

        public async Task Add<TEntity>([NotNull] TEntity entity) where TEntity : class
        {
            (entity as Bloco).Id = _blocoDao.Count + 1;
            await Task.Run(() => _blocoDao.Add(entity as Bloco));
        }

        public async Task<List<Bloco>> ListASync()
        {
            return await Task.Run(() => _blocoDao.FindAll(x => x.Id > 0));
        }

        public async Task Remove<TEntity>([NotNull] TEntity entity) where TEntity : class
        {
            var bloco = _blocoDao.Where(x => x.Id == (entity as Bloco).Id).FirstOrDefault();
            await Task.Run(() => _blocoDao.Remove(bloco));
        }

        public async Task Update<TEntity>([NotNull] TEntity entity) where TEntity : class
        {
            var bloco = _blocoDao.Where(x => x.Id == (entity as Bloco).Id).FirstOrDefault();

            if (entity != null)
            {
                _blocoDao.Remove(bloco);
                await Task.Run(() => _blocoDao.Add(bloco));
            }
        }

        public async Task<Bloco> Get(int id)
        {
            return await Task.Run(() => _blocoDao.Where(x => x.Id == id).FirstOrDefault());
        }

        public async Task<Bloco> BuscarBlocoPorCondominio(int id)
        {
            return await Task.Run(() => _blocoDao.Where(x => x.CondominioId == id).FirstOrDefault());
        }

        public async Task<Bloco> GetByName(string nome)
        {
            return await Task.Run(() => _blocoDao.Where(x => x.Nome.Equals(nome)).FirstOrDefault());
        }
    }
}
