using Entities.AreaPredial;
using Entities.Pessoa;
using Infrastructure.ObjectsDao.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Unity.Mock
{
    public class MoradorMock : IMoradorDAO
    {
        private readonly List<Morador> _moradorDao = new List<Morador>();
        private readonly List<Apartamento> _apartamentosDao;

        public MoradorMock(List<Apartamento> apartamentosDao)
        {
            _apartamentosDao = apartamentosDao;
        }

        public async Task Add<TEntity>([NotNull] TEntity entity) where TEntity : class
        {
            (entity as Morador).Id = _moradorDao.Count + 1;
            await Task.Run(() => _moradorDao.Add(entity as Morador));
        }

        public async Task<List<Morador>> ListASync()
        {
            return await Task.Run(() => _moradorDao.FindAll(x => x.Id > 0));
        }

        public async Task Remove<TEntity>([NotNull] TEntity entity) where TEntity : class
        {
            var condominio = _moradorDao.Where(x => x.Id == (entity as Morador).Id).FirstOrDefault();
            await Task.Run(() => _moradorDao.Remove(condominio));
        }

        public async Task Update<TEntity>([NotNull] TEntity entity) where TEntity : class
        {
            var condominio = _moradorDao.Where(x => x.Id == (entity as Morador).Id).FirstOrDefault();

            if (entity != null)
            {
                _moradorDao.Remove(condominio);
                await Task.Run(() => _moradorDao.Add(condominio));
            }
        }

        public async Task<Morador> Get(int id)
        {
            return await Task.Run(() => _moradorDao.Where(x => x.Id == id).FirstOrDefault());
        }

        public async Task<Morador> BuscarMoradorPorCondominio(int condominio, int bloco, string cpf)
        {
            var apartamento = await Task.Run(() => _apartamentosDao.Where(a => a.CondominioId == condominio && a.BlocoId == bloco).FirstOrDefault());
            return await Task.Run(() => _moradorDao.Where(c => c.ApartamentoId == apartamento.Id && c.Cpf.Equals(cpf)).FirstOrDefault());
        }

        public async Task<Morador> BuscarMoradorPorApartamento(int idApartamento)
        {
            return await Task.Run(() => _moradorDao.Where(c => c.ApartamentoId == idApartamento).FirstOrDefault());
        }

        public async Task<Morador> BuscarMorador(string nome, string cpf)
        {
            return await Task.Run(() => _moradorDao.Where(c => c.Nome.Equals(nome) && c.Cpf.Equals(cpf)).FirstOrDefault());
        }
    }
}
