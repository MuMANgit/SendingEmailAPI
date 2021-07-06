using Microsoft.EntityFrameworkCore;
using SE.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SE.DAL.EF
{
    public class SubmissionResultContext : DbContext
    {
        public DbSet<SubmissionResult> SubmissionsResult { get; set; }
        public DbSet<MessageInfo> Messages { get; set; }

        public SubmissionResultContext (DbContextOptions<SubmissionResultContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubmissionResult>()
                .HasOne(p => p.Message)
                .WithMany(t => t.submissionResults)
                .HasForeignKey(p => p.MessageID);
        }
    }
}
