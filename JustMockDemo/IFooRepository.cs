using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustMockDemo
{
    public interface IFooRepository
    {
        List<Foo> GetFoos { get; set; }
    }
}
