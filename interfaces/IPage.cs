using laboratory.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace laboratory.interfaces
{
    public interface IPage
    {
        Window ParentWindow { get; set; }
        IWidget CurrentWidget { get; set; }
    }
}
