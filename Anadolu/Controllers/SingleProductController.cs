using Anadolu.DTO;
using Anadolu.Models;
using Anadolu.Repository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anadolu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class SingleProductController : ControllerBase
    {
        IUnitOfWork unitOfWork;


        public SingleProductController(IUnitOfWork _unitOfWork)
        {

            unitOfWork = _unitOfWork;


        }

        [HttpGet("product/{id}")]
        public IActionResult GetProductByID(int id)
        {
            ResultDTO result = new ResultDTO();

            Product Product = unitOfWork.ProductRepository.GetProductById(id, l => l.IsDeleted == false);
            string productName = Product.Name;
            float productPrice = Product.Price;
            string productImage = Product.ImagePath;
            string ProductDescription = Product.Description;

            if (ModelState.IsValid)
            {
                Discount discount = unitOfWork.DiscountRepository.GetDiscountByProductId(Product.Id);

                SingleProductDTO singleProductDTO = new SingleProductDTO();
                singleProductDTO.ProductName = productName;
                singleProductDTO.ProductPrice = productPrice;
                singleProductDTO.ProductImage = productImage;
                singleProductDTO.ProductDescription = ProductDescription;
                singleProductDTO.ProductPriceAfterDiscount = discount.ProductPriceAfterDiscount;
                singleProductDTO.Id = Product.Id;

                if (Product != null)
                {
                    result.IsPassed = true;
                    result.Data = singleProductDTO;
                    return Ok(result);
                }
            }
            result.IsPassed = false;
            result.Data = "No user Exist With this Id";
            return BadRequest(result);
        }

        [HttpPost]
        public ActionResult<ResultDTO> AddToCart(AddToCartDTO AddToCartDTO)
        {
            ResultDTO result = new ResultDTO();
            ProductCart productcart = new ProductCart();

            if (ModelState.IsValid)
            {
                Cart cart = unitOfWork.CartRepository.GetByIdString(productcart.CartId, c => c.IsDeleted == false);

                productcart.ProductId = AddToCartDTO.ProductId;

                productcart.CartId = AddToCartDTO.UserId;
                productcart.Quantity = AddToCartDTO.Quantity;



                unitOfWork.ProductCartRepository.Add(productcart);



                result.IsPassed = true;
                result.Data = productcart;
                return Ok(result);
            }
            result.IsPassed = false;
            result.Data = "ModelState Is Invalid";
            return BadRequest(result);
        }


        //public class SingleProductController : ControllerBase
        //{
        //    IUnitOfWork unitOfWork;


        //    public SingleProductController(IUnitOfWork _unitOfWork)
        //    {

        //        unitOfWork = _unitOfWork;


        //    }

        //    [HttpGet("product/{id}")]
        //    public IActionResult GetProductByID(int id)
        //    {
        //        ResultDTO result = new ResultDTO();

        //        Product Product = unitOfWork.ProductRepository.GetProductById(id, l => l.IsDeleted == false);
        //        string productName = Product.Name;
        //        float productPrice = Product.Price;
        //        string productImage = Product.ImagePath;
        //        string ProductDescription = Product.Description;

        //        if (ModelState.IsValid)
        //        {
        //            Discount discount = unitOfWork.DiscountRepository.GetDiscountByProductId(Product.Id);

        //            SingleProductDTO singleProductDTO = new SingleProductDTO();
        //            singleProductDTO.ProductName = productName;
        //            singleProductDTO.ProductPrice = productPrice;
        //            singleProductDTO.ProductImage = productImage;
        //            singleProductDTO.ProductDescription = ProductDescription;
        //            singleProductDTO.ProductPriceAfterDiscount = discount.ProductPriceAfterDiscount;

        //            if (Product != null)
        //            {

        //                result.IsPassed = true;
        //                result.Data = singleProductDTO;
        //                return Ok(result);
        //            }
        //        }
        //        result.IsPassed = false;
        //        result.Data = "No user Exist With this Id";
        //        return BadRequest(result);
        //    }

        //    [HttpPost]
        //    public ActionResult<ResultDTO> AddToCart(AddToCartDTO AddToCartDTO)
        //    {
        //        ResultDTO result = new ResultDTO();
        //        ProductCart productcart = new ProductCart();

        //        if (ModelState.IsValid)
        //        {
        //            Cart cart = unitOfWork.CartRepository.GetByIdString(productcart.CartId,c=>c.IsDeleted == false);


        //            productcart.ProductId = AddToCartDTO.ProductId;

        //            productcart.CartId = AddToCartDTO.UserId;
        //            productcart.Quantity = AddToCartDTO.Quantity;



        //            unitOfWork.ProductCartRepository.Add(productcart);



        //            result.IsPassed = true;
        //            result.Data = productcart;
        //            return Ok(result);
        //        }
        //        result.IsPassed = false;
        //        result.Data = "ModelState Is Invalid";
        //        return BadRequest(result);
        //    }
    }
}
