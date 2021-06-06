
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ColoredLive.Core.Entities;
using ColoredLive.Core.RefEntities;

namespace ColoredLive.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<EventEntity> Events { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<TicketEntity> Tickets { get; set; }
        
        public DbSet<UserRoleRef> UserRoleRefs { get; set; }
        public DbSet<UserSubscribeRef> SubscribedOnEventUsers { get; set; }
        public DbSet<EventTagRef> TaggedEvents { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        
    }
}
