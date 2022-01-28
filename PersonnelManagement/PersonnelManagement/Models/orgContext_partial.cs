using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PersonnelManagement.Models
{
    public partial class orgContext : DbContext
    {
        public orgContext(DbContextOptions<orgContext> options) : base(options) { }
    }
}
