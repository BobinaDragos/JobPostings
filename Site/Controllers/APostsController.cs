using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Site.Data;
using Site.Models;

namespace Site.Controllers
{
    public class APostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public APostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: APosts
        public async Task<IActionResult> Index()
        {
            return View(await _context.APost.ToListAsync());
        }
        // GET: APosts/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }
        // GET: APosts/ShowSearchResult
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index",await _context.APost.Where(p=>p.PostDescription.Contains(SearchPhrase)).ToListAsync());
        }
        // GET: APosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aPost = await _context.APost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aPost == null)
            {
                return NotFound();
            }

            return View(aPost);
        }

        // GET: APosts/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: APosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PostTitle,PostDescription,PostTags")] APost aPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aPost);
        }

        // GET: APosts/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aPost = await _context.APost.FindAsync(id);
            if (aPost == null)
            {
                return NotFound();
            }
            return View(aPost);
        }

        // POST: APosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostTitle,PostDescription,PostTags")] APost aPost)
        {
            if (id != aPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!APostExists(aPost.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aPost);
        }

        // GET: APosts/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aPost = await _context.APost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aPost == null)
            {
                return NotFound();
            }

            return View(aPost);
        }

        // POST: APosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aPost = await _context.APost.FindAsync(id);
            _context.APost.Remove(aPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool APostExists(int id)
        {
            return _context.APost.Any(e => e.Id == id);
        }
    }
}
