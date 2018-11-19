using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETSafeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //三次装箱 一次拆箱
            //建议：装箱和拆箱有性能损失 通常情况下尽可能避免
            int i = 10;
            object o = i;
            Console.WriteLine(i + "," + (int)o);

            Console.WriteLine(getTimeOfDay(0));

            //遍历枚举索引
            foreach (int j in Enum.GetValues(typeof(TimeOfDay)))
            {
                Console.WriteLine(j);
            }

            //遍历枚举值
            foreach (string temp in Enum.GetNames(typeof(TimeOfDay)))
            {
                Console.WriteLine(temp);
            }

            Console.ReadKey();


        }

        public static string getTimeOfDay(TimeOfDay time)
        {
            string result = string.Empty;
            switch (time)
            {
                case TimeOfDay.Morning:
                    result = "上午";
                    break;
                case TimeOfDay.Afternoon:
                    result = "下午";
                    break;
                case TimeOfDay.Evening:
                    result = "晚上";
                    break;
                default:
                    result = "未知";
                    break;
            }
            return result;
        }

    }

    //在语法上把枚举当成结构时不会有性能损失的，实际上一旦编译好，就变为基本类型
    public enum TimeOfDay
    {
        Morning = 0,
        Afternoon = 1,
        Evening = 2
    }
}
