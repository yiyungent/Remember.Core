// Code generated by a template
// Project: Remember
// https://github.com/yiyungent/Remember
// Author: yiyun <yiyungent@gmail.com>
// LastUpadteTime: 2020-08-10 12:36:44
using Domain.Entities;
using Repositories.Core;
using Repositories.Interface;

namespace Repositories.Implement
{
    public partial class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        private readonly RemDbContext _context;

        public CommentRepository(RemDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}