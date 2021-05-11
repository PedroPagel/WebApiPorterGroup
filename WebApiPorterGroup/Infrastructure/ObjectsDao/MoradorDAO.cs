using Entities.Pessoa;
using Infrastructure.Context;
using Infrastructure.ObjectsDao.Base;
using Infrastructure.ObjectsDao.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.ObjectsDao
{
    public class MoradorDAO : BaseDAO, IMoradorDAO
    {
        public MoradorDAO(WebApiContext context) : base(context)
        {
        }

        public async Task<Morador> Get(int id)
        {
            return await _context.Moradores.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Morador> BuscarMoradorPorCondominio(int condominio, int bloco, string cpf)
        {
            var apartamento = await _context.Apartamentos.Where(a => a.CondominioId == condominio && a.BlocoId == bloco).FirstOrDefaultAsync();
            return await _context.Moradores.Where(c => c.ApartamentoId == apartamento.Id && c.Cpf.Equals(cpf)).FirstOrDefaultAsync();
        }

        public async Task<List<Morador>> ListASync()
        {
            return await _context.Moradores.ToListAsync();
        }

        public async Task<Morador> BuscarMoradorPorApartamento(int idApartamento)
        {
            return await _context.Moradores.Where(c => c.ApartamentoId == idApartamento).FirstOrDefaultAsync();
        }

        public async Task<Morador> BuscarMorador(string nome, string cpf)
        {
            return await _context.Moradores.Where(c => c.Nome.Equals(nome) && c.Cpf.Equals(cpf)).FirstOrDefaultAsync();
        }
    }
}
