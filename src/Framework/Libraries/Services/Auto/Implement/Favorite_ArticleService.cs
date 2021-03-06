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
    public partial class Favorite_ArticleService : BaseService<Favorite_Article>, IFavorite_ArticleService
    {
        private readonly IFavorite_ArticleRepository _repository;
        public Favorite_ArticleService(IFavorite_ArticleRepository repository) : base(repository)
        {
            this._repository = repository;
        }
    }
}
