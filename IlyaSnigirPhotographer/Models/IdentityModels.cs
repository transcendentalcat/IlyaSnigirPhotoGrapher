using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IlyaSnigirPhotographer.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class Album
    {
        public int AlbumID { get; set; }
        [Required]
        [DisplayName("Название")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Описание")]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Дата создания")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }

    public class Photo
    {
        public int PhotoID { get; set; }

        [Required]
        [DisplayName("Название")]
        public string Title { get; set; }

        [DisplayName("Picture")]
        [MaxLength]
        public byte[] PhotoFile { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Описание")]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Дата создания")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        public int AlbumID { get; set; }

        public virtual Album Album { get; set; }
    }

    public class Review
    {
        public virtual int Id { get; set; }
        [Required]
        [DisplayName("Имя")]
        public virtual string Name { get; set; }

        [Required]
        [DisplayName("Отзыв")]
        public virtual string Message { get; set; }

        [DisplayName("Дата")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public virtual DateTime DateAdded { get; set; }
    }

    public class PortfolioDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public PortfolioDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        static PortfolioDbContext()
        {
            Database.SetInitializer<PortfolioDbContext>(new PortfolioInitializer());
        }
        public static PortfolioDbContext Create()
        {
            return new PortfolioDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>()
                .HasOptional<Album>(a => a.Album)
                .WithMany()
                .WillCascadeOnDelete(true);
        }
    }

    public class PortfolioInitializer : CreateDatabaseIfNotExists<PortfolioDbContext>
    {
        //This method puts sample data into the database
        protected override void Seed(PortfolioDbContext context)
        {
            base.Seed(context);


            var albums = new List<Album>
            {
                new Album
                {
                    Title = "Nature", CreatedDate = DateTime.Today.AddDays(-7), Description = "Beauliful nature"
                },
                new Album
                {
                    Title = "Traveling", CreatedDate = DateTime.Now, Description = "Just travelling"
                }
            };
            albums.ForEach(a => context.Albums.Add(a));
            context.SaveChanges();

            //Create some photos
            var photos = new List<Photo>
            {
                new Photo {
                    Title = "Sample Photo 1",
                    Description = "This is the first sample photo in the Adventure Works photo application",
                    PhotoFile = LoadPhoto("\\Images\\1.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today.AddDays(-5),
                    AlbumID = 2
                },
                new Photo {
                    Title = "Sample Photo 2",
                    Description = "This is the second sample photo in the Adventure Works photo application",
                    PhotoFile = LoadPhoto("\\Images\\2.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today.AddDays(-14),
                    AlbumID = 1
                },
                new Photo {
                    Title = "Sample Photo 3",
                    Description = "This is the third sample photo in the Adventure Works photo application",
                    PhotoFile = LoadPhoto("\\Images\\3.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today.AddDays(-14),
                    AlbumID = 1
                },
                new Photo {
                    Title = "Sample Photo 6",
                    Description = "This is the sixth sample photo in the Adventure Works photo application",
                    PhotoFile = LoadPhoto("\\Images\\4.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today.AddDays(-11),
                    AlbumID = 2
                },
                new Photo {
                    Title = "Sample Photo 7",
                    Description = "This is the seventh sample photo in the Adventure Works photo application",
                    PhotoFile = LoadPhoto("\\Images\\5.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today.AddDays(-10),
                    AlbumID = 2

                },
                new Photo {
                    Title = "Sample Photo 8",
                    Description = "This is the eigth sample photo in the Adventure Works photo application",
                    PhotoFile = LoadPhoto("\\Images\\6.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today.AddDays(-9),
                    AlbumID = 2

                },
                new Photo {
                    Title = "Sample Photo 9",
                    Description = "This is the ninth sample photo in the Adventure Works photo application",
                    PhotoFile = LoadPhoto("\\Images\\7.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today.AddDays(-8),
                    AlbumID = 1

                },
                new Photo {
                    Title = "Sample Photo 10",
                    Description = "This is the tenth sample photo in the Adventure Works photo application",
                    PhotoFile = LoadPhoto("\\Images\\8.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today.AddDays(-7),
                    AlbumID = 1

                },
                new Photo {
                    Title = "Sample Photo 11",
                    Description = "This is the eleventh sample photo in the Adventure Works photo application",
                    PhotoFile = LoadPhoto("\\Images\\9.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today.AddDays(-5),
                    AlbumID = 1

                },
                new Photo {
                    Title = "Sample Photo 12",
                    Description = "This is the twelth sample photo in the Adventure Works photo application",
                    PhotoFile = LoadPhoto("\\Images\\10.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today.AddDays(-3),
                    AlbumID = 1

                },
                new Photo {
                    Title = "Sample Photo 13",
                    Description = "This is the thirteenth sample photo in the Adventure Works photo application",
                    PhotoFile = LoadPhoto("\\Images\\11.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today.AddDays(-1),
                    AlbumID = 1
                }
            };
            photos.ForEach(s => context.Photos.Add(s));
            context.SaveChanges();

        }


        private byte[] LoadPhoto(string path)
        {
            FileStream fileOnDisk = new FileStream(HttpRuntime.AppDomainAppPath + path, FileMode.Open);
            byte[] fileBytes;
            using (BinaryReader br = new BinaryReader(fileOnDisk))
            {
                fileBytes = br.ReadBytes((int)fileOnDisk.Length);
            }
            return fileBytes;
        }
    }
}