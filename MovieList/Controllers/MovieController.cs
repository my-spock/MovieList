using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieList.Models;

namespace MovieList.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieContext _context;

        public MovieController(MovieContext context)
        {
            _context = context;
        }

        // View add movie
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            //ViewBag.Genres = _context.Genres.OrderBy(g => g.Name).ToList();
            return View("Edit", new Movie());
        }

        //View edit movie
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            //ViewBag.Genres = _context.Genres.OrderBy(g => g.Name).ToList();
            var movie = _context.Movies.Find(id);
            return View(movie);
        }

        //submit action for save and edit movie
        [HttpPost]
        public IActionResult Save(Movie movie)
        {
            if (ModelState.IsValid)
            {
                if (movie.MovieId == 0)
                    _context.Movies.Add(movie);
                else
                    _context.Movies.Update(movie);

                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Action = (movie.MovieId == 0) ? "Add" : "Edit";
                //ViewBag.Genres = _context.Genres.OrderBy(g => g.Name).ToList();
                return View(movie);
            }
        }

        //view delete movie
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.Find(id);
            return View(movie);
        }

        //submit delete movie
        [HttpPost]
        public IActionResult Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
