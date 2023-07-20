using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laboratory.Data;
using Laboratory.Models;

namespace Laboratory.Controllers
{
    public class ManagementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManagementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Managements
        public async Task<IActionResult> Index()
        {
              return _context.Management != null ? 
                          View(await _context.Management.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Management'  is null.");
        }

        // GET: Managements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Management == null)
            {
                return NotFound();
            }

            var management = await _context.Management
                .FirstOrDefaultAsync(m => m.Id == id);
            if (management == null)
            {
                return NotFound();
            }

            return View(management);
        }

        // GET: Managements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Managements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Value")] Management management)
        {
            if (ModelState.IsValid)
            {
                _context.Add(management);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(management);
        }

        // GET: Managements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Management == null)
            {
                return NotFound();
            }

            var management = await _context.Management.FindAsync(id);
            if (management == null)
            {
                return NotFound();
            }
            return View(management);
        }

        // POST: Managements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Value")] Management management)
        {
            if (id != management.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(management);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManagementExists(management.Id))
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
            return View(management);
        }

        // GET: Managements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Management == null)
            {
                return NotFound();
            }

            var management = await _context.Management
                .FirstOrDefaultAsync(m => m.Id == id);
            if (management == null)
            {
                return NotFound();
            }

            return View(management);
        }

        // POST: Managements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Management == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Management'  is null.");
            }
            var management = await _context.Management.FindAsync(id);
            if (management != null)
            {
                _context.Management.Remove(management);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManagementExists(int id)
        {
          return (_context.Management?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
