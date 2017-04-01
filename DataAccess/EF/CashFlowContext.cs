using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CashFlow.DataAccess.EF
{
    public partial class CashFlowContext : DbContext
    {
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountBalance> AccountBalance { get; set; }
        public virtual DbSet<DictAccountType> DictAccountType { get; set; }
        public virtual DbSet<DictTransferPeriod> DictTransferPeriod { get; set; }
        public virtual DbSet<Transfer> Transfer { get; set; }
        public virtual DbSet<TransferSchema> TransferSchema { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Data Source=B-PC;Initial Catalog=CashFlow;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.AccountTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Account_Dict.AccountType");
            });

            modelBuilder.Entity<AccountBalance>(entity =>
            {
                entity.Property(e => e.Balance).HasColumnType("decimal");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountBalance)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AccountBalance_Account");
            });

            modelBuilder.Entity<DictAccountType>(entity =>
            {
                entity.ToTable("Dict.AccountType");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DictTransferPeriod>(entity =>
            {
                entity.ToTable("Dict.TransferPeriod");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TransferDate).HasColumnType("datetime");

                entity.HasOne(d => d.AccountFrom)
                    .WithMany(p => p.TransferAccountFrom)
                    .HasForeignKey(d => d.AccountFromId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Transfer_AccountFrom");

                entity.HasOne(d => d.AccountTo)
                    .WithMany(p => p.TransferAccountTo)
                    .HasForeignKey(d => d.AccountToId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Transfer_AccountTo");
            });

            modelBuilder.Entity<TransferSchema>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal");

                entity.Property(e => e.LastTransferDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TransferEndDate).HasColumnType("date");

                entity.Property(e => e.TransferStartDate).HasColumnType("date");

                entity.Property(e => e.TransferTime).HasDefaultValueSql("'00:00:00'");

                entity.HasOne(d => d.AccountFrom)
                    .WithMany(p => p.TransferSchemaAccountFrom)
                    .HasForeignKey(d => d.AccountFromId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TransferSchema_AccountFrom");

                entity.HasOne(d => d.AccountTo)
                    .WithMany(p => p.TransferSchemaAccountTo)
                    .HasForeignKey(d => d.AccountToId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TransferSchema_AccountTo");

                entity.HasOne(d => d.TransferPeriodNavigation)
                    .WithMany(p => p.TransferSchema)
                    .HasForeignKey(d => d.TransferPeriod)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TransferSchema_Dict.TransferPeriod1");
            });
        }
    }
}