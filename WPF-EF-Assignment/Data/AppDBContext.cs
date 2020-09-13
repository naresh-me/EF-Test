using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_EF_Assignment.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
             : base("name=ConString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public virtual DbSet<ReagentLot> ReagentLots { get; set; }
        public virtual DbSet<ReagentBatch> ReagentBatchs { get; set; }
    }
    public class AppDbInitializer : DropCreateDatabaseAlways<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            IList<ReagentBatch> Batch = new List<ReagentBatch>();

            //Batch.Add(new ReagentBatch() { });

            context.ReagentBatchs.AddRange(Batch);

            base.Seed(context);
        }
    }
}
