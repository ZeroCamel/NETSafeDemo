using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAppDomainDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ////创建 
            //AppDomain myDomain = AppDomain.CreateDomain("MyDomainName");
            //获取此应用程序域的友好名称
            //Console.WriteLine("My Domain is :{0}",myDomain.FriendlyName);

            //Console.WriteLine("当前程序域的名称是:{0}",AppDomain.CurrentDomain.FriendlyName);

            ////卸载
            //AppDomain.Unload(myDomain);

            ////试图访问被卸载的应用程序域
            //try
            //{
            //    Console.WriteLine("My Domain is :{0}", myDomain.FriendlyName);
            //}
            //catch (CannotUnloadAppDomainException ex)
            //{

            //    throw ex;
            //}

            //获取当前应用程序域的相关信息

            //1、获取上下文
            ActivationContext tContext = AppDomain.CurrentDomain.ActivationContext;
            //2、获取应用程序标识
            ApplicationIdentity iIdentity = AppDomain.CurrentDomain.ApplicationIdentity;
            //3、获取当前应用程序的信任级别
            System.Security.Policy.ApplicationTrust tTrust = AppDomain.CurrentDomain.ApplicationTrust;
            //4、获取程序集目录 由程序集冲突解决程序来探测程序集 --静态
            string tDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //动态目录
            string tDynamicDirectory = AppDomain.CurrentDomain.DynamicDirectory;
            //获取相对于基目录的路径 探测专用程序集
            string tRPath = AppDomain.CurrentDomain.RelativeSearchPath;

            //5、获取应用程序域管理器
            AppDomainManager tDomainManager = AppDomain.CurrentDomain.DomainManager;
            //6、获取此应用程序相关联的Evident 用于安全策略的输入
            System.Security.Policy.Evidence tEvidence = AppDomain.CurrentDomain.Evidence;

            //标识应用程序域中的ID
            int tID = AppDomain.CurrentDomain.Id;
            //标识应用程序是否完全信任级别
            bool blIsFullyTrust = AppDomain.CurrentDomain.IsFullyTrusted;
            //获取一个值表示是否拥有对加载当前应用程序域的所有程序集的权限集合
            bool isHomoGenous = AppDomain.CurrentDomain.IsHomogenous;
            //安装信息
            AppDomainSetup taSetup = AppDomain.CurrentDomain.SetupInformation;
            //是否影响复制
            bool tsCopyFile = AppDomain.CurrentDomain.ShadowCopyFiles;
            
            Console.WriteLine(taSetup);

            Console.Read();
        }
    }
}
