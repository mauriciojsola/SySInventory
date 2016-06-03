using System.Linq;
using System.Net;
using System.Web.Mvc;
using SySInventory.Core.Model.Entities;
using SySInventory.Core.Repository.Product;

namespace SySInventory.Web.Controllers
{
    public class ProductsController : Controller
    {
        public IProductRepository ProductRepository { get; set; }
        public IProductCategoryRepository ProductCategoryRepository { get; set; }

        // GET: Products
        public ActionResult Index()
        {
            var products = ProductRepository.GetAll().ToList();
            return View(products);
        }

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
}
