﻿using System.Linq;
using System.Web.Mvc;
using Grid.Mvc.Ajax.GridExtensions;

namespace SySInventory.Web.Helpers
{
    public class GridMvcHelper : IGridMvcHelper
    {
        public AjaxGrid<T> GetAjaxGrid<T>(IOrderedQueryable<T> items) where T : class
        {
            return this.GetAjaxGrid(items, null);
        }

        public AjaxGrid<T> GetAjaxGrid<T>(IOrderedQueryable<T> items, int? page) where T : class
        {
            var ajaxGridFactory = new AjaxGridFactory();
            int pageValue = page.HasValue ? page.Value : 1;
            var grid = ajaxGridFactory.CreateAjaxGrid(items, pageValue, page.HasValue);
            var castGrid = (AjaxGrid<T>)grid;

            return castGrid;
        }

        public object GetGridJsonData<T>(AjaxGrid<T> grid, string gridPartialViewPath, Controller controller) where T : class
        {
            string html = grid.ToJson(gridPartialViewPath, controller);
            bool hasItems = grid.HasItems;
            object jsonData = new
            {
                Html = html,
                hasItems
            };

            return jsonData;
        }
    }

    public interface IGridMvcHelper
    {
        AjaxGrid<T> GetAjaxGrid<T>(IOrderedQueryable<T> items) where T : class;
        AjaxGrid<T> GetAjaxGrid<T>(IOrderedQueryable<T> items, int? page) where T : class;
        object GetGridJsonData<T>(AjaxGrid<T> grid, string gridPartialViewPath, Controller controller) where T : class;
    }
}