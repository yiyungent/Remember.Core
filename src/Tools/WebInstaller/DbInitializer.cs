using System;
using System.Collections.Generic;
using System.Text;
using Repositories.Core;

namespace WebInstaller
{
    public static class DbInitializer
    {
        public static void Initialize(RemDbContext context)
        {
            context.Database.EnsureCreated();

            int userCount = ImportUserData.Import(context);
            Console.WriteLine($"导入 {userCount} 条用户数据");
            int articleCount = ImportArticleData.Import(context);
            Console.WriteLine($"导入 {articleCount} 条文章数据");

        }
    }
}
