using MenuGUI.Models;
using System.Globalization;
using MenuGUI.Controllers.Api;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Globalization;
using Wigos.I18N.Modules.Common;

namespace MenuGUI.Service
{
	public static class TreeViewService
	{
		public static TreeView LoadTree(List<MenuGuiItem> items)
		{
			//var rootNode = items.FirstOrDefault(i => i.ParentId == 0);
			//if (rootNode == null)
			//{
			//  return new TreeView(0, "Sin items", "Sin items", 0);
			//}

			var rootNode = new MenuGuiItem();
			rootNode.Title = "Root";


			var treeViewRetorno = new TreeView(rootNode.Id, rootNode.Title, rootNode.I18n, rootNode.OrderMenu);
			LoadLeaves(items, treeViewRetorno);
			return treeViewRetorno;
		}

		private static void LoadLeaves(List<MenuGuiItem> items, TreeView node)
		{
			var children = items.Where(m => m.ParentId == node.Id).ToList();
			foreach (var child in children)
			{
				if (node.Children == null)
				{
					node.Children = new List<TreeView>();
				}

				var childNode = new TreeView(child.Id, child.Title, child.I18n, child.OrderMenu);
				node.Children.Add(childNode);
				LoadLeaves(items, childNode);
			}
		}
	}

	public class TreeView
	{
		public TreeView(int id, string? title, string? i18n, int order)
		{
			Id = id;

			I18n = i18n;
			Order = order;

			if (i18n != null && i18n.Trim() != string.Empty)
			{
				var resourceMan = new ResourceManager("Wigos.I18N.Modules.GuiPlayerTracking.Resources", typeof(Resources).Assembly);

                Text = resourceMan.GetString(i18n, new CultureInfo("en-US"));
			}
			else
			{
				Text = title;
			}
		}
		public int Id { get; set; }
		public string? I18n { get; set; }
		public string? Text { get; set; } 
		public int Order { get; set; }
		public List<TreeView> Children { get; set; }  
	}
}
