using Entities.AreaPredial;
using Infrastructure.Context;
using Infrastructure.ObjectsDao.Base;
using Infrastructure.ObjectsDao.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.ObjectsDao
{
    public class CondominioDAO : BaseDAO, ICondominioDAO
    {
        public CondominioDAO(WebApiContext context) : base(context)
        {
        }

        public async Task<List<Condominio>> ListASync()
        {
            return await _context.Condominios.ToListAsync();
        }

        public async Task<Condominio> Get(int id)
        {
            return await _context.Condominios.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Condominio> GetByName(string nome)
        {
            return await _context.Condominios.Where(c => c.Nome.Equals(nome)).FirstOrDefaultAsync();
        }
    }
}
