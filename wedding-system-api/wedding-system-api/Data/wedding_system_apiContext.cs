using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wedding_system_api.Models;

namespace wedding_system_api.Data
{
    public class wedding_system_apiContext : DbContext
    {
        public wedding_system_apiContext (DbContextOptions<wedding_system_apiContext> options)
            : base(options)
        {
        }

        public DbSet<wedding_system_api.Models.presentsModel> presentsModel { get; set; } = default!;
    }
}
