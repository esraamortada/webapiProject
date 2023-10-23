using Anadolu.DTO;
using Anadolu.Models;
using Anadolu.Repository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Anadolu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly IHostingEnvironment host;
        public ProductController(IUnitOfWork _unit, IHostingEnvironment _host)
        {
            unit = _unit;
            host = _host;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] ProductDTO productDTO)
        {
            ResultDTO result = new ResultDTO();
            if (ModelState.IsValid)
            {

                Product product = new Product();
                product.Name = productDTO.Name;
                product.Description = productDTO.Description;
                product.Quantity = productDTO.Quantity;
                product.Price = productDTO.Price;

                product.SubCategoryId = productDTO.SubCategoryId;

                UploaderImage up = new UploaderImage(host);
                string fileName = await up.Uploade(productDTO.File);

                product.ImagePath = "http://localhost:5194/images/" + fileName;
                unit.ProductRepository.Add(product);

                result.Data = productDTO;
                result.IsPassed = true;
                return Ok(result);
            }

            result.IsPassed = false;
            result.Data = "ModelState Is Invalid";
            return BadRequest(result);
        }

        [HttpPut("editproduct")]
        public async Task<IActionResult> EditProduct([FromForm] EditProductDTO productDTO)
        {
            ResultDTO resultDTO = new ResultDTO();
            if (ModelState.IsValid)
            {
                Product product = unit.ProductRepository.GetById(productDTO.Id, a => a.IsDeleted == false);
                if (product != null)
                {
                    product.Name = productDTO.Name;
                    product.Price = productDTO.Price;
                    product.Description = productDTO.Description;
                    product.Quantity = productDTO.Quantity;
                    product.SubCategoryId = productDTO.SubCategoryId;

                    UploaderImage up = new UploaderImage(host);
                    string fileName = await up.Uploade(productDTO.File);

                    product.ImagePath = "http://localhost:5194/images/" + fileName;

                    unit.ProductRepository.Update(productDTO.Id, product);

                    resultDTO.IsPassed = true;
                    resultDTO.Data = productDTO;
                    return Ok(resultDTO);
                }
                resultDTO.IsPassed = false;
                resultDTO.Data = "No Product Exist With This Id";
                return BadRequest(resultDTO);
            }
            resultDTO.IsPassed = false;
            resultDTO.Data = "ModelState Is Invalid";
            return BadRequest(resultDTO);
        }

        [HttpDelete("deleteproduct/{Id}")]
        public IActionResult DeleteProduct(int Id)
        {
            ResultDTO result = new ResultDTO();

            Product product = unit.ProductRepository.GetById(Id, a => a.IsDeleted == false);
            if (product != null)
            {
                product.IsDeleted = true;

                unit.ProductRepository.Update(Id, product);

                ReturnProductDTO returnProductDTO = new ReturnProductDTO();

                returnProductDTO.Name = product.Name;
                returnProductDTO.SubCategoryId = product.SubCategoryId;
                returnProductDTO.Description = product.Description;
                returnProductDTO.Price = product.Price;
                returnProductDTO.Quantity = product.Quantity;
                returnProductDTO.ImagePath = product.ImagePath;

                result.IsPassed = true;
                result.Data = returnProductDTO;
                return Ok(result);
            }
            result.IsPassed = false;
            result.Data = "No Product Exist With This Id";
            return BadRequest(result);
        }

        /// /////////////////////////////////

        [HttpGet("Productdetails/{Id}")]
        public IActionResult GetProduct(int Id)
        {
            ResultDTO result = new ResultDTO();
            Product product = unit.ProductRepository.GetById(Id, a => a.IsDeleted == false);
            if (product != null)
            {
                ReturnProductDTO returnProductDTO = new ReturnProductDTO();

                returnProductDTO.Name = product.Name;
                returnProductDTO.SubCategoryId = product.SubCategoryId;
                returnProductDTO.Description = product.Description;
                returnProductDTO.Price = product.Price;
                returnProductDTO.Quantity = product.Quantity;
                returnProductDTO.ImagePath = product.ImagePath;

                Discount discount = unit.DiscountRepository.GetById(Id, a => a.IsDeleted == false);
                DiscountDTO discountDTO = new DiscountDTO();

                discountDTO.DiscountValue = discount.Value;
                discountDTO.DiscountStartDate = discountDTO.DiscountStartDate;
                discountDTO.DiscountEndDate = discount.EndDate;

                result.IsPassed = true;
                result.Data = new { Product = returnProductDTO, Discount = discountDTO };
                return Ok(result);
            }
            result.IsPassed = false;
            result.Data = "No Product Exist With This Id";
            return BadRequest(result);
        }

        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            ResultDTO result = new ResultDTO();
            List<Product> Products = unit.ProductRepository.GetAll(d => d.IsDeleted == false);
            if (Products != null)
            {
                List<ReturnProductDTO> ProductDTOs = new List<ReturnProductDTO>();
                for (int i = 0; i < Products.Count; i++)
                {
                    ReturnProductDTO dto = new ReturnProductDTO();

                    dto.Id = Products[i].Id;
                    dto.Name = Products[i].Name;
                    dto.SubCategoryId = Products[i].SubCategoryId;
                    dto.Description = Products[i].Description;
                    dto.Price = Products[i].Price;
                    dto.Quantity = Products[i].Quantity;
                    dto.ImagePath = Products[i].ImagePath;


                    ProductDTOs.Add(dto);
                }
                result.IsPassed = true;
                result.Data = ProductDTOs;
                return Ok(result);
            }
            result.IsPassed = false;
            result.Data = "No Category Exist";
            return BadRequest(result);
        }
        [HttpGet("Discounts/{Id}")]
        public IActionResult GetAllProductsInDiscount(int Id)
        {
            ResultDTO result = new ResultDTO();

            var products = unit.ProductRepository.GetAll(l => l.SubCategoryId == Id && l.IsDeleted == false);
            List<Discount> discounts = unit.DiscountRepository.GetAll(l => false);
            for (int i = 0; i < products.Count; i++)
            {
                int id = products[i].Id;
                Discount discount = unit.DiscountRepository.GetById(id, d => false);
                discounts.Add(discount);
            }

            List<ProductWithDiscountDTO> discountsInHome = new List<ProductWithDiscountDTO>();
            for (int i = 0; i < products.Count; i++)
            {
                ProductWithDiscountDTO dis = new ProductWithDiscountDTO();
                if (discounts[i] != null)
                {
                    dis.ProductId = products[i].Id;
                    dis.ProductName = products[i].Name;
                    dis.DiscountValue = discounts[i].Value;
                    dis.EndDate = discounts[i].EndDate;

                    dis.ProductPrice = products[i].Price;
                    dis.Image = products[i].ImagePath;

                    discountsInHome.Add(dis);
                }

            }




            result.IsPassed = true;
            result.Data = discountsInHome;
            return Ok(result);



        }
    }

}
