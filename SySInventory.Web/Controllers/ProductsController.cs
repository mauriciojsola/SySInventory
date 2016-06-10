using System.Collections;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SySInventory.Core.Model.Entities;
using SySInventory.Core.Repository.Product;
using SySInventory.Web.Helpers;

namespace SySInventory.Web.Controllers
{
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

        public ActionResult Index()
        {
            return View();
        }

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
            InUnitOfWork(() =>
            {
                var products = ProductRepository.GetAll().OrderBy(x => x.Description);
                var grid = _gridMvcHelper.GetAjaxGrid(products, page);
                jsonData = _gridMvcHelper.GetGridJsonData(grid, GRID_PARTIAL_PATH, this);
            });

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        // GET: Products
        //public ActionResult Index()
        //{
        //    var products = ProductRepository.GetAll();
        //    var grid = (AjaxGrid<Product>)new AjaxGridFactory().CreateAjaxGrid(products, 1, false);

        //    return View(grid);
        //}

        //private IQueryable<ProductModel> Data()
        //{
        //    var cars = new List<ProductModel>()
        //        {
        //            new ProductModel
        //                    {
        //                        Code = "PRODUCTO1",Description = "DESCRIPCION 1",CostPrice = 20.0m,RetailPrice = 25.0m,WholesalePrice = 30.0m
        //                    },
        //            new ProductModel
        //                    {
        //                        Code = "PRODUCTO2",Description = "DESCRIPCION 2",CostPrice = 20.0m,RetailPrice = 25.0m,WholesalePrice = 30.0m
        //                    }

        //        }.AsQueryable();

        //    return cars;
        //}


        // GET: Products/Details/5
        public ActionResult Details(int? id)
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

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductCategoryId = new SelectList(ProductCategoryRepository.GetAll().ToList(), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
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
        public ActionResult Edit(int? id)
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
            ViewBag.ProductCategoryId = new SelectList(ProductCategoryRepository.GetAll().ToList(), "Id", "Name", product.ProductCategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Description,CostPrice,RetailPrice,WholesalePrice,ProductCategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                //var editProduct = ProductRepository.GetById(product.Id);
                //if (editProduct != null)
                //{
                //    editProduct.Code = product.Code;
                ProductRepository.Update(product);
                //}

                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryId = new SelectList(ProductCategoryRepository.GetAll().ToList(), "Id", "Name", product.ProductCategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = ProductRepository.GetById(id);
            ProductRepository.Remove(product);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    db.Dispose();
            //}
            base.Dispose(disposing);
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
