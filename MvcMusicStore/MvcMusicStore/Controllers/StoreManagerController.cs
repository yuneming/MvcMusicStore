using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;
using EntityState = System.Data.Entity.EntityState;

namespace MvcMusicStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StoreManagerController : Controller
    {
        //
        // GET: /StoreManager/
        MusicStoreEntities storeDB = new MusicStoreEntities();

        public ActionResult Index()
        {
            var albums = storeDB.Albums.Include("Genre").Include("Artist");
            return View(albums.ToList());
            
        }

        //
        // GET: /StoreManager/Details/5

        public ActionResult Details(int id)
        {
            Album album = storeDB.Albums.Find(id);
            return View(album);
        }

        //
        // GET: /StoreManager/Create

        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(storeDB.Genres,"GenreId","Name");
            ViewBag.ArtistId = new SelectList(storeDB.Artists,"ArtistId","Name");
            return View();
        } 

        //
        // POST: /StoreManager/Create

        [HttpPost]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)     //检查表单的数据是否通过了验证规则
            {
                storeDB.Albums.Add(album);
                storeDB.SaveChanges();
                return RedirectToAction("Index");
            }
            //如果表单没有通过验证，重新显示带有验证提示信息的表单
            ViewBag.GenreId = new SelectList(storeDB.Genres,"GenreId","Name",album.AlbumId);
            ViewBag.ArtistId = new SelectList(storeDB.Artists,"ArtistId","Name",album.ArtistId);
            return View(album);
        }
        
        //
        // GET: /StoreManager/Edit/5
 
        public ActionResult Edit(int id)
        {
            Album album = storeDB.Albums.Find(id);
            ViewBag.GenreId = new SelectList(storeDB.Genres, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(storeDB.Artists, "ArtistId", "Name", album.ArtistId);
            return this.View(album);
        }

        //
        // POST: /StoreManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                storeDB.Entry(album).State = EntityState.Modified; 
                storeDB.SaveChanges();
                return RedirectToAction("Index");
                //bool saveFailed;
                //do
                //{
                //    saveFailed = false;

                //    try
                //    {
                //        storeDB.SaveChanges();
                //    }
                //    catch (DbUpdateConcurrencyException ex)
                //    {
                //        saveFailed = true;

                //        // Update the values of the entity that failed to save from the store 
                //        ex.Entries.Single().Reload();
                //    }

                //} while (saveFailed); 
                
            }
            ViewBag.GenreId = new SelectList(storeDB.Genres, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(storeDB.Artists, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }

        //
        // GET: /StoreManager/Delete/5
 
        public ActionResult Delete(int id)
        {
            Album album = storeDB.Albums.Find(id);
            return View(album);
        }

        //
        // POST: /StoreManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = storeDB.Albums.Find(id);
            storeDB.Albums.Remove(album);
            storeDB.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
