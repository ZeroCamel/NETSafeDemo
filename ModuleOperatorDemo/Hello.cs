using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleOperatorDemo
{
    //模块的生成、设置等基本操作
    //1、创建一个名为Hello的类型
    //2、一个Hello方法
    //3、一个Main(string[] args) 静态方法
    //4、添加类型引用 MSCrolib.dll-（通用对象运行时库） 中的Console.WriteLine Console.Read
    //5、EXE 托管PE文件 模块 程序集
    public class Hello
    {
        public Hello()
        {

        }
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello,world!");
            Console.Read();
        }
    }
}
