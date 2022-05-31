using System.Dynamic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using todolist.Models;
using TodoListApp.Models;

namespace todolist.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _db;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        List<Todolist> lists = _db.todolist.OrderBy(x => x.priority).OrderBy(x => x.finish_date).ToList();
        return View(lists);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        Console.WriteLine(id);

        Todolist list = new Todolist();
        list = _db.todolist.FirstOrDefault(t => t.id == id);
        _db.todolist.Remove(list);
        _db.SaveChanges();
        
        return Redirect("~/");
    }

    public IActionResult Edit(int id)
    {
        dynamic model = new ExpandoObject();
        model.todolist = _db.todolist.FirstOrDefault(t => t.id == id);
        model.format_finish = model.todolist.finish_date.ToString("dd/MM/yyyy");
        model.lists = _db.todolist.ToList();

        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(Todolist todolist)
    {
        try
        {
            _db.todolist.Update(todolist);
            _db.SaveChanges();
            return Redirect("~/");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return Redirect("~/");
    }

    [HttpPost]
    public IActionResult AddList(Todolist todolist)
    {
        try
        {
            _db.todolist.Add(todolist);
            _db.SaveChanges();
            return Redirect("~/");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return Redirect("~/");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
