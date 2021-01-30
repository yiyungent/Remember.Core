using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Services.Core;

namespace Services.Interface
{
    public partial interface IUserInfoService :  IService<UserInfo>
    {
        Task<IList<string>> UserHaveAuthKeys(int userId);

        Task<bool> HasAuth(int userId, string authKey);
    }
}
