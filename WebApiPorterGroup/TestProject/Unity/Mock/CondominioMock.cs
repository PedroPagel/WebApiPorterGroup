using Entities.AreaPredial;
using Infrastructure.ObjectsDao.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Unity.Mock
{
    public class CondominioMock : ICondominioDAO
    {
        private readonly List<Condominio> _condominioDao = new List<Condominio>();

        public async Task Add<TEntity>([NotNull] TEntity entity) where TEntity : class
        {
            (entity as Condominio).Id = _condominioDao.Count + 1;
            await Task.Run(() => _condominioDao.Add(entity as Condominio));
        }

        public async Task<List<Condominio>> ListASync()
        {
            return await Task.Run(() => _condominioDao.FindAll(x => x.Id > 0));
        }

        public async Task Remove<TEntity>([NotNull] TEntity entity) where TEntity : class
        {
            var condominio = _condominioDao.Where(x => x.Id == (entity as Condominio).Id).FirstOrDefault();
            await Task.Run(() => _condominioDao.Remove(condominio));
        }

        public async Task Update<TEntity>([NotNull] TEntity entity) where TEntity : class
        {
            var condominio = _condominioDao.Where(x => x.Id == (entity as Condominio).Id).FirstOrDefault();

            if (entity != null)
            {
                _condominioDao.Remove(condominio);
                await Task.Run(() => _condominioDao.Add(condominio));
            }
        }

        public async Task<Condominio> Get(int id)
        {
            return await Task.Run(() => _condominioDao.Where(x => x.Id == id).FirstOrDefault());
        }

        public async Task<Condominio> GetByName(string nome)
        {
            return await Task.Run(() => _condominioDao.Where(x => x.Nome.Equals(nome)).FirstOrDefault());
        }
    }
}
