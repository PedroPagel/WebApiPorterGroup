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
    public class ApartamentoDAO : BaseDAO, IApartamentoDAO
    {
        public ApartamentoDAO(WebApiContext context) : base(context)
        {
        }

        public async Task<Apartamento> BuscarPorBlocosCondominios(int idCondominio, int idBloco)
        {
            return await _context.Apartamentos.Where(c => c.CondominioId == idCondominio && c.BlocoId == idBloco).FirstOrDefaultAsync();
        }

        public async Task<Apartamento> Get(int id)
        {
            return await _context.Apartamentos.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Apartamento> BuscarApartamentoPorCondominio(int numeroAp, int andar, int condominio, int bloco)
        {
            return await _context.Apartamentos.Where(c => c.Numero == numeroAp && c.Andar == andar && c.CondominioId == condominio && c.BlocoId == bloco).FirstOrDefaultAsync();
        }

        public async Task<List<Apartamento>> ListASync()
        {
            return await _context.Apartamentos.ToListAsync();
        }
    }
}
