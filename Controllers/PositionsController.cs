using DSCC.CW1._7902.UI.Models;

namespace DSCC.CW1._7902.UI.Controllers
{
    // Create PositionsController class, that extends BaseController. As a
    // generic parameters Position model is passed
    public class PositionsController : BaseController<Position>
    {
        // Passing "positions" as modelName parameter
        public PositionsController(string modelName = "positions") : base(modelName)
        {
        }
    }
}
