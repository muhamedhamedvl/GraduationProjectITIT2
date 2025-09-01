using CourseManagement.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.BLL.ViewModels
{
    public class CourseListVM
    {
        public List<CourseVM> Items { get; set; } = new List<CourseVM>();
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public string? Search { get; set; }
        public Category? Category { get; set; }
    }
}
