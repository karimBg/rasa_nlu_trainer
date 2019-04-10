using Microsoft.EntityFrameworkCore;
using rasa_nlu_storage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rasa_nlu_db_storage.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<NluModel> NluModel { get; set; }
        public DbSet<RasaNLUData> RasaNLUDatas { get; set; }
        public DbSet<CommonExample> CommonExamples { get; set; }
        public DbSet<Entity> Entities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NluModel>()
                .HasData(new NluModel() { Id = 1 });

            modelBuilder.Entity<RasaNLUData>()
                .HasData(new RasaNLUData()
                {
                    Id = 1,
                    NluModelId = 1
                });

            modelBuilder.Entity<CommonExample>()
                .HasData(new
                {
                    Id = 1,
                    RasaNLUDataId = 1,
                    Text = "Hello",
                    Intent = "Greeting"
                });

            modelBuilder.Entity<Entity>()
                .HasData(new
                {
                    Id = 1,
                    CommonExampleId = 1,
                    Start = 1,
                    End = 20,
                    Value = "Karim",
                    EntityName = "Name"
                }, 
                new
                {
                    Id = 2,
                    CommonExampleId = 1,
                    Start = 9,
                    End = 26,
                    Value = "Monastir",
                    EntityName = "Place"
                }
                );
        }
    }
}
