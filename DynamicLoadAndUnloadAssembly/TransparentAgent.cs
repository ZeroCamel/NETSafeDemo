using DynamicLoadAndUnloadAssembly.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLoadAndUnloadAssembly.ServiceAgent
{
    //MarshalByRefObject：允许支持远程处理的应用程序中跨应用程序域边界访问
    //不同应用程序域中的对象的通信方式由两种
    //1、跨应用程序域边界传输对象副本
    //2、使用代理交换消息
    class TransparentAgent : MarshalByRefObject
    {
        private const BindingFlags bfi = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;

        //无参构造函数
        public TransparentAgent() { }

        public IObject Create(string assemblyFile,string typeName,object[] args)
        {
            //Activator-包含方法用以创建本地和远程对象，或者获取对现有远程对象的引用，此类不能被继承
            //contains methods to increate objects loaclly or remotely,or obtain references to existing remote objects. this class cannot be inherited
            return (IObject)Activator.CreateInstanceFrom(assemblyFile, typeName, false, bfi, null, args, null, null).Unwrap();
        }

        //泛型创建
        public T Create<T>(string assemblyPath,string typeName,object[] args)
        {
            string assemblyFile = AssemblyHelper.LoadAssemblyFile(assemblyPath, typeName);
            return (T)Activator.CreateInstanceFrom(assemblyFile, typeName, false, bfi, null, args, null, null).Unwrap();
        }
    }
}
