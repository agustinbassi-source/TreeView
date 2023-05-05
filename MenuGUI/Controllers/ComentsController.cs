using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MenuGUI.Data;
using MenuGUI.Models;

namespace MenuGUI.Controllers
{
    public class ComentsController : Controller
    {
        private readonly MenuGUIContext _context;

        public ComentsController(MenuGUIContext context)
        {
            _context = context;
        }

        // GET: Coments
        public async Task<IActionResult> Index(int? id)
        {
            var response = _context.Coment.Where(x => x.MenuId == id).ToList();

            response.ForEach(x => x.UsrComent = x.UsrComent == null ? "" : x.UsrComent.Replace(System.Environment.NewLine, "<br />"));

            return View(response);
        }

        // GET: Coments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Coment == null)
            {
                return NotFound();
            }

            var coment = await _context.Coment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coment == null)
            {
                return NotFound();
            }

            coment.UsrComent = coment.UsrComent == null ? "" : coment.UsrComent.Replace(System.Environment.NewLine, "<br />");

            return View(coment);
        }

        // GET: Coments/Create
        public IActionResult Create()
        {
            return View(new Coment() { Date = DateTime.Now });
        }

        // POST: Coments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Link,Usr,UsrComent,Date,MenuId")] Coment coment)
        {
            if (ModelState.IsValid)
            {
                coment.Usr = User.Identities.FirstOrDefault().Name;
                coment.Date = DateTime.Now;
                _context.Add(coment);
                await _context.SaveChangesAsync();
                return Redirect("/Coments/Index/" + coment.MenuId);
            }
            return View(coment);
        }

        // GET: Coments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Coment == null)
            {
                return NotFound();
            }

            var coment = await _context.Coment.FindAsync(id);
            if (coment == null)
            {
                return NotFound();
            }
            return View(coment);
        }

        // POST: Coments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Link,Usr,UsrComent,Date,MenuId")] Coment coment)
        {
            if (id != coment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentExists(coment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/Coments/Index/" + coment.MenuId);
            }
            return View(coment);
        }

        // GET: Coments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Coment == null)
            {
                return NotFound();
            }

            var coment = await _context.Coment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coment == null)
            {
                return NotFound();
            }

            return View(coment);
        }

        // POST: Coments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Coment == null)
            {
                return Problem("Entity set 'MenuGUIContext.Coment'  is null.");
            }
            var coment = await _context.Coment.FindAsync(id);
            if (coment != null)
            {
                _context.Coment.Remove(coment);
            }

            var idMenu = coment.MenuId;

            await _context.SaveChangesAsync();
            return Redirect("/Coments/Index/" + idMenu);
        }

        private bool ComentExists(int id)
        {
            return (_context.Coment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
