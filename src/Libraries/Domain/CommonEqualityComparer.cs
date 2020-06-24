using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class Sys_Menu_Compare : IEqualityComparer<Sys_Menu>
    {
        public bool Equals(Sys_Menu x, Sys_Menu y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            return x.ID == y.ID;
        }

        public int GetHashCode(Sys_Menu obj)
        {
            throw new NotImplementedException();
        }
    }

    public class FunctionInfo_Compare : IEqualityComparer<FunctionInfo>
    {
        public bool Equals(FunctionInfo x, FunctionInfo y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            return x.ID == y.ID;
        }

        public int GetHashCode(FunctionInfo obj)
        {
            throw new NotImplementedException();
        }
    }

    #region 权限(操作)键相等比较器
    public class AuthKeyCompare : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return x.ToLower() == y.ToLower();
        }

        public int GetHashCode(string obj)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region 角色相等比较器
    public class RoleInfoEqualityComparer : IEqualityComparer<RoleInfo>
    {
        public bool Equals(RoleInfo x, RoleInfo y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            return x.ID == y.ID;
        }

        public int GetHashCode(RoleInfo obj)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
