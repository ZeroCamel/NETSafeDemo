using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
//
using System.Reflection.Emit;

namespace DynaCreateAssemDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //动态的定义类型
            AssemblyBuilder ab = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("test"),AssemblyBuilderAccess.Run);
            //模型
            ModuleBuilder mb = ab.DefineDynamicModule("showHello");
            //类型
            TypeBuilder tb = mb.DefineType("TestClass", TypeAttributes.Public);
            //函数的声明 不能生成函数的执行代码
            MethodBuilder mbb = tb.DefineMethod("method1",MethodAttributes.Public);
            //IL代码 64bttes 默认 64位
            ILGenerator ilg = mbb.GetILGenerator();
        }
    }
}
