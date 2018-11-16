using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class MenuCompare : IEqualityComparer<AdminMenu>
    {
        public bool Equals(AdminMenu x, AdminMenu y)
        {
            return (x.Id == y.Id);
        }

        public int GetHashCode(AdminMenu obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
