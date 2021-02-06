using ColoredLive.Core.Entities;
using ColoredLive.Core.RefEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColoredLive.DAL
{
    public class AppDbContext : DbContext
    {

        #region Entity Tables
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserSettingEntity> UserSettings { get; set; }
        public DbSet<RecommendationEntity> Recomendations { get; set; }
        public DbSet<RecommendationEventEntity> RecommendationEvents { get; set; }
        public DbSet<RecommendationEventTagEntity> RecommendationEventTagEntities { get; set; }
        public DbSet<RecommendationTagEntity> RecommendationTagEntities { get; set; }
        public DbSet<CompanyEntity> Compnanies { get; set; }
        
        #endregion

        #region Ref Tables
        public DbSet<RecommendationEventRefTagEntity> EventTagRefs { get; set; }
        public DbSet<RecommendationRefTagEntity> RecommendationTagRefs { get; set; }
        #endregion

        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
            Database.EnsureCreated();
        }
    }
}
