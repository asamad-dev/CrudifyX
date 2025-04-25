using CrudifyX.Web.DataAccess;
using CrudifyX.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudifyX.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ProductRepository _repository;

        public ProductController(ProductRepository repository)
        {
            _repository = repository;
        }


        [Route("/Product")]
        public async Task<IActionResult> Index()
        {
            var products = await _repository.GetAllProductsAsync();
            return View(products);
        }

        [Route("/Product/Create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Product/Create")]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid) return View(product);
            await _repository.InsertProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        [Route("/Product/Edit/{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Route("/Product/Edit")]
        public async Task<IActionResult> Edit(Product product)
        {
            if (!ModelState.IsValid) return View(product);
            await _repository.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        [Route("/Product/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("/Product/DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // ----------- API FOR REACT ------------------

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _repository.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateReact([FromBody] Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _repository.InsertProductAsync(product);
            return Ok();
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditReact(int id, [FromBody] Product product)
        {
            if (id != product.Id) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _repository.UpdateProductAsync(product);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteReact(int id)
        {
            await _repository.DeleteProductAsync(id);
            return Ok();
        }
    }
}
