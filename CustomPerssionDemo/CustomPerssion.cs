using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Permissions;

namespace CustomPerssionDemo
{
    [SerializableAttribute]
    //只有用SerizlizableAttribute标记的类才支持使用属性的声明式语法
    public sealed class CustomPerssion : CodeAccessPermission, IUnrestrictedPermission
    {
        private bool unrestricted;
        public CustomPerssion(PermissionState state)
        {
            if (state == PermissionState.Unrestricted)
            {
                unrestricted = true;
            }
            else
            {
                unrestricted = false;
            }
        }
        public override IPermission Copy()
        {
            CustomPerssion copy = new CustomPerssion(PermissionState.None);
            if (this.unrestricted)
            {
                copy.unrestricted = true;
            }
            else
            {
                copy.unrestricted = false;
            }
            return copy;
        }

        public override void FromXml(SecurityElement passedElem)
        {
            string element = passedElem.Attribute("Unrestricted");
            if (element != null)
            {
                this.unrestricted = Convert.ToBoolean(element);
            }
        }

        public override IPermission Intersect(IPermission target)
        {
            try
            {
                if (target == null)
                {
                    return null;
                }
                CustomPerssion PassedPermission = (CustomPerssion)target;
                if (!PassedPermission.IsUnrestricted())
                {
                    return PassedPermission;
                }
                else
                {
                    return this.Copy();
                }
            }
            catch (InvalidCastException)
            {

                throw new ArgumentException("Argument_wrongType", this.GetType().FullName);
            }
        }

        public override bool IsSubsetOf(IPermission target)
        {
            if (target == null)
            {
                return !this.unrestricted;
            }
            try
            {
                CustomPerssion passedPermission = (CustomPerssion)target;
                if (!this.unrestricted == passedPermission.unrestricted)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (InvalidCastException)
            {

                throw new ArgumentException("Argument_wrongType", this.GetType().FullName);
            }
        }

        public bool IsUnrestricted()
        {
            return unrestricted;
        }

        public override SecurityElement ToXml()
        {
            SecurityElement element = new SecurityElement("IPermission");
            Type type = this.GetType();
            StringBuilder AssemblyName = new StringBuilder(type.Assembly.ToString());
            AssemblyName.Replace('\"', '\'');
            element.AddAttribute("class", type.FullName + "," + AssemblyName);
            element.AddAttribute("version", "1");
            element.AddAttribute("Unrestricted", unrestricted.ToString());
            return element;
        }
    }
}
