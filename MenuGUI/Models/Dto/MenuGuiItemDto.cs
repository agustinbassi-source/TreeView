using System.ComponentModel.DataAnnotations;

namespace MenuGUI.Models
{
  public class MenuGuiItemDto
  {
    [Key]
    public int Id { get; set; }
    public string? I18n { get; set; }
    public string? Title { get; set; }
    public int Order { get; set; }
    public List<MenuGuiItemDto> Items { get; set; } = new List<MenuGuiItemDto>();
  }
}
