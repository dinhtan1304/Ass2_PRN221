using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q2.Models;
using System.Xml.Serialization;

namespace Q2.Pages.List
{
    public class IndexModel : PageModel
    {
        private readonly Q2.Models.Prn221TrialContext _context;

        public IndexModel(Q2.Models.Prn221TrialContext context)
        {
            _context = context;
        }

        public IList<Service> Service { get; set; } = default!;

        public string SearchRoom { get; set; }


        public async Task OnGetAsync(string searchString)
        {
            Service = await _context.Services
                .OrderByDescending(item => item.Month)
                .Include(s => s.EmployeeNavigation)
                .Include(s => s.RoomTitleNavigation)
                .ToListAsync();

            SearchRoom = searchString;
            IQueryable<Service> servicesSearch = from s in _context.Services select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                servicesSearch = servicesSearch.Where(s => s.RoomTitle.Contains(searchString));
                Service = await servicesSearch.ToListAsync();
            }

        }

        public async Task<IActionResult> OnPostImportServices(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;

                    var serializer = new XmlSerializer(typeof(List<Service>));
                    var services = serializer.Deserialize(stream) as List<Service>;
                    await _context.Services.AddRangeAsync(services);
                    await _context.SaveChangesAsync();
                }
            }
            Service = await _context.Services
                        .OrderByDescending(item => item.Month)
                        .Include(s => s.EmployeeNavigation)
                        .Include(s => s.RoomTitleNavigation)
                        .ToListAsync();
            return RedirectToPage();
        }
    }
}
