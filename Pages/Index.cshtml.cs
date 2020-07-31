﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NudeSolutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NudeSolutions.Pages.InsuranceItems
{
    public class IndexModel : PageModel
    {
        private readonly Data.NudeSolutionsContext _context;

        public IndexModel(Data.NudeSolutionsContext context)
        {
            _context = context;
        }

        #region Properties
        [BindProperty]
        public InsuranceItem InsuranceItem { get; set; }

        public IList<InsuranceItem> InsuranceItems { get; set; }

        public IList<InsuranceCategory> InsuranceCategories { get; set; }

        #endregion Properties

        #region Methods
        public async Task<IActionResult> OnGetAsyncByID(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await OnGetAsync();

            InsuranceItem = await _context.InsuranceItem.FirstOrDefaultAsync(m => m.ID == id);

            if (InsuranceItem == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnGetAsyncForSort(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await OnGetAsync();

            switch (id)
            {
                case 1:
                    InsuranceItems = InsuranceItems.OrderBy(item => item.InsuranceCategoryID).ToList();
                    break;
                case 2:
                    InsuranceItems = InsuranceItems.OrderBy(item => item.Name).ToList();
                    break;
                case 3:
                    InsuranceItems = InsuranceItems.OrderBy(item => item.Amount).ToList();
                    break;
            }

            if (InsuranceItem == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task OnGetAsync()
        {
            InsuranceItems = await _context.InsuranceItem.ToListAsync();
            InsuranceCategories = await _context.InsuranceCategory.ToListAsync();
        }

        public async Task OnPostSortAsync(int id)
        {
            await OnGetAsyncForSort(id);
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {


            InsuranceItem = _context.InsuranceItem.Where(item => item.ID == id).FirstOrDefault();
            _context.InsuranceItem.Remove(InsuranceItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public async Task OnPostEditAsync(int id)
        {
            if (id == 0)
            {                
                _context.InsuranceItem.Add(InsuranceItem);
                await _context.SaveChangesAsync();
                await OnGetAsync();
                RedirectToPage("./Index");
            }
            else
            {
                await OnGetAsyncByID(id);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            _context.InsuranceItem.Add(InsuranceItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
    #endregion Methods

}