using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratory.interfaces
{
    public interface IConfigWidget
    {
        IWidget ParentWidget { get; set; }
    }
}
