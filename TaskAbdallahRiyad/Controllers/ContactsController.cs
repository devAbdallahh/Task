using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskAbdallahRiyad.Data;
using TaskAbdallahRiyad.Models;

namespace TaskAbdallahRiyad.Controllers
{

    public class ContactsController : Controller
    {
        /*configuration*/
        #region configuration
        private readonly AppDbContext _db;

        public ContactsController(AppDbContext db)
        {
            _db = db;
        }
        #endregion

        /*Operation{CRUD}*/
        #region Operation{CRUD}

        #endregion

        /*Index View List Contact*/
        public async Task<IActionResult> Index()
        {
            return View(await _db.contacts.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string term)
        {
            return View(await _db.contacts.Where(x => x.FirstName!.Contains(term)).ToListAsync());
        }
        /*Details HttpGet*/
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.contacts == null)
            {
                return NotFound();
            }

            var contact = await _db.contacts
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }
        /*Create HttpGet*/
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        /*Create HttpPost*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _db.Add(contact);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }
        /*Edit HttpGet*/
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.contacts == null)
            {
                return NotFound();
            }

            var contact = await _db.contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }
        /*Edit HttpPost*/
        [HttpPost]
        public async Task<IActionResult> Edit(int id,Contact contact)
        {
            if (id != contact.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(contact);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.ContactId))
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
            return View(contact);
        }
        /*Delete HttpGet*/
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.contacts == null)
            {
                return NotFound();
            }

            var contact = await _db.contacts
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }
        /*Delete HttpPost*/
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.contacts == null)
            {
                return Problem("Entity set 'AppDbContext.contacts'  is null.");
            }
            var contact = await _db.contacts.FindAsync(id);
            if (contact != null)
            {
                _db.contacts.Remove(contact);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
          return (_db.contacts?.Any(e => e.ContactId == id)).GetValueOrDefault();
        }
    }
    /*NotFound*/
    /*public IActionResult NotFound()
    {
        return View();
    }*/
}
