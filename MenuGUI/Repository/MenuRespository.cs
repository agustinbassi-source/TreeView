using MenuGUI.Data;
using MenuGUI.Models;
using MenuGUI.Service;

namespace MenuGUI.Repository
{
    public class MenuRespository
    {
        private readonly MenuGUIContext _context;

        public MenuRespository(MenuGUIContext context)
        {
            _context = context;
        }

        public List<Menu> GetMenus()
        {
            return _context.Menu.OrderBy(x => x.Title).ToList();
        }

        public TreeView GetMenuItems(int menuId)
        {
            if (menuId == 0)
            {
                throw new Exception("Menu Id == 0");
            }

            var items = _context.MenuGuiItem.Where(x => x.MenuId == menuId).OrderBy(x => x.OrderMenu).ToList();

            var menu = TreeViewService.LoadTree(items);

            return menu;

            //foreach (var item in items)
            //{
            //  if (item.ParentId == null)
            //  {
            //    menu.Items.Add(Convert(item));
            //  }
            //  else
            //  {

            //  }

            //}

            //var systemMenu = new MenuGuiItemDto();

            //systemMenu.Items.Add(new MenuGuiItemDto { I18n = "" });

            //var egms = new MenuGuiItemDto();


            //menu.Items.Add(systemMenu);
            //menu.Items.Add(egms);

            return menu;
        }

        private MenuGuiItemDto Convert(MenuGuiItem item)
        {
            return new MenuGuiItemDto
            {
                I18n = item.I18n,
                Title = item.I18n,
                Id = item.Id,
                Order = item.OrderMenu
            };
        }

        public int CreateMenuItem(MenuGuiItem item)
        {
            if (item.MenuId == 0)
            {
                throw new Exception("Menu Item Id == 0");
            }

            _context.MenuGuiItem.Add(item);
            _context.SaveChanges();

            return item.Id;
        }

        public int UpdateMenuItem(MenuGuiItem item)
        {
            if (item.MenuId == 0 || item.Id == 0)
            {
                throw new Exception("Menu Item Id == 0");
            }

            var itemUpdate = _context.MenuGuiItem.Where(_x => _x.Id == item.Id).FirstOrDefault();
 
            itemUpdate.Title = item.Title;
            itemUpdate.OrderMenu = item.OrderMenu;
            itemUpdate.ParentId = item.ParentId;

            _context.SaveChanges();

            return item.Id;
        }
    }
}
