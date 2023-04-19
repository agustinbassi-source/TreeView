using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuGUI.Models
{
    public class MenuGuiItem : BaseEntity
    {
    
        public string? I18n { get; set; }
        public string? Title { get; set; }
        public int OrderMenu { get; set; }
        public int ParentId { get; set; }
        public int MenuId { get; set; }
    }
}
