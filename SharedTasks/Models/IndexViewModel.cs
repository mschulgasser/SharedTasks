using SharedTasks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharedTasks.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Task> Tasks { get; set; }
        public User User { get; set; }
    }
}