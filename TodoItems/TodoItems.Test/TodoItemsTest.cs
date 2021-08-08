using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TodoItems.API;
using System.Net;
using System.Net.Http.Json;
using System.Collections.Generic;
using TodoItems.Models.Entities;
using System.Text.Json;
using TodoItems.Models.Interfaces;

namespace TodoItems.Test
{
    [TestClass]
    public class TodoItemsTest
    {
        private readonly TodoItemsWebAppFactory<TodoItems.API.Startup> _factory;

        public TodoItemsTest()
        {
            _factory = new TodoItemsWebAppFactory<Startup>();
        }

        [TestMethod]
        public async Task Insert()
        {
            var client = _factory.CreateClient();
            var todo = new TodoItem { Name = "Lavar Cachorro", IsComplete = false };
            var res = await client.PostAsJsonAsync<TodoItem>("/api/TodoItems", todo);
            var resObj = await res.Content.ReadFromJsonAsync<TodoItem>();
           // var resObj = JsonSerializer.Deserialize<TodoItem>(contentStr);

            Assert.AreEqual(resObj.Name, todo.Name);
            Assert.AreEqual(resObj.IsComplete, false);
            Assert.IsTrue(resObj.Id > 0);
        }

        [TestMethod]
        public async Task SimpleList()
        {
            var client = _factory.CreateClient();
            var res = await client.GetAsync("/api/TodoItems");
            Assert.AreEqual(res.StatusCode, HttpStatusCode.OK);

        }

        [TestMethod]
        public async Task List()
        {
            var client = _factory.CreateClient();
            var res = await client.GetFromJsonAsync<ICollection<TodoItem>>("/api/TodoItems");

            Assert.IsTrue(res.Count > 10, $"Count equals {res.Count}");

        }
    }
}
