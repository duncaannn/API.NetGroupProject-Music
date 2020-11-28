﻿using API.NetGroupProject_Music_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.NetGroupProject_Music_.Controllers
{
    public class MusicController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> MusicTESTSearchAsync(string data, string SearchBy)
        {
            if (SearchBy == "artist")
            {

                var result = await _dal.GetMusicAsync(data);

                return View("getsearch", result); //bart- put the name of the view for artist here
            }
            if (SearchBy == "album")
            {

                var result = await _dal.GetMusicAsync(data);

                return View("getsearch", result); //bart- put the name of the view for album here
            }
            if (SearchBy == "title")
            {

                var result = await _dal.GetMusicAsync(data);

                return View("getsearch", result); //bart- put the name of the view for song here
            }
            else
                return View("index");
        }
        private readonly MusicDAL _dal;
        private readonly MusicProjectDbContext _db = new MusicProjectDbContext();
        public MusicController(MusicDAL dal)
        {
            _dal = dal;
        }
        public async Task<IActionResult> Index()
        {
            //var result = await _dal.GetSearchAsync();
            return View();
        }

        

        public IActionResult Favorites()
        {
            return View(_db.Favorites.ToList());
        }

        [Authorize]
        public IActionResult UserFavorites()
        {
            return View(_db.UserFavorites.ToList());
        }


        [HttpPost]
        public IActionResult RemoveFavorite(Favorites f)
        {
            if (ModelState.IsValid)
            {
                _db.Favorites.Remove(f);
                _db.SaveChanges();
            }
            return RedirectToAction("/MusicFavorites");
        }

        [HttpPost]
        public IActionResult AddFavorite(Favorites f)
        {
            if (ModelState.IsValid)
            {
                _db.Favorites.Add(f);
                _db.SaveChanges();
            }
            return RedirectToAction("Music/Favorites");
        }
    }

}
