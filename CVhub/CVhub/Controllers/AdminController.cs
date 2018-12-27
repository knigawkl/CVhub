﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CVhub.EntityFramework;
using CVhub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVhub.Controllers
{
    public class AdminController : Controller
    {
        private readonly DataContext _context;

        public AdminController(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            var companies = await _context.Companies.ToListAsync(); 
            return View(companies);
        }

        public async Task<ActionResult> Details(int id)
        {
            var company = await this._context.Companies.FirstOrDefaultAsync(x => x.Id == id);
            return View(company);
        }

        /*
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var company = await this._context.Companies.FirstOrDefaultAsync(x => x.Id == id);
            if (company == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Company model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var company = await this._context.Companies.FirstOrDefaultAsync(x => x.Id == model.Id);
            company.Name = model.Name;

            var newCompany = new Company {
                Id = company.Id,
                Name = company.Name
            };
            await _context.Companies.AddAsync(newCompany);
            await _context.SaveChangesAsync();


            return RedirectToAction("Details", new { id = model.Id });
        }
        */
    }
}