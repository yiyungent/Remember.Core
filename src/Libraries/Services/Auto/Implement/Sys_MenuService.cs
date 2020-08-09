// Code generated by a template
// Project: remember
// https://github.com/yiyungent/remember
// Author: yiyun <yiyungent@gmail.com>
// LastUpadteTime: 2020-08-10 12:36:29
using Domain.Entities;
using Repositories.Interface;
using Services.Core;
using Services.Interface;

namespace Services.Implement
{
    public partial class Sys_MenuService : BaseService<Sys_Menu>, ISys_MenuService
    {
        private readonly ISys_MenuRepository _repository;
        public Sys_MenuService(ISys_MenuRepository repository) : base(repository)
        {
            this._repository = repository;
        }
    }
}
