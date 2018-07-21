using IlyaSnigirPhotographer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IlyaSnigirPhotographer.Controllers
{
    public class PhotoController : Controller
    {
        PortfolioDbContext db = new PortfolioDbContext();
        const int recordsPerPage = 4;

        public ActionResult Index(int? id, int albumId)
        {
            ViewBag.AlbumId = albumId;
            ViewBag.AlbTitle = db.Albums.Find(albumId).Title;

            var page = id ?? 0;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Photos", GetPaginatedPhotos(albumId, page));
            }

            return View("Index", db.Photos.Where(p => p.AlbumID == albumId).Take(recordsPerPage));
        }

        public ActionResult Product(int? id, int albumId)
        {
            var page = id ?? 0;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Photos", GetPaginatedPhotos(page, albumId));
            }

            return View("Index", db.Photos.Take(recordsPerPage));
        }

        private List<Photo> GetPaginatedPhotos(int albumId, int page = 1)
        {
            var skipRecords = page * recordsPerPage;

            return db.Photos.Where(p => p.AlbumID == albumId)
                .OrderBy(x => x.CreatedDate)
                .Skip(skipRecords)
                .Take(recordsPerPage).ToList();
        }
    }
}