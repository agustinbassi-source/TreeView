using System.ComponentModel.DataAnnotations;

namespace MenuGUI.Models
{
	public class Coment
	{
		[Key]
		public int Id { get; set; }
		public string? Link { get; set; }
		public string? Usr { get; set; }
		public string? UsrComent { get; set; }
		public DateTime Date { get; set; }
		public int MenuId { get; set; }
 
	}
}
