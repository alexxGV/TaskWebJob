using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskWebJob.Model;

namespace TaskWebJob.Data
{
    public class WebJobContext :DbContext
    {
        public WebJobContext(DbContextOptions<WebJobContext> options) : base(options) { }
        public DbSet<WebJob> WebJobs { get; set; }
    }
}
