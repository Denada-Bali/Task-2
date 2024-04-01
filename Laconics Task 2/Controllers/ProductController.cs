using LaconicsCrm.webapi.Data;
using LaconicsCrm.webapi.Models.Domain;
using LaconicsCrm.webapi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaconicsCrm.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly LaconicsDatabaseContext laconicsDatabaseContext;
        private readonly IProductRepository productRepository;

        public ProductController(LaconicsDatabaseContext laconicsDatabaseContext, IProductRepository productRepository)
        {
            this.laconicsDatabaseContext = laconicsDatabaseContext;
            this.productRepository = productRepository;
        }

        //get all products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productDomain = await productRepository.GetAllAsync(); //get data from database
            return Ok(productDomain);
        }


        //get product by id (single products)
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        //create new product
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            var productModel = new Product
            {
                productName = product.productName,
                price = product.price
            };
            productModel = await productRepository.CreateAsync(productModel);
            laconicsDatabaseContext.SaveChanges();

            //return CreatedAtAction(nameof(GetByIdAsync), new { id = productModel.productId }, productModel);
            return Ok(productModel);
        }


        //update product
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] Product product)
        {
            var productModel = new Product
            {
                productName = product.productName,
                price = product.price
            };

            productModel = await productRepository.UpdateAsync(id, product);

            if (productModel == null)
            {
                return NotFound();
            }

            return Ok(productModel);
        }

        //delete product
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var productModel = await productRepository.DeleteAsync(id);

            if (productModel == null)
            {
                return NotFound();
            }
            return Ok(productModel);
        }
    }
}
