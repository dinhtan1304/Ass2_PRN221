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


        public async Task OnGetAsync(string searchString, string searchRoom)
        {
            if (_context.Services != null)
            {
                Service = await _context.Services
                .OrderByDescending(item => item.Month)
                .Include(s => s.EmployeeNavigation)
                .Include(s => s.RoomTitleNavigation).ToListAsync();
            }

            SearchRoom = searchString;
            IQueryable<Service> servicesSearch = from s in _context.Services select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                servicesSearch = servicesSearch.Where(s => s.RoomTitle.Contains(searchString));
                Service = servicesSearch.ToList();
            }

        }
        public ActionResult ImportServices()
        {
            return Page();
        }

        [HttpPost]
        public ActionResult ImportServices(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Đọc tệp XML được tải lên
                var serializer = new XmlSerializer(typeof(ServiceXML));
                ServiceXML services;

                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    services = (ServiceXML)serializer.Deserialize(reader);
                }

                // Lưu danh sách dịch vụ vào cơ sở dữ liệu
                using (var context = new Prn221TrialContext())
                {
                    foreach (var service in services.Services)
                    {
                        context.Services.Add(service);
                    }

                    context.SaveChanges();
                }

                // Chuyển hướng người dùng đến trang hiển thị danh sách dịch vụ
                return RedirectToAction("List");
            }

            // Trường hợp không có tệp được chọn, trả về lại View
            return Page();
        }
    }
}
