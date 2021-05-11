using Contracts.Request;
using Entities.AreaPredial;
using Infrastructure.ObjectsDao.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.AreaPredial;
using System;
using System.Threading.Tasks;
using TestProject.Unity.Mock;

namespace TestProject.Unity
{
    [TestClass]
    public class BlocoTests
    {
        [TestMethod]
        public async Task Adicionar_um_bloco_sem_erros()
        {
            ICondominioDAO condominioDAO = new CondominioMock();
            IBlocoDAO blocoDAO = new BlocoMock();
            IApartamentoDAO apartamento = new ApartamentoMock();

            var condominioEntity = new Condominio()
            {
                TelefoneSindico = "123",
                EmailSindico = "123",
                Nome = "Teste"
            };

            await condominioDAO.Add(condominioEntity);

            var bloco = new BlocoService(blocoDAO, apartamento);

            BlocoRequest request = new()
            {
                Nome = "qweqwe",
                IdCondominio = condominioEntity.Id
            };

            await bloco.Adicionar(request);

            var list = blocoDAO.ListASync().Result;
            var item = list.Find(x => x.Id == 1);

            Assert.IsTrue(list.Count > 0, "Dados não inseridos");
            Assert.IsTrue(item != null, "Id inserido inválido");
            Assert.IsTrue(list.Count == 1, "Quantidade total incorreta");

        }

        [TestMethod]
        public async Task Alterar_um_bloco_existente()
        {
            ICondominioDAO condominioDAO = new CondominioMock();
            IBlocoDAO blocoDAO = new BlocoMock();
            IApartamentoDAO apartamento = new ApartamentoMock();

            var condominioEntity = new Condominio()
            {
                TelefoneSindico = "123",
                EmailSindico = "123",
                Nome = "Teste"
            };

            await condominioDAO.Add(condominioEntity);

            var blocoEntity = new Bloco()
            {
                Nome = "bloco",
                CondominioId = condominioEntity.Id
            };

            await blocoDAO.Add(blocoEntity);

            var bloco = new BlocoService(blocoDAO, apartamento);

            BlocoRequest request = new()
            {
                Nome = "qqqq",
                IdCondominio = condominioEntity.Id
            };

            await bloco.Alterar(1, request);

            var list = blocoDAO.ListASync().Result;
            var item = list.Find(x => x.Id == 1);

            Assert.IsTrue(item.Nome.Equals("qqqq"), "Nome alterado inválido");
        }

        [TestMethod]
        public async Task Remover_um_bloco_existente()
        {
            ICondominioDAO condominioDAO = new CondominioMock();
            IBlocoDAO blocoDAO = new BlocoMock();
            IApartamentoDAO apartamentoDao = new ApartamentoMock();

            var bloco = new BlocoService(blocoDAO, apartamentoDao);

            var condominioEntity = new Condominio()
            {
                TelefoneSindico = "123",
                EmailSindico = "123",
                Nome = "Teste"
            };

            await condominioDAO.Add(condominioEntity);

            var blocoEntity = new Bloco()
            {
                Nome = "Bloco A",
                CondominioId = condominioEntity.Id
            };

            await blocoDAO.Add(blocoEntity);

            await bloco.Remover(1);

            var list = blocoDAO.ListASync().Result;

            Assert.IsTrue(list.Count == 0, "Condominio nao foi removido");
        }

        [TestMethod]
        public async Task Nao_Remover_um_bloco_existente_com_apartamentos()
        {
            ICondominioDAO condominioDAO = new CondominioMock();
            IBlocoDAO blocoDAO = new BlocoMock();
            IApartamentoDAO apartamentoDao = new ApartamentoMock();

            var bloco = new BlocoService(blocoDAO, apartamentoDao);

            var condominioEntity = new Condominio()
            {
                TelefoneSindico = "123",
                EmailSindico = "123",
                Nome = "Teste"
            };

            await condominioDAO.Add(condominioEntity);

            var blocoEntity = new Bloco()
            {
                Nome = "Bloco A",
                CondominioId = condominioEntity.Id
            };

            await blocoDAO.Add(blocoEntity);

            var apartamentoEntity = new Apartamento()
            {
                Andar = 1,
                Numero = 101,
                CondominioId = condominioEntity.Id,
                BlocoId = blocoEntity.Id
            };

            await apartamentoDao.Add(apartamentoEntity);

            try
            {
                await bloco.Remover(1);
            }
            catch (Exception e)
            {
                Assert.IsTrue(!e.Equals("Existem apartamento(s) ligado(s) a este bloco"), "Bloco removido indevidamente, existem apartamentos ligados");
            }
        }
    }
}
