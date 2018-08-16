using IlyaSnigirPhotographer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace IlyaSnigirPhotographer.Controllers
{
    public class AdminController : Controller
    {
        PortfolioDbContext db = null;

        public AdminController()
        {
            db = new PortfolioDbContext();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View("Index", "Home");
        }

        public ActionResult ManageAlbums()
        {
            var albums = db.Albums;
            if(albums != null)
            {
                foreach (var album in albums)
                {
                    if (album.Cover.PhotoFile == null && album.Photos.Count != 0)
                    {
                        album.Cover.PhotoFile = album.Photos.FirstOrDefault().PhotoFile;
                        album.Cover.ImageMimeType = album.Photos.FirstOrDefault().ImageMimeType;
                    }
                }
                return View(albums);
            }
            
                ViewBag.Message = "Альбомы не найдены";
                return View("NotFound");           
        }

        [HttpGet]
        public ActionResult CreateAlbum()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAlbum(Album album)
        {
            album.CreatedDate = DateTime.Now;            
            db.Albums.Add(album);
            db.SaveChanges();

            return RedirectToAction("ManageAlbums");
        }

        [HttpGet]
        public ActionResult EditAlbum(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Album album = db.Albums.Find(id);
            if (album != null)
            {
                return View(album);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditAlbum(Album album)
        {
            Album editedAlbum = db.Albums.Find(album.AlbumID);
            editedAlbum.Title = album.Title;
            editedAlbum.Description = album.Description;
            db.Entry(editedAlbum).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ManageAlbums");
        }

        public ActionResult DeleteAlbum(int id)
        {
            Album album = db.Albums.Find(id);
            
            if (album != null)
            {
                List<Photo> photos = db.Photos.Where(p => p.AlbumID == id).ToList();
                db.Albums.Remove(album);
                db.SaveChanges();
            }
            return RedirectToAction("ManageAlbums");
        }

        public ActionResult ManagePhotos(int id)
        {
            ViewBag.AlbumId = id;
            ViewBag.AlbumTitle = db.Albums.Find(id).Title;
            var photos = db.Photos.Where(p => p.AlbumID == id);
            if (photos != null)
            {
                return View(photos);
            }
            else
            {
                ViewBag.Message = "Фотографии не найдены";
                return View("NotFound");
            }           
        }

        public ActionResult ManageReviews()
        {
            var reviews = db.Reviews;
            if (reviews != null)
            {
                return View(reviews);
            }
            else
            {
                ViewBag.Message = "Отзывы не найдены";
                return View("NotFound");
            }
        }

        public ActionResult AddPhoto(int id)
        {
            ViewBag.AlbumId = id;
            Photo newPhoto = new Photo();
            newPhoto.CreatedDate = DateTime.Today;

            return View(newPhoto);
        }

        [HttpPost]
        public ActionResult Create(Photo photo, HttpPostedFileBase Image)
        {
            photo.CreatedDate = DateTime.Today;

            if (!ModelState.IsValid)
            {
                return View("AddPhoto", photo);
            }
            else
            {
                if (Image != null)
                {
                    photo.ImageMimeType = Image.ContentType;
                    photo.PhotoFile = new byte[Image.ContentLength];
                    Image.InputStream.Read(photo.PhotoFile, 0, Image.ContentLength);
                }

                db.Photos.Add(photo);
                db.SaveChanges();
                return RedirectToAction("ManagePhotos", new { id = photo.AlbumID});
            }
        }

        [HttpGet]
        public ActionResult EditPhoto(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Photo photo = db.Photos.Find(id);
            if (photo != null)
            {
                return View(photo);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditPhoto(Photo photo)
        {
            Photo editedPhoto = db.Photos.Find(photo.PhotoID);
            editedPhoto.Title = photo.Title;
            editedPhoto.Description = photo.Description;
            db.Entry(editedPhoto).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ManagePhotos", new { id = editedPhoto.AlbumID });
        }
        
        public ActionResult DeletePhoto(int id)
        {
            Photo photo = db.Photos.Find(id);
            int AlbumId = photo.AlbumID;

            if (photo != null)
            {
                db.Photos.Remove(photo);
                db.SaveChanges();
            }
            return RedirectToAction("ManagePhotos", new { id = AlbumId });
        }

        public ActionResult SetCover(int id)
        {
            Photo photo = db.Photos.Find(id);
            Album album = photo.Album;
            album.Cover.ImageMimeType = photo.ImageMimeType;
            album.Cover.PhotoFile = photo.PhotoFile;

            return RedirectToAction("ManagePhotos", new { id = album.AlbumID });
        }

        [OutputCache(Duration = 600, Location = OutputCacheLocation.Server, VaryByParam = "id")]
        public FileContentResult GetImage(int id)
        {
            Photo photo = db.Photos.Find(id);

            if (photo != null)
            {
                return File(photo.PhotoFile, photo.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}