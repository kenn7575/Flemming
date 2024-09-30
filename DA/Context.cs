﻿using Microsoft.EntityFrameworkCore;

using BL;

namespace DA
{
    public class MailContext : DbContext
    {

        //public MailContext(DbContextOptions<MailContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=PC5577;Database=Flemming;Trusted_Connection=True;TrustServerCertificate=True");

        public DbSet<CategorizedEmail> CategorizedEmails { get; set; }
        public DbSet<PilotageEmail> PilotageEmails { get; set; }
    }
}





