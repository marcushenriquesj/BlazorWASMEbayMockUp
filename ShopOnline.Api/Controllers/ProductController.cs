using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models.Dtos;
using ShopOnline.Api.Extentions;
using ShopOnline.Api.Repositories.Interface;
using ShopOnline.Api.Entities;


namespace ShopOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        //returns requested data and a appropriate status response
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
        {
            try
            {
                var products = await this.productRepository.GetItems();
                var productCategories = await this.productRepository.GetCategories();

                if (products == null || productCategories == null)
                {
                    return NotFound();
                }
                else
                {
                    var productDtos = products.ConverToDto(productCategories);
                    return Ok(productDtos);
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetItem(int id)
        {
            try
            {
                var product = await this.productRepository.GetItem(id);

                if (product == null)
                {
                    return BadRequest();
                }
                else
                {      
                    var productCategory = await this.productRepository.GetCategory(product.CategoryId);

                    var productDto = product.ConverToDto(productCategory);

                    return Ok(productDto);
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route(nameof(GetProductCategories))]
        public async Task<ActionResult<ProductCategoryDto>> GetProductCategories()
        {
            try
            {
                var productCategories = await this.productRepository.GetCategories();

                if (productCategories == null)
                    return BadRequest();
                var productCategoryDtos = productCategories.ConvertToDto();
                if (productCategoryDtos == null)
                    return BadRequest();
                return Ok(productCategoryDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                               "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("{categoryId}/GetProductsByCategory")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(int categoryId)
        {
            try
            {
                var products = await this.productRepository.GetItemsByCategory(categoryId);
                if(products == null)
                    return BadRequest();

                var productCategories = await this.productRepository.GetCategories();

                if (productCategories == null)
                    return BadRequest();

                var productDtos = products.ConverToDto(productCategories);

                return Ok(productDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                                "Error retrieving data from the database");
            }
        }

    }
}