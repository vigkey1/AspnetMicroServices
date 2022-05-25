using CatalogAPI.Entities;
using CatalogAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CatalogAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepo _productRepo;
        private readonly ILogger<CatalogController> _logger;
        public CatalogController(IProductRepo productRepo, ILogger<CatalogController> logger)
        {
            _productRepo = productRepo ?? throw new ArgumentNullException(nameof(productRepo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepo.GetProducts();
            return Ok(products);
        }


        [HttpGet("{id=length(24)}", Name = "GetProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _productRepo.GetProduct(id);

            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not available");
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        [Route("[action]/{category}", Name = "GetProductCategory")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var products = await _productRepo.GetProductByCategory(category);
            return Ok(products);
        }
        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        { 
            await _productRepo.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }


        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> updateProduct([FromBody] Product product)
        {
           
            return Ok(await _productRepo.UpdateProduct(product));
        }

        [HttpDelete("{id:length(24)}",Name ="DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> DeleteProductById(string id)
        {
           
            return Ok(await _productRepo.DeleteProduct(id));
        }


    }
}
