using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratory.interfaces
{
    public interface IErrorMessage
    {
        string MessageErrorString { get; set; }
        void ShowMessageErrorString(string errorString);
    }
}
