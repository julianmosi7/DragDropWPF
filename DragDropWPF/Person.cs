using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragDropWPF
{
    public class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public override string ToString() => $"{Firstname} {Lastname}";
    }
}
