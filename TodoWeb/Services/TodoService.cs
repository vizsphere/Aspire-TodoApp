using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Todo.Model;

namespace TodoWeb.Services
{
    public interface ITodoService
    {
        Task<UserTodo> GetById(string id);
        Task UpdateAsync(UserTodo data);
    }

    public class TodoService(HttpClient httpClient) : ITodoService
    {
        private readonly string remoteServiceBaseUrl = "http://localhost:5085/api/todo/";
        
        public async Task<UserTodo> GetById(string id)
        {
            var userTodo =  new UserTodo() { Items = new List<TodoItem>() };

            var res = await httpClient.GetAsync(remoteServiceBaseUrl + id);
            
            if(res.IsSuccessStatusCode)
                userTodo = await res.Content.ReadFromJsonAsync<UserTodo>();   
            
            return userTodo;
        }

        public async Task UpdateAsync(UserTodo data)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, remoteServiceBaseUrl);

            request.Content = JsonContent.Create(data);

            await httpClient.SendAsync(request);
        }
    }
}
