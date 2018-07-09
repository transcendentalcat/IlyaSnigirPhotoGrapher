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
        const int recordsPerPage = 8;

        public ActionResult Index(int? id)
        {
            var page = id ?? 0;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Photos", GetPaginatedPhotos(page));
            }

            return View("Index", db.Photos.Take(recordsPerPage));
        }

        public ActionResult Product(int? id)
        {
            var page = id ?? 0;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Photos", GetPaginatedPhotos(page));
            }

            return View("Index", db.Photos.Take(recordsPerPage));
        }

        private List<Photo> GetPaginatedPhotos(int page = 1)
        {
            var skipRecords = page * recordsPerPage;

            return db.Photos
                .OrderBy(x => x.CreatedDate)
                .Skip(skipRecords)
                .Take(recordsPerPage).ToList();
        }
    }
}