using Microsoft.AspNet.SignalR;
using SharedTasks.Data;
using SharedTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SharedTasks.Controllers
{
    [System.Web.Mvc.Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var repo = new TasksRepository(Properties.Settings.Default.ConStr);
            var accountRepo = new UserManager(Properties.Settings.Default.ConStr);
            var vm = new IndexViewModel();
            vm.Tasks = repo.GetUnresolvedTasks();
            vm.User = accountRepo.GetUser(int.Parse(User.Identity.Name));
            return View(vm);
        }
        [HttpPost]
        public ActionResult AddTask(Task t)
        {
            var repo = new TasksRepository(Properties.Settings.Default.ConStr);
            repo.AddTask(t);
            var context = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            context.Clients.All.reload();
            return Redirect("/");
        }
        public ActionResult GetTasks()
        {
            var repo = new TasksRepository(Properties.Settings.Default.ConStr);
            var tasks = repo.GetUnresolvedTasks().Select(t => new
            {
                date = t.Date,
                title = t.Title,
                id = t.Id,
                isCompleted = t.IsCompleted,
                userId = t.UserId,
                userFirstName = t.UserId != null ? t.User.FirstName : null,
                userLastName = t.UserId != null ? t.User.LastName : null
            });
            return Json(new { tasks = tasks, userId = int.Parse(User.Identity.Name) }, JsonRequestBehavior.AllowGet);

        }
    }
}