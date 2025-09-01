using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.BLL.ViewModels
{
    public class GradeListVM
    {
        public IEnumerable<GradeVM> Items { get; set; } = Enumerable.Empty<GradeVM>();
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
        public string? SearchString { get; set; }
    }
}
