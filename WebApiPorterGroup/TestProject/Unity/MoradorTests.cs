using Contracts.Request;
using Entities.AreaPredial;
using Entities.Pessoa;
using Infrastructure.ObjectsDao.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Pessoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Unity.Mock;

namespace TestProject.Unity
{
    [TestClass]
    public class MoradorTests
    {
        [TestMethod]
        public async Task Adicionar_um_morador_sem_erros()
        {
            ICondominioDAO condominioDAO = new CondominioMock();
            IBlocoDAO blocoDAO = new BlocoMock();
            IApartamentoDAO apartamentoDAO = new ApartamentoMock();
            IMoradorDAO moradorDAO = new MoradorMock(await apartamentoDAO.ListASync());

            var condominioEntity = new Condominio()
            {
                TelefoneSindico = "123",
                EmailSindico = "123",
                Nome = "Teste"
            };

            await condominioDAO.Add(condominioEntity);

            var blocoEntity = new Bloco()
            {
                Nome = "bloco 1",
                CondominioId = condominioEntity.Id
            };

            await blocoDAO.Add(blocoEntity);

            var apartamentoEntity = new Apartamento()
            {
                Andar = 2,
                Numero = 201,
                CondominioId = condominioEntity.Id,
                BlocoId = blocoEntity.Id
            };

            await apartamentoDAO.Add(apartamentoEntity);

            var morador = new MoradorService(moradorDAO);

            MoradorRequest request = new()
            {
                Cpf = "123",
                Nome = "Pedro",
                DataNascimento = new DateTime(1986, 7, 9),
                Email = "pedro@gmail.com",
                Telefone = "04430301919",
                IdApartamento = apartamentoEntity.Id
            };

            await morador.Adicionar(request);

            var list = moradorDAO.ListASync().Result;
            var item = list.Find(x => x.Id == 1);

            Assert.IsTrue(list.Count > 0, "Dados não inseridos");
            Assert.IsTrue(item != null, "Id inserido inválido");
            Assert.IsTrue(list.Count == 1, "Quantidade total incorreta");
        }

        [TestMethod]
        public async Task Alterar_um_morador_existente()
        {
            ICondominioDAO condominioDAO = new CondominioMock();
            IBlocoDAO blocoDAO = new BlocoMock();
            IApartamentoDAO apartamentoDAO = new ApartamentoMock();
            IMoradorDAO moradorDAO = new MoradorMock(await apartamentoDAO.ListASync());

            var condominioEntity = new Condominio()
            {
                TelefoneSindico = "123",
                EmailSindico = "123",
                Nome = "Teste"
            };

            await condominioDAO.Add(condominioEntity);

            var blocoEntity = new Bloco()
            {
                Nome = "bloco 1",
                CondominioId = condominioEntity.Id
            };

            await blocoDAO.Add(blocoEntity);

            var apartamentoEntity = new Apartamento()
            {
                Andar = 2,
                Numero = 201,
                CondominioId = condominioEntity.Id,
                BlocoId = blocoEntity.Id
            };

            await apartamentoDAO.Add(apartamentoEntity);

            var moradorEntity = new Morador()
            {
                Cpf = "123",
                Nome = "Pedro",
                DataNascimento = new DateTime(1986, 7, 9),
                Email = "pedro@gmail.com",
                Telefone = "04430301919",
                ApartamentoId = apartamentoEntity.Id
            };

            await moradorDAO.Add(moradorEntity);

            var morador = new MoradorService(moradorDAO);

            MoradorRequest request = new()
            {
                Cpf = "1234",
                Nome = "Pedro1",
                DataNascimento = new DateTime(1986, 7, 9),
                Email = "pedro@gmail.com1",
                Telefone = "044303019191",
                IdApartamento = apartamentoEntity.Id
            };

            await morador.Alterar(1, request);

            var list = moradorDAO.ListASync().Result;
            var item = list.Find(x => x.Id == 1);

            Assert.IsTrue(item.Nome.Equals("Pedro1"), "Numero alterado inválido");
        }

        [TestMethod]
        public async Task Remover_um_morador_existente()
        {
            ICondominioDAO condominioDAO = new CondominioMock();
            IBlocoDAO blocoDAO = new BlocoMock();
            IApartamentoDAO apartamentoDAO = new ApartamentoMock();
            IMoradorDAO moradorDAO = new MoradorMock(await apartamentoDAO.ListASync());

            var condominioEntity = new Condominio()
            {
                TelefoneSindico = "123",
                EmailSindico = "123",
                Nome = "Teste"
            };

            await condominioDAO.Add(condominioEntity);

            var blocoEntity = new Bloco()
            {
                Nome = "bloco 1",
                CondominioId = condominioEntity.Id
            };

            await blocoDAO.Add(blocoEntity);

            var apartamentoEntity = new Apartamento()
            {
                Andar = 2,
                Numero = 201,
                CondominioId = condominioEntity.Id,
                BlocoId = blocoEntity.Id
            };

            await apartamentoDAO.Add(apartamentoEntity);

            var moradorEntity = new Morador()
            {
                Cpf = "123",
                Nome = "Pedro",
                DataNascimento = new DateTime(1986, 7, 9),
                Email = "pedro@gmail.com",
                Telefone = "04430301919",
                ApartamentoId = apartamentoEntity.Id
            };

            await moradorDAO.Add(moradorEntity);

            var morador = new MoradorService(moradorDAO);

            await morador.Remover(1);

            var list = moradorDAO.ListASync().Result;

            Assert.IsTrue(list.Count == 0, "Morador nao foi removido");
        }
    }
}
