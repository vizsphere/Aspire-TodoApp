namespace Todo.Model
{
    public class UserTodo
    {
        public string UserId { get; set; } = UserSession.UserId;

        public IList<TodoItem> Items { get; set; }
    }

    public class TodoItem
    {
        public TodoItem()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
