using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketListMAUI
{
    public class Task
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }

        public bool IsCompleted { get; set; }
        public double Progress { get; set; }

        public List<Option> Options { get; set; }
    }
    public class Option
    {
        public string Text { get; set; }

        public bool IsSelected { get; set; }
    }
}
