
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
        
        public DbSet<TicketEntity> Tickets { get; set; }
        
        public DbSet<UserSubscribeRef> SubscribedOnEventUsers { get; set; }
        public DbSet<EventTagRef> TaggedEvents { get; set; }
        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            if (Database.EnsureCreated())
            {
#if !RELEASE
                Setup();    
#endif
            }
        }

#if !RELEASE
        private void Setup()
        {
            var userEntry =  Users.Add(new UserEntity() {Login = "hello", Password = "world", Email = "hello@gmail.com",}); 
            Users.Add(new UserEntity() {Login = "test1", Password = "12345", Email = "hello2@gmail.com",});
            Users.Add(new UserEntity() {Login = "test2", Password = "12345", Email = "hello3@gmail.com",});

            var userId= userEntry.Entity.Id;
            
            var firstEvent = Events.Add(new EventEntity()
            {
                Name = "Мюзикл в большом театре",
                Description = "Самый громкий мюзикл в самом большом театре",
                OwnerUserId =  userId,
                StartSellingDate = DateTime.Now.AddDays(1),
                EndSellingDate = DateTime.Now.AddDays(3),
                PlayTime = DateTime.Now.AddDays(4)
            }).Entity;

            var theatreTag = Tags.Add(new TagEntity() {Name = "Театр"}).Entity;
            var musicalTag = Tags.Add(new TagEntity() {Name = "Мюзикл"}).Entity;

            TaggedEvents.Add(new EventTagRef {TagId = theatreTag.Id, EventId = firstEvent.Id});
            TaggedEvents.Add(new EventTagRef {TagId = musicalTag.Id, EventId = firstEvent.Id});
            
            
            SaveChanges();
        }
#endif
       
    }
}
