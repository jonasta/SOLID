using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodoItems.API;
using TodoItems.Models.AutoMapperProfiles;
using TodoItems.Models.DTO;
using TodoItems.Models.Entities;

namespace TodoItems.Test
{
    [TestClass]
    public class TodoItemsTest
    {
        private readonly TodoItemsWebAppFactory<Startup> _factory;
        private TodoItem _todoItem;
        private IMapper _mapper;

        public TodoItemsTest()
        {
            _factory = new TodoItemsWebAppFactory<Startup>();
        }

        [TestInitialize]
        public async Task Setup()
        {
            var client = _factory.CreateClient();
            var list = (await client.GetFromJsonAsync<ICollection<TodoList>>("/api/TodoLists/")).Last();
            _todoItem = new TodoItem() { Name = "Dar banho no Cachorro", IsComplete = false, TodoListId = list.Id };

            var myProfile = new TodoItemProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
        }

        [TestMethod]
        public async Task Insert()
        {
            var client = _factory.CreateClient();
            var data = _mapper.Map<TodoItemPostDTO>(_todoItem);
            var res = await client.PostAsJsonAsync($"/api/TodoLists({data.TodoListId})/TodoItems", data);
            //var resObj = await res.Content.ReadFromJsonAsync<TodoItem>();
            Assert.IsTrue(res.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task InsertFail()
        {
            var client = _factory.CreateClient();
            var data = _mapper.Map<TodoItemPostDTO>(_todoItem);
            var res = await client.PostAsJsonAsync($"/api/TodoLists({data.TodoListId})/TodoItems", new TodoItemPostDTO { });
            Assert.IsTrue(!res.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task List()
        {
            var client = _factory.CreateClient();
            var res = await client.GetFromJsonAsync<ICollection<TodoItemDTO>>($"/api/TodoLists({_todoItem.TodoListId})/TodoItems");
            Assert.IsTrue(res.Count > 0, $"Count equals {res.Count}");
        }

        [TestMethod]
        public async Task UpdateFail()
        {
            var client = _factory.CreateClient();
            var todoItem = (await client.GetFromJsonAsync<ICollection<TodoItemDTO>>($"/api/TodoLists({_todoItem.TodoListId})/TodoItems")).Last();
            todoItem.Id = long.MaxValue;
            var res = await client.PutAsJsonAsync($"/api/TodoLists({todoItem.TodoListId})/TodoItems({todoItem.Id})", todoItem);
            Assert.IsTrue(!res.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Update()
        {
            var client = _factory.CreateClient();
            var todoItem = (await client.GetFromJsonAsync<ICollection<TodoItemDTO>>($"/api/TodoLists({_todoItem.TodoListId})/TodoItems")).Last();
            todoItem.Name = "different name";
            var res = await client.PutAsJsonAsync($"/api/TodoLists({todoItem.TodoListId})/TodoItems({todoItem.Id})", todoItem);
            Assert.IsTrue(res.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Delete()
        {
            var client = _factory.CreateClient();
            var todoItem = (await client.GetFromJsonAsync<ICollection<TodoItemDTO>>($"/api/TodoLists({_todoItem.TodoListId})/TodoItems")).Last();
            var res = await client.DeleteAsync($"/api/TodoLists({todoItem.TodoListId})/TodoItems({todoItem.Id})");
            Assert.IsTrue(res.IsSuccessStatusCode);
        }
    }
}