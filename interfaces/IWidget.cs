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
        IPage ParentPage { get; set; }
        IWidget ParentWidget { get; set; }
        IWidget CurrentWidget { get; set; }
        List<IWidget> Widgets { get; set; }
        void UpdateData();
        void ChangeConfigWidget<T>();
    }
}
