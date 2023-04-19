using System.ComponentModel.DataAnnotations;

namespace MenuGUI.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
