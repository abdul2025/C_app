using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consoleApp.interfaces
{
    public interface IOutputWriter
    {
        void ShowData<T>(IEnumerable<T> data);

    }
}