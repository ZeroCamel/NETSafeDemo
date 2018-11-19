using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DynamicLoadAndUnloadAssembly.ServiceAgent
{
    public class ServiceManager<T> : IDisposable where T : class
    {
        /// <summary>
        /// 如果是泛型-值类型/引用类型 值类型=0有效 引用类型=null 有效
        /// 如果是值类型-判断是值类型还是结构类型
        /// 私有字段-返回泛型的默认值
        /// </summary>
        private T proxy = default(T);
        public T Proxy
        {
            get
            {
                //判断字段是否为空
                if (proxy == null)
                {
                    proxy = (T)InitProxy(AssemblyPlugs);
                }
                return proxy;
            }
        }

        private AppDomain ctorProxy = null;

        /// <summary>
        /// 私有字段-应用程序运行域容器
        /// </summary>
        public AppDomain CtorProxy
        {
            get { return ctorProxy; }
        }

        public ServiceManager()
        {
            if (proxy == null)
            {
                proxy = (T)InitProxy(AssemblyPlugs);
            }
        }

        /// <summary>
        /// 外挂插件程序集目录路径
        /// </summary>
        private string assemblyPlugs;

        public string AssemblyPlugs
        {
            get
            {
                assemblyPlugs = ConfigHelper.GetValue("PrivatePath");
                if (assemblyPlugs.Equals(""))
                {
                    assemblyPlugs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Plugins");
                }
                if (!Directory.Exists(assemblyPlugs))
                {
                    Directory.CreateDirectory(assemblyPlugs);
                }
                return assemblyPlugs;
            }

            set
            {
                assemblyPlugs = value;
            }
        }

        public void Dispose()
        {
            this.UnLoad();
        }


        public void UnLoad()
        {
            try
            {
                if (CtorProxy!=null)
                {
                    AppDomain.Unload(CtorProxy);
                    ctorProxy = null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private T InitProxy(string assemblyPlugs)
        {
            try
            {
                //影像复制功能
                //AppDomain.CurrentDomain.SetShadowCopyFiles();
                //string callingDomainName = Thread.GetDomain().FriendlyName;
                //Get And Display the full name of the exe assembly
                //string exeAssembly = Assembly.GetEntryAssembly().FullName;
                //Console.WriteLine(exeAssembly);

                #region 应用程序基本信息设置和构建
                //应用程序域新建
                AppDomainSetup ads = new AppDomainSetup();
                ads.ApplicationName = "shadow";

                //应用程序根目录
                ads.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
                //子目录（相对形式）在AppDomainSetup中加入外部程序集所在目录，多个目录用；分割
                ads.PrivateBinPath = assemblyPlugs;
                //设置缓存目录
                ads.CachePath = ads.ApplicationBase;
                //获取和设置影像复制是打开还是关闭
                ads.ShadowCopyFiles = "true";
                //获取和设置目录的名称，目录包含要影像复制的程序集
                ads.ShadowCopyDirectories = ads.ApplicationBase;
                
                ads.DisallowBindingRedirects = false;
                ads.DisallowCodeDownload = true;

                ads.ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

                Evidence evidence = AppDomain.CurrentDomain.Evidence;
                ctorProxy = AppDomain.CreateDomain("AD #2", evidence, ads);

                string assemblyName = Assembly.GetExecutingAssembly().FullName;

                Console.WriteLine("CtorProxy:" + Thread.GetDomain().FriendlyName);

                #endregion

                TransparentAgent factory = (TransparentAgent)ctorProxy.CreateInstanceAndUnwrap(assemblyName, typeof(TransparentAgent).FullName);
                Type meetType = typeof(T);
                string typeName = AssemblyHelper.CategoryInfo(meetType);

                object[] args = new object[0];
                string assemblyPath = ctorProxy.SetupInformation.PrivateBinPath;

                T obj = factory.Create<T>(assemblyPath, typeName, args);

                return obj;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
