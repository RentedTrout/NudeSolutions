using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NudeSolutions.Models;
using System.Threading.Tasks;

namespace NudeSolutions.Pages.InsuranceItems
{
    public class DetailsModel : PageModel
    {
        private readonly NudeSolutions.Data.NudeSolutionsContext _context;

        public DetailsModel(NudeSolutions.Data.NudeSolutionsContext context)
        {
            _context = context;
        }

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
    }
}