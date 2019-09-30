using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp.net_tutorial.Models
{
    public class TodoItem
    {
        public long Id { get; set; } //functions as the UNIQUE KEY in db
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
