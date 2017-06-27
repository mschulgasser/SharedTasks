using Microsoft.AspNet.SignalR;
using SharedTasks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharedTasks
{
    public class MyHub : Hub
    {
        public void completedTask(int taskId)
        {
            var repo = new TasksRepository(Properties.Settings.Default.ConStr);
            repo.SetTaskAsCompleted(taskId);
            Clients.All.reload();
        }
        public void reserveTask(int taskId)
        {
            var repo = new TasksRepository(Properties.Settings.Default.ConStr);
            var accountRepo = new UserManager(Properties.Settings.Default.ConStr);
            repo.SetUserForTask(taskId, int.Parse(HttpContext.Current.User.Identity.Name));
            Clients.All.reload();
        }

    }
}