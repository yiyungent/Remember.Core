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
    public partial class FunctionInfoService : BaseService<FunctionInfo>, IFunctionInfoService
    {
        private readonly IFunctionInfoRepository _repository;
        public FunctionInfoService(IFunctionInfoRepository repository) : base(repository)
        {
            this._repository = repository;
        }
    }
}
