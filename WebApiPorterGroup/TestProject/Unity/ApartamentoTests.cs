using Contracts.Request;
using Entities.AreaPredial;
using Entities.Pessoa;
using Infrastructure.ObjectsDao.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.AreaPredial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Unity.Mock;

namespace TestProject.Unity
{
    [TestClass]
    public class ApartamentoTests
    {
        [TestMethod]
        public async Task Adicionar_um_apartamento_sem_erros()
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

            var apartamento = new ApartamentoService(apartamentoDAO, moradorDAO);

            ApartamentoRequest request = new()
            {
                Andar = 1,
                Numero = 101,
                IdCondominio = condominioEntity.Id,
                IdBloco = blocoEntity.Id
            };

            await apartamento.Adicionar(request);

            var list = blocoDAO.ListASync().Result;
            var item = list.Find(x => x.Id == 1);

            Assert.IsTrue(list.Count > 0, "Dados não inseridos");
            Assert.IsTrue(item != null, "Id inserido inválido");
            Assert.IsTrue(list.Count == 1, "Quantidade total incorreta");
        }

        [TestMethod]
        public async Task Alterar_um_apartamento_existente()
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

            var apartamento = new ApartamentoService(apartamentoDAO, moradorDAO);

            var apartamentoEntity = new Apartamento()
            {
                Andar = 2,
                Numero = 201,
                CondominioId = condominioEntity.Id,
                BlocoId = blocoEntity.Id
            };

            await apartamentoDAO.Add(apartamentoEntity);

            ApartamentoRequest request = new()
            {
                Andar = 1,
                Numero = 101,
                IdCondominio = condominioEntity.Id,
                IdBloco = blocoEntity.Id
            };

            await apartamento.Alterar(1, request);

            var list = apartamentoDAO.ListASync().Result;
            var item = list.Find(x => x.Id == 1);

            Assert.IsTrue(item.Numero == 101, "Numero alterado inválido");
            Assert.IsTrue(item.Andar == 1, "Andar alterado inválido");
        }

        [TestMethod]
        public async Task Remover_um_apartamento_existente()
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

            var apartamento = new ApartamentoService(apartamentoDAO, moradorDAO);

            var apartamentoEntity = new Apartamento()
            {
                Andar = 2,
                Numero = 201,
                CondominioId = condominioEntity.Id,
                BlocoId = blocoEntity.Id
            };

            await apartamentoDAO.Add(apartamentoEntity);

            await apartamento.Remover(1);

            var list = apartamentoDAO.ListASync().Result;

            Assert.IsTrue(list.Count == 0, "Apartamento nao foi removido");
        }

        [TestMethod]
        public async Task Nao_Remover_um_apartamento_existente_com_moradores()
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

            var apartamento = new ApartamentoService(apartamentoDAO, moradorDAO);

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
                Email = "teste@gmail.com",
                DataNascimento = DateTime.Now,
                ApartamentoId = apartamentoEntity.Id,
                Nome = "teste",
                Telefone = "1233123"
            };

            await moradorDAO.Add(moradorEntity);

            try
            {
                await apartamento.Remover(1);
            }
            catch (Exception e)
            {
                Assert.IsTrue(!e.Equals("Existem morador(es) ligado(s) a este apartamento"), "Apartamento removido indevidamente, existem moradores ligados");
            }
        }
    }
}
