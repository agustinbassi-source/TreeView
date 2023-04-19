using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MenuGUI.Models;

namespace MenuGUI.Data
{
    public class MenuGUIContext : DbContext
    {
        public MenuGUIContext (DbContextOptions<MenuGUIContext> options)
            : base(options)
        {
        }
        public DbSet<MenuGUI.Models.Menu> Menu { get; set; } = default!;

        public DbSet<MenuGUI.Models.MenuGuiItem> MenuGuiItem { get; set; } = default!;

        public DbSet<MenuGUI.Models.Coment>? Coment { get; set; }
    }
}
