using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLoadAndUnloadAssembly.Interface
{
    [CategoryInfo("Filter.FilterHandler", "")]
    public interface IObject
    {
        void Put();

        string Put(string plus);
    }
}
