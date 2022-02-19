using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace laboratory.interfaces
{
    public interface IWidget
    {
        Page ParentPage { get; set; }
        void UpdateData();
    }
}
