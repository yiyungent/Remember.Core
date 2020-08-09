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


        }
    }
}
