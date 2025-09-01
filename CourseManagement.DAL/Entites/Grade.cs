using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CourseManagement.DAL.Entites
{
    public class Grade
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; }

        public int? TraineeId { get; set; }
        public User Trainee { get; set; }
    }
}
