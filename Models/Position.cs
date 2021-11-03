using System.ComponentModel.DataAnnotations;

namespace DSCC.CW1._7902.UI.Models
{
    // Create model for Position, that implements IModel.
    public class Position : IModel
    {
        #region Variables
        // Implement the interface member 'Id'.
        public int Id { get; set; }

        // Create the rest of the required variables.
        [Required]
        [Display(Name = "Position name"), MinLength(3), MaxLength(50)]
        public string Name { get; set; }
        #endregion
    }
}
