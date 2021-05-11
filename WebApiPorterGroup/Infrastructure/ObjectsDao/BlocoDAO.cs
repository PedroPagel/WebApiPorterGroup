using Entities.AreaPredial;
using Infrastructure.Context;
using Infrastructure.ObjectsDao.Base;
using Infrastructure.ObjectsDao.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ObjectsDao
{
    public class BlocoDAO : BaseDAO, IBlocoDAO
    {
        public BlocoDAO(WebApiContext context) : base(context)
        {
        }

        public async Task<Bloco> BuscarBlocoPorCondominio(int id)
        {
            return await _context.Blocos.Where(b => b.CondominioId == id).FirstOrDefaultAsync();
        }

        public async Task<Bloco> Get(int id)
        {
            return await _context.Blocos.Where(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Bloco> GetByName(string nome)
        {
            return await _context.Blocos.Where(b => b.Nome.Equals(nome)).FirstOrDefaultAsync();
        }

        public async Task<List<Bloco>> ListASync()
        {
            return await _context.Blocos.ToListAsync();
        }
    }
}
