using IlyaSnigirPhotographer.Models;
using System;
using System.Collections.Generic;
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
                return View(albums);
            }
            else
            {
                ViewBag.Message = "Альбомы не найдены";
                return View("NotFound");
            }
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

        public ActionResult DeletePhoto(int id)
        {
            Photo photo = db.Photos.Find(id);
            int AlbumId = photo.AlbumID;
            if (photo != null)
            {
                return PartialView("DeletePhoto", photo);
            }
            return View("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePhotoRecord(int id)
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