using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Todo.Model;
using TodoWeb.Models;
using TodoWeb.Services;

namespace TodoWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITodoService _todoService;
    public HomeController(ILogger<HomeController> logger, ITodoService todoService)
    {
        _logger = logger;
        _todoService = todoService;
    }

    public async Task<IActionResult> Index()
    {
        var userTodo = new UserTodo() { Items = new List<TodoItem>() };

        userTodo = await _todoService.GetById(UserSession.UserId);
        
        return View(userTodo);
    }

    public IActionResult Add()
    {
        return View(new TodoItem());
    }

    [HttpPost]
    public async Task<IActionResult> Add(TodoItem todoItem)
    {
        var userTodo = await _todoService.GetById(UserSession.UserId);
        
        userTodo.Items.Add(todoItem);

        await _todoService.UpdateAsync(userTodo);

        return RedirectToAction(nameof(Index));
    }
}
