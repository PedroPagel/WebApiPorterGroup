using Contracts.Request;
using Entities.AreaPredial;
using Infrastructure.Context;
using Infrastructure.ObjectsDao.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.AreaPredial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Unity.Mock;

namespace TestProject.Unity
{
    [TestClass]
    public class CondominioTests
    {
        [TestMethod]
        public async Task Adicionar_um_condominio_sem_erros()
        {
            ICondominioDAO condominioDAO = new CondominioMock();
            IBlocoDAO blocoDAO = new BlocoMock();

            var condominio = new CondominioService(condominioDAO, blocoDAO);

            CondominioRequest request = new()
            {
                EmailSindico = "qweqwe",
                TelefoneSindico = "asdasdasd",
                Nome = "asdasd"
            };

            await condominio.Adicionar(request);

            var list = condominioDAO.ListASync().Result;
            var item = list.Find(x => x.Id == 1);

            Assert.IsTrue(list.Count > 0, "Dados não inseridos");
            Assert.IsTrue(item != null, "Id inserido inválido");
            Assert.IsTrue(list.Count == 1, "Quantidade total incorreta");
        }

        [TestMethod]
        public async Task Alterar_um_condominio_existente()
        {
            ICondominioDAO condominioDAO = new CondominioMock();
            IBlocoDAO blocoDAO = new BlocoMock();

            var condominio = new CondominioService(condominioDAO, blocoDAO);

            var condominioEntity = new Condominio()
            {
                TelefoneSindico = "123",
                EmailSindico = "123",
                Nome = "Teste"
            };

            await condominioDAO.Add(condominioEntity);

            CondominioRequest request = new()
            {
                EmailSindico = "p@gamil.com",
                TelefoneSindico = "321321",
                Nome = "Sol"
            };

            await condominio.Alterar(1, request);

            var list = condominioDAO.ListASync().Result;
            var item = list.Find(x => x.Id == 1);

            Assert.IsTrue(item.EmailSindico.Equals("p@gamil.com"), "Email inserido inválido");
            Assert.IsTrue(item.Nome.Equals("Sol"), "Nome inserido inválido");
            Assert.IsTrue(item.TelefoneSindico.Equals("321321"), "Telefone inserido inválido");
        }

        [TestMethod]
        public async Task Remover_um_condominio_existente()
        {
            ICondominioDAO condominioDAO = new CondominioMock();
            IBlocoDAO blocoDAO = new BlocoMock();

            var condominio = new CondominioService(condominioDAO, blocoDAO);

            var condominioEntity = new Condominio()
            {
                TelefoneSindico = "123",
                EmailSindico = "123",
                Nome = "Teste"
            };

            await condominioDAO.Add(condominioEntity);

            await condominio.Remover(1);

            var list = condominioDAO.ListASync().Result;

            Assert.IsTrue(list.Count == 0, "Condominio nao foi removido");
        }

        [TestMethod]
        public async Task Nao_Remover_um_condominio_existente_com_blocos()
        {
            ICondominioDAO condominioDAO = new CondominioMock();
            IBlocoDAO blocoDAO = new BlocoMock();

            var condominio = new CondominioService(condominioDAO, blocoDAO);

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

            try
            {
                await condominio.Remover(1);
            } catch (Exception e)
            {
                Assert.IsTrue(!e.Equals("Existem Blocos de apartamentos ligados a este condominio"), "Condominio removido indevidamente, existem blocos ligados");
            }
        }
    }
}
