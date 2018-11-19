using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Permissions;

namespace CustomPerssionDemo
{
    [AttributeUsage(AttributeTargets.All,AllowMultiple =true)]
    public class CodeAccessPermissionAttribute : CodeAccessSecurityAttribute
    {
        bool unrestricated = false;

        public CodeAccessPermissionAttribute(SecurityAction action) : base(action)
        {
        }

        public  bool Unrestricated
        {
            get
            {
                return unrestricated;
            }

            set
            {
                unrestricated = value;
            }
        }

        public override IPermission CreatePermission()
        {
            if (Unrestricated)
            {
                return new CustomPerssion(PermissionState.Unrestricted);
            }
            else
            {
                return new CustomPerssion(PermissionState.None);
            }
        }
    }
}
