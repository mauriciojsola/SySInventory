using System.Collections;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SySInventory.Core.Model.Entities;
using SySInventory.Core.Repository.Product;
using SySInventory.Web.Helpers;

namespace SySInventory.Web.Controllers
{
    [RoutePrefix("productos")]
    [Route("{action=index}")]
    public class ProductsController : ControllerBase
    {
        public IProductRepository ProductRepository { get; set; }
        public IProductCategoryRepository ProductCategoryRepository { get; set; }

        private readonly IGridMvcHelper _gridMvcHelper;
        private const string GRID_PARTIAL_PATH = "List";

        public ProductsController()
        {
            _gridMvcHelper = new GridMvcHelper();
        }

        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult GetGrid()
        {
            var products = ProductRepository.GetAll().OrderBy(x => x.Description);
            var grid = _gridMvcHelper.GetAjaxGrid(products);
            return PartialView(GRID_PARTIAL_PATH, grid);
        }

        [HttpGet]
        public ActionResult GridPager(int? page)
        {
            object jsonData = null;
            //InUnitOfWork(() =>
            //{
            var products = ProductRepository.GetAll().OrderBy(x => x.Description);
            var grid = _gridMvcHelper.GetAjaxGrid(products, page);
            jsonData = _gridMvcHelper.GetGridJsonData(grid, GRID_PARTIAL_PATH, this);
            //});

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // GET: Products/Details/5
        [Route("{id}/detalles")]
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var product = ProductRepository.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [Route("crear")]
        public ActionResult Create()
        {
            ViewBag.ProductCategoryId = new SelectList(ProductCategoryRepository.GetAll().ToList(), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("crear")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Description,CostPrice,RetailPrice,WholesalePrice,ProductCategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                ProductRepository.Add(product);
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategoryId = new SelectList(ProductCategoryRepository.GetAll().ToList(), "Id", "Name", product.ProductCategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        [HttpGet]
        [Route("{id}/editar")]
        public ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var product = ProductRepository.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryId = new SelectList(ProductCategoryRepository.GetAll().ToList(), "Id", "Name", product.ProductCategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("editar")]
        public ActionResult Update([Bind(Include = "Id,Code,Description,CostPrice,RetailPrice,WholesalePrice,ProductCategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                ProductRepository.Update(product);
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryId = new SelectList(ProductCategoryRepository.GetAll().ToList(), "Id", "Name", product.ProductCategoryId);
            return View("Edit", product);
        }

        // GET: Products/Delete/5
        [Route("{id}/eliminar")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = ProductRepository.GetById(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost]
        [Route("eliminar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = ProductRepository.GetById(id);
            ProductRepository.Remove(product);
            return RedirectToAction("Index");
        }

    }

    public static class ModelStateHelper
    {
        public static IEnumerable Errors(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return modelState.ToDictionary(kvp => kvp.Key,
                                               kvp => kvp.Value.Errors
                                                         .Select(e => e.ErrorMessage).ToArray())
                                 .Where(m => m.Value.Length > 0).ToList();
            }
            return null;
        }
    }
}
