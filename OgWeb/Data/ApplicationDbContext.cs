﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OgWeb.Models;

namespace OgWeb.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var admin = new IdentityRole("admin");
        admin.NormalizedName = "admin";

        var client = new IdentityRole("client");
        client.NormalizedName = "client";

        builder.Entity<IdentityRole>().HasData(admin, client);
    }
}
