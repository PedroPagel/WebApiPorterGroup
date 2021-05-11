using Contracts.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TestProject.Integration
{
    [TestClass]
    public class CondominioTest : BaseTest
    {
        [TestMethod]
        public async Task Retornar_um_condominio()
        {
            var response = await _client.GetAsync("api/Condominio?nome=Royal%20Paradise");

            Assert.IsTrue(response.IsSuccessStatusCode == true, "Não foi possível buscar a api do retorno de condominio");
        }

        [TestMethod]
        public async Task Adicionar_um_condominio()
        {
            CondominioRequest request = new()
            {
                EmailSindico = "123",
                TelefoneSindico = "123",
                Nome = "teste"
            };

            var response = await _client.PostAsJsonAsync("api/Condominio", request);

            Assert.IsTrue(response.IsSuccessStatusCode == true, "Não foi possível buscar a api do adição de condominio");
        }

        [TestMethod]
        public async Task Alterar_um_condominio()
        {
            CondominioRequest request = new()
            {
                EmailSindico = "123",
                TelefoneSindico = "123",
                Nome = "teste"
            };

            var response = await _client.PutAsJsonAsync("api/Condominio?condominioId=1", request);

            Assert.IsTrue(response.IsSuccessStatusCode == true, "Não foi possível buscar a api do alteração de condominio");
        }

        [TestMethod]
        public async Task Remover_um_condominio()
        {
            var response = await _client.DeleteAsync("api/Condominio?condominioId=2");

            Assert.IsTrue(response.IsSuccessStatusCode == true, "Não foi possível buscar a api do remoção de condominio");
        }
    }
}