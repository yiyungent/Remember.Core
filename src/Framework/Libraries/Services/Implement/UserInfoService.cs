using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Domain.Entities;
using Services.Core;
using Services.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implement
{
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        public async Task<IList<string>> UserHaveAuthKeys(int userId)
        {
            IList<string> authKeys = new List<string>();
            var authKeyCompare = new AuthKeyCompare();
            UserInfo userInfo = await this._repository.FindAsync(m => m.ID == userId);
            if (userInfo.Role_Users != null && userInfo.Role_Users.Count >= 1)
            {
                var roleInfos = userInfo.Role_Users.Select(m => m.RoleInfo);
                foreach (var role in roleInfos)
                {
                    var role_funcs = role.Role_Permissions;
                    if (role_funcs != null && role_funcs.Count >= 1)
                    {
                        var funcs = role_funcs.Select(m => m.PermissionInfo);
                        foreach (var func in funcs)
                        {
                            if (!authKeys.Contains(func.AuthKey, authKeyCompare))
                            {
                                authKeys.Add(func.AuthKey);
                            }
                        }
                    }
                }
            }

            return authKeys;
        }

        public async Task<bool> HasAuth(int userId, string authKey)
        {
            IList<string> haveAuthKeyList = await UserHaveAuthKeys(userId);
            if (haveAuthKeyList.Contains(authKey, new AuthKeyCompare()))
            {
                return true;
            }

            return false; ;
        }
    }
}
