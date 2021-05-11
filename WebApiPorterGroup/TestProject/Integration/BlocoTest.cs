using Contracts.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TestProject.Integration
{
    [TestClass]
    public class BlocoTest : BaseTest
    {
        [TestMethod]
        public async Task Retornar_um_bloco()
        {
            var response = await _client.GetAsync("api/Bloco?nome=Bloco%20I");

            Assert.IsTrue(response.IsSuccessStatusCode == true, "Não foi possível buscar a api do retorno de bloco");
        }

        [TestMethod]
        public async Task Adicionar_um_bloco()
        {
            BlocoRequest request = new()
            {
                Nome = "Bloco 1",
                IdCondominio = 1
            };

            var response = await _client.PostAsJsonAsync("api/Bloco", request);

            Assert.IsTrue(response.IsSuccessStatusCode == true, "Não foi possível buscar a api do adição de bloco");
        }

        [TestMethod]
        public async Task Alterar_um_bloco()
        {
            BlocoRequest request = new()
            {
                Nome = "Bloco 1",
                IdCondominio = 1
            };

            var response = await _client.PutAsJsonAsync("api/Bloco?blocoId=1", request);

            Assert.IsTrue(response.IsSuccessStatusCode == true, "Não foi possível buscar a api do alteração de bloco");
        }

        [TestMethod]
        public async Task Remover_um_bloco()
        {
            var response = await _client.DeleteAsync("api/Bloco?blocoId=3");

            Assert.IsTrue(response.IsSuccessStatusCode == true, "Não foi possível buscar a api do remoção de bloco");
        }
    }
}
