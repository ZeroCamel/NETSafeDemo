using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace ReflectionForGetAssemDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (Assembly ab in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine(ab.FullName);
                //mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
                OutModule(ab);
                Console.ReadKey();
            }
        }
        //返回每个程序集的模块
        public static void OutModule(Assembly ab)
        {
            foreach (Module md in ab.GetModules())
            {
                Console.WriteLine(ab);
                Console.WriteLine(md.Name);
                OutTypes(md);
                Console.WriteLine();
            }
        }
        //获得每个模块里的类型
        public static void OutTypes(Module md)
        {
            foreach (Type ty in md.GetTypes())
            {
                Console.WriteLine(ty);
                OutMethods(ty);
            }
        }
        //获取模块里的方法
        public static void OutMethods(Type t)
        {
            foreach (MemberInfo mi in t.GetMethods())
            {
                Console.WriteLine(mi.Name);
            }
        }
        //调用
    }
}
