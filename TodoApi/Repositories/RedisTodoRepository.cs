using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Todo.Model;

namespace TodoApi.Repositories
{
    public class RedisTodoRepository : IRedisTodoRepository
    {
        private readonly ILogger<RedisTodoRepository> _logger;
        private readonly IDatabase _database;
        private static RedisKey TodoKeyPrefix = "/todo/"u8.ToArray();

        public RedisTodoRepository(ILogger<RedisTodoRepository> logger, IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
            _logger = logger;
        }
        private static RedisKey GetTodoKey(string todoId) => TodoKeyPrefix.Append(todoId);

        public async Task<UserTodo> GetById(string id)
        {
            using var data = await _database.StringGetLeaseAsync(GetTodoKey(id));

            if (data is null || data.Length == 0)
            {
                return await AddDefault(id);
            }

            return JsonSerializer.Deserialize(data.Span, TodoSerializationContext.Default.UserTodo);
        }

        public async Task<UserTodo> UpdateAsync(UserTodo data)
        {
            var json = JsonSerializer.SerializeToUtf8Bytes(data, TodoSerializationContext.Default.UserTodo);
            var created = await _database.StringSetAsync(GetTodoKey(data.UserId.ToString()), json);

            if (!created)
            {
                _logger.LogInformation("Problem occurred persisting the item.");
                return null;
            }

            _logger.LogInformation("todo item persisted successfully.");

            return await GetById(GetTodoKey(data.UserId.ToString()));
        }

        private async Task<UserTodo> AddDefault(string id)
        {
            var json = JsonSerializer.SerializeToUtf8Bytes(UserSession.DefaultUserTodo, TodoSerializationContext.Default.UserTodo);
            var created = await _database.StringSetAsync(GetTodoKey(id), json);

            if (!created)
            {
                _logger.LogInformation("Problem occurred persisting the item.");
                return null;
            }

            using var data = await _database.StringGetLeaseAsync(GetTodoKey(id));

            if (data is null || data.Length == 0)
            {
                return null;
            }

            return JsonSerializer.Deserialize(data.Span, TodoSerializationContext.Default.UserTodo);
        }
    }
}


[JsonSerializable(typeof(UserTodo))]
[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
public partial class TodoSerializationContext : JsonSerializerContext
{

}