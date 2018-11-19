using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLoadAndUnloadAssembly.Interface
{
    //自定义属性并设置属性的使用范围
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface,Inherited =false,AllowMultiple =false)]
    public class CategoryInfoAttribute : Attribute
    {
        public string Category { get; set; }

        public string Describution { get; set; }

        public CategoryInfoAttribute(string category,string decribution)
        {
            this.Category = category;
            this.Describution = decribution;
        }
    }
}
