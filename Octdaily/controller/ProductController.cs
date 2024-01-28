
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Octdaily.Data;
using Octdaily.Dto;
using Octdaily.interfaces;
using Octdaily.Models;
using Octdaily.repository;

namespace Octdaily.controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProducts()
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProducts());


            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(products);
        }


        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(400)]
        public IActionResult GetProduct(string id)
        {
            if (!_productRepository.ProductExists(id))
                return NotFound();

            var products = _productRepository.GetProduct(id);



            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateProduct([FromBody] ProductDto productCreate)
        {
            if (productCreate == null)
                return BadRequest(ModelState);
            var products = _productRepository.GetProducts().Where(p => p.name.Trim().ToUpper() == productCreate.name.TrimEnd().ToUpper()).FirstOrDefault();

            if (products != null)
            {
                ModelState.AddModelError("", "Product already exists");
                return StatusCode(422, ModelState);

            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productCreate);

            if(!_productRepository.CreateProduct(productMap))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);

            }
          
            return Ok("Successfully created");
        }
        [HttpPut("{ProductId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateProduct([FromQuery]string id,  [FromBody] ProductDto updateProduct)
        {
            if (updateProduct == null)
                return BadRequest(ModelState);

            if(id != updateProduct.id)
                return BadRequest(ModelState);

            if (!_productRepository.ProductExists(id))
                return NotFound();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(updateProduct);

            if(!_productRepository.UpdateProduct(productMap))
                {

                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);

            }
            return NoContent(); 

        }

        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteProduct([FromQuery] string id)
        {
            if (!_productRepository.ProductExists(id))
            {
                return NotFound();
            }

            var productToDelete = _productRepository.GetProduct(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productRepository.DeleteProduct(productToDelete))
            {
                ModelState.AddModelError("", "something went wrong while deleting");

            }
            return NoContent();
            
        }




    }
}
