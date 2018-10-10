using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HowToMockAsyncAction.Model;

namespace HowToMockAsyncAction.Models
{
    public class HowToMockAsyncActionContext : DbContext
    {
        public HowToMockAsyncActionContext (DbContextOptions<HowToMockAsyncActionContext> options)
            : base(options)
        {
        }

        public DbSet<HowToMockAsyncAction.Model.Person> Person { get; set; }
    }
}
