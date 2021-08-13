using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodoItems.API;
using TodoItems.Models.DTO;
using TodoItems.Models.Entities;
using TodoItems.Test;

namespace TodoLists.Test
{
    [TestClass]
    public class TodoListsTest
    {
        private readonly TodoItemsWebAppFactory<Startup> _factory;
        private const string API_URI = "/api/TodoLists/";

        public TodoListsTest()
        {
            _factory = new TodoItemsWebAppFactory<Startup>();
        }

        [TestMethod]
        public async Task Insert()
        {
            var client = _factory.CreateClient();
            var data = new TodoListPostDTO { Name = "Example List" };
            var res = await client.PostAsJsonAsync(API_URI, data);
            //var resObj = await res.Content.ReadFromJsonAsync<TodoList>();
            Assert.IsTrue(res.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task InsertFail()
        {
            var client = _factory.CreateClient();
            var res = await client.PostAsJsonAsync(API_URI, new TodoListPostDTO { });
            Assert.IsTrue(!res.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task List()
        {
            var client = _factory.CreateClient();
            var res = await client.GetFromJsonAsync<ICollection<TodoList>>(API_URI);
            Assert.IsTrue(res.Count > 0, $"Count equals {res.Count}");
        }

        [TestMethod]
        public async Task UpdateFail()
        {
            var client = _factory.CreateClient();
            var todo = (await client.GetFromJsonAsync<ICollection<TodoList>>(API_URI)).Last();
            var data = new TodoListPutDTO { Id = long.MaxValue, Name = todo.Name };
            var res = await client.PutAsJsonAsync($"{API_URI}{data.Id}", data);
            Assert.IsTrue(!res.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Update()
        {
            var client = _factory.CreateClient();
            var todo = (await client.GetFromJsonAsync<ICollection<TodoList>>(API_URI)).Last();
            var data = new TodoListPutDTO { Id = todo.Id, Name = todo.Name, };
            data.Name = "List 2";
            var res = await client.PutAsJsonAsync($"{API_URI}{data.Id}", data);
            Assert.IsTrue(res.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Delete()
        {
            var client = _factory.CreateClient();
            var todo = (await client.GetFromJsonAsync<ICollection<TodoList>>(API_URI)).Last();
            var res = await client.DeleteAsync($"{API_URI}{todo.Id}");
            Assert.IsTrue(res.IsSuccessStatusCode);
        }
    }
}