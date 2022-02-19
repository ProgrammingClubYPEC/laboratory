using laboratory.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratory.interfaces
{
    public interface IAction
    {
        List<ButtonAction> Actions { get; set; }
    }
}
