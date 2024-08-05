using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Model
{
    public class UserSession
    {
        public static  string UserId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";
        public static UserTodo DefaultUserTodo = new UserTodo()
        {
            UserId = "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            Items = new List<TodoItem>()
            {
                new TodoItem() { Id = "3cc133a3-2620-4fc5-babf-8dd1bd9ef8f1" , Title =  "Clean room"},
                new TodoItem() { Id = "4b077a34-4229-4fd3-b5d0-33c7eadb7a97" , Title =  "Tidy up desk"},
                new TodoItem() { Id = "6b50d419-41f0-49ef-91fa-b46f7f8b3a00" , Title =  "Make a tea"},
            }
        };
    }
}
