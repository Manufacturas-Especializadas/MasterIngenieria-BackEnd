using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Master> Masters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Master>(entity =>
            {
                entity.ToTable("MasterIndustrial");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ParentPartNumber).HasColumnName("parentPartNumber");
                entity.Property(e => e.ChildPartNumber).HasColumnName("childPartNumber");
                entity.Property(e => e.ExternalDiameter).HasColumnName("externalDiameter");
                entity.Property(e => e.WallThickness).HasColumnName("wallThickness");
                entity.Property(e => e.Development).HasColumnName("development");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Type).HasColumnName("type");
                entity.Property(e => e.Family).HasColumnName("family");
                entity.Property(e => e.Client).HasColumnName("client");
                entity.Property(e => e.Line).HasColumnName("line");
                entity.Property(e => e.PartOfPurchase).HasColumnName("partOfPurchase");
                entity.Property(e => e.QuantityXQuantity).HasColumnName("quantityXQuantity");
                entity.Property(e => e.Operation).HasColumnName("operation");
                entity.Property(e => e.Sequence).HasColumnName("sequence");
                entity.Property(e => e.ProcessComments).HasColumnName("processComments");
                entity.Property(e => e.MajorSetup).HasColumnName("majorSetup");
                entity.Property(e => e.MinorSetup).HasColumnName("minorSetup");
                entity.Property(e => e.PzsHr).HasColumnName("pzsHr");
                entity.Property(e => e.Verification).HasColumnName("verification");

                entity.Property(e => e.Oper)
                    .HasColumnType("decimal(10, 3)")
                    .HasColumnName("oper");

                entity.Property(e => e.OperSetup)
                    .HasColumnType("decimal(10, 3)")
                    .HasColumnName("operSetup");

                entity.Property(e => e.TCiclo)
                    .HasColumnType("decimal(10, 3)")
                    .HasColumnName("tCiclo");
            });
        }
    }
}
