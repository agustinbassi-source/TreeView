using Microsoft.AspNetCore.Mvc;
using System;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Globalization;
using MenuGUI.Service;
using MenuGUI.Repository;
using MenuGUI.Data;
using MenuGUI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MenuGUI.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MenuGUIContext _context;

        public MenuController(MenuGUIContext context)
        {
            _context = context;
        }

        // GET: api/<MenuController>
        [HttpGet]
        public List<TreeView> GetMenuItems(int menuId)
        {
            if (menuId == 0)
            {
                throw new Exception("Menu Item Id == 0");
            }

            MenuRespository menuRespository = new MenuRespository(_context);

            var menu = menuRespository.GetMenuItems(menuId);

            return menu.Children;
        }

        [HttpGet]
        public List<Menu> GetMenus()
        {
            MenuRespository menuRespository = new MenuRespository(_context);

            var menu = menuRespository.GetMenus();

            return menu;
        }

        // GET api/<MenuController>/5
        [HttpGet("{id}")]
        public MenuGuiItem GetNode(int id)
        {
            return _context.MenuGuiItem.Where(x => x.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public void CreateNode(MenuGuiItem item)
        {
            if ( item.MenuId == 0)
            {
                throw new Exception("Menu Item Id == 0");
            }
        }

        [HttpPost]
        public void UpdateNode(MenuGuiItem item)
        {
            if (item.Id == 0 || item.MenuId == 0)
            {
                throw new Exception("Menu Item Id == 0");
            }
        }
    }
}
