using CourseManagement.DAL.Data;
using CourseManagement.DAL.Entites;
using CourseManagement.DAL.Repositores.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.DAL.Repositores
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
       
    }
}
