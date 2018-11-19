using DynamicLoadAndUnloadAssembly.Interface;
using DynamicLoadAndUnloadAssembly.ServiceAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DynamicLoadAndUnloadAssembly.APP
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("");
            using (ServiceManager<IObject> ServiceManager=new ServiceManager<IObject>())
            {
                string result = ServiceManager.Proxy.Put("apprun one");
                Console.WriteLine(result);
                Console.WriteLine("");
                Console.WriteLine("----------Thread AppDomain info-------------");
                Console.WriteLine(ServiceManager.CtorProxy.FriendlyName);
                Console.WriteLine(Thread.GetDomain().FriendlyName);
                Console.WriteLine(ServiceManager.CtorProxy.BaseDirectory);
                Console.WriteLine(ServiceManager.CtorProxy.ShadowCopyFiles);
                Console.WriteLine("");
            }
            Console.ReadLine();
        }
    }
}
