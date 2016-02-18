using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class StoreController : Controller
    {
        //
        // GET: /Store/
        MusicStoreEntities storeDB = new MusicStoreEntities();
        public ActionResult Index()
        {
            var genres = storeDB.Genres.ToList();
            return this.View(genres);
        }

        public ActionResult Browse(string genre)
        {
            //string message = HttpUtility.HtmlEncode("Store.Browse,Genre = " + genre);//将文本字符串转换为 HTML 编码的字符串,防止脚本攻击
            ////< 转换为&lt > 转换为&gt & 转换为&amp " 转换为&quot;
            //return message;
            //var genreModel = new Genre{Name = genre};
            //return View(genreModel);
            var genreModel = storeDB.Genres.Include("Albums").Single(g => g.Name == genre);

            return this.View(genreModel);

        }

        public ActionResult Details(int id)
        {
            //var album = new Album {Title = "Album" + id};
            //return View(album);
            var album = storeDB.Albums.Find(id);
            return this.View(album);
        }
        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var genres = storeDB.Genres.ToList();
            return PartialView(genres);
        }
    }
}
