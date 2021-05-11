using Entities.Pessoa;
using Infrastructure.ObjectsDao.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.ObjectsDao.Interfaces
{
    public interface IMoradorDAO : IBaseDAO
    {
        Task<Morador> Get(int id);
        Task<Morador> BuscarMoradorPorCondominio(int condominio, int bloco, string cpf);
        Task<Morador> BuscarMoradorPorApartamento(int idApartamento);
        Task<List<Morador>> ListASync();
        Task<Morador> BuscarMorador(string nome, string cpf);
    }
}
