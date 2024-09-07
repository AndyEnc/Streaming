using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Series> Series { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Gender> Genders { get; set; }  
        public DbSet<SeriesGender> SeriesGenders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region tables
            modelBuilder.Entity<Series>().ToTable("Series");
            modelBuilder.Entity<Producer>().ToTable("Producers");
            modelBuilder.Entity<Gender>().ToTable("Genders");
            modelBuilder.Entity<SeriesGender>().ToTable("SeriesGenders");

            #endregion

            #region primaryKey
            modelBuilder.Entity<Series>().HasKey(s => s.Id);
            modelBuilder.Entity<Producer>().HasKey(p => p.Id);
            modelBuilder.Entity<Gender>().HasKey(G => G.Id);

            modelBuilder.Entity<SeriesGender>().HasKey(s => new { s.SerieId, s.GenderId });
            #endregion

            #region relationship
            modelBuilder.Entity<Producer>()
                .HasMany<Series>(s => s.SerieProducerList)
                .WithOne(s => s.Producer)
                .HasForeignKey(s => s.ProducerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SeriesGender>()
                .HasOne(sg => sg.Serie)
                .WithMany(s => s.SeriesGenderList)
                .HasForeignKey(s => s.SerieId);

            modelBuilder.Entity<SeriesGender>()
                .HasOne(sg => sg.Gender)
                .WithMany(g => g.GendersSeriesList)
                .HasForeignKey(g => g.GenderId);
            #endregion

            #region producer
            modelBuilder.Entity<Producer>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            #endregion

            #region Series
            modelBuilder.Entity<Series>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
            #endregion
        }
    }
}
