using DSCC.CW1._7902.UI.Models;

namespace DSCC.CW1._7902.UI.Controllers
{
    // Create EmployeesController class, that extends BaseController. As a
    // generic parameters Employee model is passed
    public class EmployeesController : BaseController<Employee>
    {
        // Passing "employees" as modelName parameter
        public EmployeesController(string modelName = "employees") : base(modelName)
        {
        }
    }
}
