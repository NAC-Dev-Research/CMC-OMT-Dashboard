using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CMCOMTDashboard.Models;

namespace CMCOMTDashboard.DAL
{
    public class CMCOMTDbContext : DbContext
    {
        public CMCOMTDbContext() : base(nameOrConnectionString: "CMCOMTDb") { }
        public virtual DbSet<Materials> Materials { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<Works> Works { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
        public virtual DbSet<Activities> Activity { get; set; }
        public virtual DbSet<CCActivities> CCActivity { get; set; }
        public virtual DbSet<Locations> Location { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
    }
}