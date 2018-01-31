using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustMockDemo
{
   public class Item
    {
        public virtual string Name { get; set; }

        public Item(string name)
        {
            this.Name = name;
        }
    }
}
