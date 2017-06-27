using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTasks.Data
{
    public class TasksRepository
    {
        private string _connectionString;
        public TasksRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddTask(Task t)
        {
            t.Date = DateTime.Now.Date;
            t.IsCompleted = false;
            using(var context = new SharedTasksDBDataContext(_connectionString))
            {
                context.Tasks.InsertOnSubmit(t);
                context.SubmitChanges();
            }
        }
        public IEnumerable<Task> GetUnresolvedTasks()
        {
            using (var context = new SharedTasksDBDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Task>(t => t.User);
                context.LoadOptions = loadOptions;
                return context.Tasks.Where(t => !t.IsCompleted).ToList();
            }
        }
        public void SetUserForTask(int taskId, int userId)
        {
            using (var context = new SharedTasksDBDataContext(_connectionString))
            {
                context.ExecuteCommand("UPDATE Tasks SET UserId = {0} WHERE Id = {1}", userId, taskId);
            }
        }
        public void SetTaskAsCompleted(int taskId)
        {
            using (var context = new SharedTasksDBDataContext(_connectionString))
            {
                context.ExecuteCommand("UPDATE Tasks SET IsCompleted = 1 WHERE Id = {0}", taskId);
            }
        }
    }
}
