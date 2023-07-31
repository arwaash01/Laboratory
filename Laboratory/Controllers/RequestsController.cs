using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laboratory.Data;
using Laboratory.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Laboratory.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Requests
        [Authorize(Roles = "Admin , Recep")]
        public async Task<IActionResult> Index(string? college , string? studentstatus)
        {
            if (!string.IsNullOrEmpty(college) && !string.IsNullOrEmpty(studentstatus))
            {
                return View(await _context.Requests.Where(r => r.College == college && r.StudentStatus == studentstatus).ToListAsync());
            }

            else if(!string.IsNullOrEmpty(college) || !string.IsNullOrEmpty(studentstatus))
            {
                return View(await _context.Requests.Where(r => r.College == college || r.StudentStatus == studentstatus).ToListAsync());
            }
            else
            {
                return _context.Requests != null ?
                            View(await _context.Requests.ToListAsync()) :
                            Problem("Entity set 'ApplicationDbContext.Requests'  is null.");
                
            }


        }

        // GET: Requests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var requests = await _context.Requests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requests == null)
            {
                return NotFound();
            }

            return View(requests);
        }

        // GET: Requests/Create
        public IActionResult Create()
        {
            var managment = _context.Management.Where(x => x.Name == "limitationDays").FirstOrDefault();
            if (managment is null)
            {
                ViewBag.ErrorMessage = "You need to set the limit in mangment page";
                return View();
            }
            var limitDays = managment.Value;

            var dateTo = DateTime.Now.AddDays(30);
            List<DateTime> avilableDates = new List<DateTime>();
            for(var date = DateTime.Now;date <= dateTo;date = date.AddDays(1))
            {
                if (date.DayOfWeek.ToString() == "Friday" || date.DayOfWeek.ToString() == "Saturday")
                {
                    continue;
                 }
                var requestCount = _context.Requests.Where(x => x.TestDate.Date == date.Date).Count();
                if (requestCount >= limitDays)
                {
                    continue;
                }
                 avilableDates.Add(date);
                }
            ViewBag.AvailablDates = avilableDates;
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NationalResidenceId,UniversityNumber,StudentStatus,College,FirstNameEnglish,FatherNameEnglish,GrandFatherNameEnglish,FamilyNameEnglish,FirstNameArabic,FatherNameArabic,GrandFatherNameArabic,FamilyNameArabic,Email,PhoneNo,BirthDate,MedicalFileNo,TestDate")] Requests requests)
        {
            var management = _context.Management.Where(x => x.Name == "limitationDays").FirstOrDefault();
            if (management is null)
            {
                ViewBag.ErrorMessage = "You need to set limit in management page";
                return View();
            }
            var limitDays = management.Value;
            var requestsCount=_context.Requests.Where(X => X.TestDate == requests.TestDate).Count();
            if (requestsCount >= limitDays)
            {
                ViewBag.ErrorMessage = "Sorry,The limit of Requests for this Day is Reached";
                return View();
            }
            if (ModelState.IsValid)
            {
                _context.Add(requests);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Message");
            }
            return View(requests);
        }
        public IActionResult Message()
        {
            return View();
        }
        // GET: Requests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var requests = await _context.Requests.FindAsync(id);
            if (requests == null)
            {
                return NotFound();
            }
            return View(requests);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NationalResidenceId,UniversityNumber,StudentStatus,College,FirstNameEnglish,FatherNameEnglish,GrandFatherNameEnglish,FamilyNameEnglish,FirstNameArabic,FatherNameArabic,GrandFatherNameArabic,FamilyNameArabic,Email,PhoneNo,BirthDate,MedicalFileNo,TestDate")] Requests requests)
        {
            if (id != requests.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requests);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestsExists(requests.Id))
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
            return View(requests);
        }

        // GET: Requests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var requests = await _context.Requests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requests == null)
            {
                return NotFound();
            }

            return View(requests);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Requests == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Requests'  is null.");
            }
            var requests = await _context.Requests.FindAsync(id);
            if (requests != null)
            {
                _context.Requests.Remove(requests);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestsExists(int id)
        {
          return (_context.Requests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
