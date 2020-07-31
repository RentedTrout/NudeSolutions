using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NudeSolutions.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NudeSolutions.Pages.InsuranceItems
{
    public class EditModel : PageModel
    {
        private readonly NudeSolutions.Data.NudeSolutionsContext _context;

        public EditModel(NudeSolutions.Data.NudeSolutionsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InsuranceItem InsuranceItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InsuranceItem = await _context.InsuranceItem.FirstOrDefaultAsync(m => m.ID == id);

            if (InsuranceItem == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(InsuranceItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsuranceItemExists(InsuranceItem.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool InsuranceItemExists(int id)
        {
            return _context.InsuranceItem.Any(e => e.ID == id);
        }
    }
}