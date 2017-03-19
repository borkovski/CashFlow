using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CashFlow.DataAccess.EF
{
    public partial class CashFlowContext : DbContext
    {
        public static string ConnectionString { get; set; }
        public virtual DbSet<DictDirection> DictDirection { get; set; }
        public virtual DbSet<DictRepeatPeriod> DictRepeatPeriod { get; set; }
        public virtual DbSet<Transfer> Transfer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DictDirection>(entity =>
            {
                entity.ToTable("dict.Direction");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DictRepeatPeriod>(entity =>
            {
                entity.ToTable("dict.RepeatPeriod");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.FinishDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Direction)
                    .WithMany(p => p.Transfer)
                    .HasForeignKey(d => d.DirectionId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Transfer_dict.Direction");

                entity.HasOne(d => d.RepeatPeriod)
                    .WithMany(p => p.Transfer)
                    .HasForeignKey(d => d.RepeatPeriodId)
                    .HasConstraintName("FK_Transfer_dict.RepeatPeriod");
            });
        }
    }
}