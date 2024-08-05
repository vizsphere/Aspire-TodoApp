using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Todo.Model;
using TodoApi.Repositories;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        ILogger<TodoController> _logger;
        IRedisTodoRepository _todoRepository;

        public TodoController(ILogger<TodoController> logger, IRedisTodoRepository todoRepository)
        {
            _logger = logger;   
            _todoRepository = todoRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([DefaultValue("3fa85f64-5717-4562-b3fc-2c963f66afa6")] string id)
        {
            var todo = await _todoRepository.GetById(id);
            if (todo == null)
                return BadRequest();
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserTodo data)
        {
            if (data == null)
                return BadRequest();
            var todo = await _todoRepository.UpdateAsync(data);
            return Ok(todo);
        }
    }
}
