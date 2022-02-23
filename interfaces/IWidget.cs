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
        IConfigWidget CurrentConfigWidget { get; set; }
        List<IConfigWidget> ConfigWidgets { get; set; }
        void UpdateData();
        void ChangeConfigWidget<T>();
    }
}
