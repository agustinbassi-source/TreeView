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
    public class MenuGuiItemsControllerNo : Controller
    {
        private readonly MenuGUIContext _context;

        public MenuGuiItemsControllerNo(MenuGUIContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Tree()
        {
            return View();
        }

        // GET: MenuGuiItems
        public async Task<IActionResult> Index()
        {
            var data = _context.MenuGuiItem;
            var some = await _context.MenuGuiItem.ToListAsync();

            return data != null ?
                        View(some) :
                        Problem("Entity set 'MenuGUIContext.MenuGuiItem'  is null.");
        }

        // GET: MenuGuiItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MenuGuiItem == null)
            {
                return NotFound();
            }

            var menuGuiItem = await _context.MenuGuiItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuGuiItem == null)
            {
                return NotFound();
            }

            return View(menuGuiItem);
        }

        // GET: MenuGuiItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuGuiItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,I18n,Title,OrderMenu,ParentId,MenuId")] MenuGuiItem menuGuiItem)
        {
            if (ModelState.IsValid)
            {
                if (menuGuiItem.MenuId == 0)
                {
                    throw new Exception("Menu Item Id == 0");
                }

                _context.Add(menuGuiItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuGuiItem);
        }

        // GET: MenuGuiItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MenuGuiItem == null)
            {
                return NotFound();
            }

            var menuGuiItem = await _context.MenuGuiItem.FindAsync(id);
            if (menuGuiItem == null)
            {
                return NotFound();
            }
            return View(menuGuiItem);
        }

        // POST: MenuGuiItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,I18n,OrderMenu,ParentId")] MenuGuiItem menuGuiItem)
        {
            if (id != menuGuiItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuGuiItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuGuiItemExists(menuGuiItem.Id))
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
            return View(menuGuiItem);
        }

        // GET: MenuGuiItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MenuGuiItem == null)
            {
                return NotFound();
            }

            var menuGuiItem = await _context.MenuGuiItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuGuiItem == null)
            {
                return NotFound();
            }

            return View(menuGuiItem);
        }

        // POST: MenuGuiItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MenuGuiItem == null)
            {
                return Problem("Entity set 'MenuGUIContext.MenuGuiItem'  is null.");
            }
            var menuGuiItem = await _context.MenuGuiItem.FindAsync(id);
            if (menuGuiItem != null)
            {
                _context.MenuGuiItem.Remove(menuGuiItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuGuiItemExists(int id)
        {
            return (_context.MenuGuiItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
