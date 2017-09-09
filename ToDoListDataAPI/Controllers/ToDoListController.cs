using System.Collections.Generic;
using System.Web.Http;
using ToDoListDataAPI.Models;
using ToDoListDataAPI.Business;

namespace ToDoListDataAPI.Controllers
{
    public class ToDoListController : ApiController
    {
        private static Dictionary<int, ToDoItem> mockData = new Dictionary<int, ToDoItem>();
        ToDoListManager _manager = new ToDoListManager();

        static ToDoListController()
        {
            mockData.Add(0, new ToDoItem { ID = 0, Description = "feed the dog" });
            mockData.Add(1, new ToDoItem { ID = 1, Description = "take the dog on a walk" });
        }

        // GET: api/ToDoList&owner=Crystal
        public IEnumerable<ToDoItem> GetTodoItems(string owner = null)
        {
            return _manager.GetTodoItems(owner);
        }

        // GET: api/ToDoList/5
        public ToDoItem GetTodoItemsById(int id)
        {
            return _manager.GetTodoItemById(id);
        }

        // POST: api/ToDoItemList
        public void Post(ToDoItem todo)
        {
             _manager.InsertTodoItem(todo);
        }

        public void Put(ToDoItem todo)
        {
            _manager.EditDescription(todo);
        }

        // DELETE: api/ToDoItemList/5
        public void Delete(int id)
        {
            _manager.DeleteById(id);
        }
    }
}

