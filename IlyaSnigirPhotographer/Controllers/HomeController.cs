using IlyaSnigirPhotographer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace IlyaSnigirPhotographer.Controllers
{
    public class HomeController : Controller
    {
        PortfolioDbContext db = null;

        public HomeController()
        {
            db = new PortfolioDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Contacts()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Portfolio()
        {
            var albums = db.Albums;
            //var firstPhotos = albums.Select(a => new {a.AlbumID, a.Photos.FirstOrDefault().PhotoID});
            //var albumsWithCover = albums.Where(a => a.CoverPhoto == null).Select()
            //foreach (var album in albums)
            //{
            //    if (album.CoverPhoto == null /*&& album.Photos.Count != 0*/)
            //    {
            //        album.CoverPhoto = album.Photos.FirstOrDefault().PhotoID;
            //    }
            //}

            return View(albums);
        }

        public ActionResult Photos(int id)
        {
            var photos = db.Photos.Where(p => p.AlbumID == id);

            return View(photos);
        }

        public ActionResult BeforeAfter()
        {
            ViewBag.Message = "Your contact page.";

            return View("Before_After");
        }

        public ActionResult Services()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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

        public ActionResult GetIndexAvatarPhoto()
        {
            SystemContent sc = db.SystemContents.FirstOrDefault();
            byte[] img = sc.IndexAvatarPhoto;
            return File(img, "image/jpg");
            
        }

        public ActionResult Display(int id)
        {
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }

            return PartialView("DisplayAjax", photo);
        }
    }
}