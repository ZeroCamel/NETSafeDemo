using System;
//有关线程操作
using System.Threading;
using System.Security;
//包含主体操纵内容
using System.Security.Principal;
//主体权限
using System.Security.Permissions;

using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace CustomPerssionDemo
{
    /// <summary>
    /// 自定义代码访问权限
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //1、代码访问安全性
            //CustomPerssion myCustomPerssion = new CustomPerssion(PermissionState.Unrestricted);
            //NamedPermissionSet pset = new NamedPermissionSet("myPermissionSet", PermissionState.None);
            //pset.Description = "Permission Set Contains My Custom Perssion";
            //pset.AddPermission(myCustomPerssion);

            //StreamWriter file = new StreamWriter("myPermission.xml");
            //file.Write(pset.ToXml());
            //file.Close();

            //2、基于角色的安全性编程
            //主题必须匹配的标识或者角色 并同声明式和命令性安全检查都兼容
            //PrincipalPermission p = new PrincipalPermission(PermissionState.None);

            //OutHello();

            //设置当前线程的主体式匿名用户
            Thread.CurrentPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            try
            {
                //OutHello();
                //OutHello1();
            }
            catch (SecurityException e)
            {
                //对主题权限的请求失败
                Console.WriteLine(e.Message);
            }

            //GenericIdentity自定义主体类和策略
            GenericIdentity identity = new GenericIdentity("a");
            string[] roles = new string[] { "Administrators" };

            GenericPrincipal principal = new GenericPrincipal(identity, roles);
            AppDomain.CurrentDomain.SetThreadPrincipal(principal);
            //OutHello1();

            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal wp = new WindowsPrincipal(id);
            //0x220
            if (!wp.IsInRole(0x220))
            {
                Console.WriteLine("The Current User is't Administrator");
            }
            if (wp.IsInRole(@"zhai\administrators"))
            {
                Console.WriteLine("The Current User Is Administrator");
            }
            BEum();

            GenericIdentity myIdentity = new GenericIdentity("xuanxun");
            GenericIdentity myIdentiry1 = new GenericIdentity("xuanxun", "xtype");
            GenericIdentity currentIdentity = GetGenericIdentity();
            GenericIdentity nullIdentity = new GenericIdentity("");

            ShowIdentityInfo(nullIdentity);
            ShowIdentityInfo(myIdentity);
            ShowIdentityInfo(myIdentiry1);
            ShowIdentityInfo(currentIdentity);

            Console.WriteLine(WindowsIdentity.GetAnonymous().Name);
            Console.ReadLine();
        }
        //使用声明式的标记 设置主体的访问权限是管理员权限组
        [PrincipalPermission(SecurityAction.Demand, Role = "Administrator")]
        static void OutHello()
        {
            Console.WriteLine("Hello,world!");
        }

        //使用命令式的标记
        static void OutHello1()
        {
            //通过实例化对象来标识当前的主体权限请求
            new PrincipalPermission("xx", "Administrator").Demand();
            Console.WriteLine("Hello,World!");
        }

        public static void BEum()
        {
            AppDomain myDomain = Thread.GetDomain();
            myDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            WindowsPrincipal myPrinciple = (WindowsPrincipal)Thread.CurrentPrincipal;
            Console.WriteLine("{0}属于：", myPrinciple.Identity.Name.ToString());

            Array wbirFields = Enum.GetValues(typeof(WindowsBuiltInRole));
            foreach (object rolename in wbirFields)
            {
                try
                {
                    Console.WriteLine("{0}?{1}.", rolename, myPrinciple.IsInRole((WindowsBuiltInRole)rolename));
                }
                catch (Exception)
                {
                    Console.WriteLine("{0}:没有包含当前RID的角色", rolename);
                }
            }
        }

        public static void ShowIdentityInfo(GenericIdentity genericIdentity)
        {
            string identityName = genericIdentity.Name;
            string identityAuthenticationType = genericIdentity.AuthenticationType;

            Console.WriteLine("用户名称:" + identityName);

            Console.WriteLine("验证类型:" + identityAuthenticationType);

            if (genericIdentity.IsAuthenticated)
            {
                Console.WriteLine("该用户已验证");
            }
            else
            {
                Console.WriteLine("该用户标识未经过验证");
            }
            Console.WriteLine("------------------------");
        }

        public static GenericIdentity GetGenericIdentity()
        {
            WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
            string authenticIdentity = windowsIdentity.AuthenticationType;
            string userName = windowsIdentity.Name;
            GenericIdentity genericIdentity = new GenericIdentity(userName, authenticIdentity);
            return genericIdentity;
        }
    }
}
