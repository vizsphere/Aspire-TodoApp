using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using Todo.Model;
namespace TodoApi.Repositories
{
    public interface IRedisTodoRepository
    {
        Task<UserTodo> GetById(string id);
        Task<UserTodo> UpdateAsync(UserTodo data);
    }
}
