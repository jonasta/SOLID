using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodoItems.API;
using TodoItems.Models.DTO;
using TodoItems.Models.Entities;

namespace TodoItems.Test
{
    [TestClass]
    public class TodoItemsTest
    {
        private readonly TodoItemsWebAppFactory<Startup> _factory;
        private const string API_URI = "/api/TodoItems/";
        private readonly TodoItem defaultModel = new() { Name = "Lavar Cachorro", IsComplete = false };

        public TodoItemsTest()
        {
            _factory = new TodoItemsWebAppFactory<Startup>();
        }

        [TestMethod]
        public async Task Insert()
        {
            var client = _factory.CreateClient();
            var res = await client.PostAsJsonAsync(API_URI, defaultModel);
            //var resObj = await res.Content.ReadFromJsonAsync<TodoItem>();
            Assert.IsTrue(res.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task InsertFail()
        {
            var client = _factory.CreateClient();
            var res = await client.PostAsJsonAsync(API_URI, new TodoItemPostDTO { });
            //var resObj = await res.Content.ReadFromJsonAsync<TodoItem>();
            Assert.IsTrue(res.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task List()
        {
            var client = _factory.CreateClient();
            var res = await client.GetFromJsonAsync<ICollection<TodoItem>>(API_URI);
            Assert.IsTrue(res.Count > 0, $"Count equals {res.Count}");
        }

        [TestMethod]
        public async Task Update()
        {
            var client = _factory.CreateClient();
            var todo = (await client.GetFromJsonAsync<ICollection<TodoItem>>(API_URI)).Last();
            Assert.AreEqual(todo.Name, defaultModel.Name);
            todo.Name = "Lavar Gato";
            var res = await client.PutAsJsonAsync($"{API_URI}{todo.Id}", todo);
            Assert.IsTrue(res.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Delete()
        {
            var client = _factory.CreateClient();
            var todo = (await client.GetFromJsonAsync<ICollection<TodoItem>>(API_URI)).Last();
            var res = await client.DeleteAsync($"{API_URI}{todo.Id}");
            Assert.IsTrue(res.IsSuccessStatusCode);
        }
    }
}