using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NudeSolutions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NudeSolutions.Pages
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

        /// <summary>
        /// Get insurance item by supplied ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Order insurance items ascending or descending by column
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsyncForSort(int id)
        {
            await OnGetAsync();

            switch (id)
            {
                case 1:
                    InsuranceItems = InsuranceItems.OrderBy(item => item.InsuranceCategoryID).ToList();
                    break;

                case 2:
                    InsuranceItems = InsuranceItems.OrderByDescending(item => item.InsuranceCategoryID).ToList();
                    break;

                case 3:
                    InsuranceItems = InsuranceItems.OrderBy(item => item.Name).ToList();
                    break;

                case 4:
                    InsuranceItems = InsuranceItems.OrderByDescending(item => item.Name).ToList();
                    break;

                case 5:
                    InsuranceItems = InsuranceItems.OrderBy(item => item.Amount).ToList();
                    break;

                case 6:
                    InsuranceItems = InsuranceItems.OrderByDescending(item => item.Amount).ToList();
                    break;
            }

            return Page();
        }

        /// <summary>
        /// retrieve items from database
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            InsuranceItems = await _context.InsuranceItem.ToListAsync();
            InsuranceCategories = await _context.InsuranceCategory.ToListAsync();
        }

        /// <summary>
        /// Handle Sort features based on supplied button id pressed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task OnPostSortAsync(int id)
        {
            await OnGetAsyncForSort(id);
        }

        /// <summary>
        /// Handle delete request for supplied insurance item ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            InsuranceItem = _context.InsuranceItem.Where(item => item.ID == id).FirstOrDefault();
            _context.InsuranceItem.Remove(InsuranceItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        /// <summary>
        /// Handle edit request for supplied insurance item ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Handle add new item request
        /// </summary>
        /// <returns></returns>
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