using DynamicLoadAndUnloadAssembly.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLoadAndUnloadAssembly.SimpleLibrary
{
    [Serializable]
    public class PlugPut : MarshalByRefObject, IObject
    {
        private string plugName = "My PlugName value is default!";

        public string PlugName
        {
            get { return plugName; }
            set { this.plugName = value; }
        }

        public void Put()
        {
            Console.WriteLine("Default Plug Value is:" + plugName);
        }

        public string Put(string plus)
        {
            Console.WriteLine("Put plus value is:" + plus);
            return ("------------Plugput result info is welcome--------------");
        }
    }
}
