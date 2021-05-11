using Contracts.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Integration
{
    [TestClass]
    public class MoradorTest : BaseTest
    {
        [TestMethod]
        public async Task Retornar_um_morador()
        {
            var response = await _client.GetAsync("api/Morador?nome=Laura&cpf=12345678988");

            Assert.IsTrue(response.IsSuccessStatusCode == true, "Não foi possível buscar a api do retorno de morador");
        }

        [TestMethod]
        public async Task Adicionar_um_morador()
        {
            MoradorRequest request = new()
            {
                Nome = "Pedro",
                Cpf = "123",
                DataNascimento = new DateTime(1986, 7, 9),
                Email = "teste@gmail.com",
                Telefone = "04830301919",
                IdApartamento = 1
            };

            var response = await _client.PostAsJsonAsync("api/Morador", request);

            Assert.IsTrue(response.IsSuccessStatusCode == true, "Não foi possível buscar a api do adição de morador");
        }

        [TestMethod]
        public async Task Alterar_um_morador()
        {
            MoradorRequest request = new()
            {
                Nome = "Pedro",
                Cpf = "123",
                DataNascimento = new DateTime(1986, 7, 9),
                Email = "teste@gmail.com",
                Telefone = "04830301919",
                IdApartamento = 1
            };

            var response = await _client.PutAsJsonAsync("api/Morador?moradorId=1", request);

            Assert.IsTrue(response.IsSuccessStatusCode == true, "Não foi possível buscar a api do alteração de morador");
        }

        [TestMethod]
        public async Task Remover_um_morador()
        {
            var response = await _client.DeleteAsync("api/Morador?moradorId=1");

            Assert.IsTrue(response.IsSuccessStatusCode == true, "Não foi possível buscar a api do remoção de morador");
        }
    }
}
