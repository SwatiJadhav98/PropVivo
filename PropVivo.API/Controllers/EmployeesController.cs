using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropVivo.API.Models;

namespace PropVivo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeTaskDbContext _context;

        public EmployeesController(EmployeeTaskDbContext context)
        {
            
        }
    }
}
