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
    public partial class Comment_LikeService : BaseService<Comment_Like>, IComment_LikeService
    {
        private readonly IComment_LikeRepository _repository;
        public Comment_LikeService(IComment_LikeRepository repository) : base(repository)
        {
            this._repository = repository;
        }
    }
}
