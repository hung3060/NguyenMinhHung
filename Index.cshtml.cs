using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using P_31_119001040_NguyenMinhHung.Models;

namespace P_31_119001040_NguyenMinhHung.Pages.Sachs
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        private readonly IConfiguration Configuration;

        public IndexModel(SchoolContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
    public string NameSort { get; set; }
    public string DateSort { get; set; }
    public string CurrentFilter { get; set; }
    public string CurrentSort { get; set; }

        public PaginatedList<Sach> Sach { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder,int? pageIndex)
        {
            // using System;
        NameSort = String.IsNullOrEmpty(sortOrder) ? "fl_id" : "";
        DateSort = sortOrder == "fl_id" ? "" : "";
        if (pageIndex ==1)
            {
                pageIndex = 1;
            }

        IQueryable<Sach> sachIQ = from s in _context.Sach
                                        select s;

        switch (sortOrder)
        {
            case "fl_id":
                sachIQ = sachIQ.OrderByDescending(s => s.SachID);
                break;
            default:

                break;
        }


        var pageSize = Configuration.GetValue("PageSize", 4);
            Sach = await PaginatedList<Sach>.CreateAsync(
                sachIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
