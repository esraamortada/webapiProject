using Anadolu.DTO;
using Anadolu.Models;
using Anadolu.Repository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anadolu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IUnitOfWork unit;

        public DiscountController(IUnitOfWork _unit)
        {
            unit = _unit;
        }

        [HttpPost]
        public IActionResult AddDiscount(DiscountDTO discountDTO)
        {
            ResultDTO resultDTO = new ResultDTO();
            if (ModelState.IsValid)
            {
                Models.Product product = unit.ProductRepository.GetById(discountDTO.ProductId, p => p.IsDeleted);
                Models.Discount discount = new Models.Discount();
                discount.Value = discountDTO.DiscountValue;
                discount.StartDate = DateTime.Now;
                discount.EndDate = discountDTO.DiscountEndDate;
                discount.ProductPriceAfterDiscount = (decimal)((double)product.Price -
                    ((double)product.Price * (discountDTO.DiscountValue)));

                discount.ProductId = discountDTO.ProductId;
                discount.ProductName = product.Name;

                unit.DiscountRepository.Add(discount);

                resultDTO.IsPassed = true;
                resultDTO.Data = discountDTO;
                return Ok(resultDTO);
            }
            resultDTO.IsPassed = false;
            resultDTO.Data = "ModelState Is Invalid";
            return BadRequest(resultDTO);
        }

        [HttpPut]
        public IActionResult EditDiscount(DiscountDTO discountDTO)
        {
            ResultDTO resultDTO = new ResultDTO();
            if (ModelState.IsValid)
            {
                Models.Discount discount = unit.DiscountRepository.GetById(discountDTO.ProductId, a => a.IsDeleted == false);
                if (discount != null)
                {
                    Models.Product product = unit.ProductRepository
                        .GetById(discountDTO.ProductId, p => p.IsDeleted == false);

                    if (product != null)
                    {
                        discount.Value = discountDTO.DiscountValue;
                        discount.StartDate = DateTime.Now;
                        discount.EndDate = discountDTO.DiscountEndDate;
                        discount.ProductPriceAfterDiscount = (decimal)((double)product.Price -
                            ((double)product.Price * ((discountDTO.DiscountValue) / 100)));

                        //discount.ProductId = discountDTO.ProdutId;
                        //discount.ProductName = product.Name;

                        unit.DiscountRepository.Update(discount.ProductId, discount);

                        resultDTO.IsPassed = true;
                        resultDTO.Data = discountDTO;
                        return Ok(resultDTO);
                    }
                    resultDTO.IsPassed = false;
                    resultDTO.Data = "No Product Exist with this Id";
                    return BadRequest(resultDTO);
                }
                resultDTO.IsPassed = false;
                resultDTO.Data = "No Discount Exist With This Id";
                return BadRequest(resultDTO);
            }
            resultDTO.IsPassed = false;
            resultDTO.Data = "ModelState Is Invalid";
            return BadRequest(resultDTO);
        }

        [HttpDelete("deletediscount/{Id}")]
        public IActionResult DeleteDiscount(int Id)
        {
            ResultDTO result = new ResultDTO();

            Models.Discount discount = unit.DiscountRepository.GetById(Id, a => a.IsDeleted == false);
            if (discount != null)
            {
                discount.IsDeleted = true;

                unit.DiscountRepository.Update(Id, discount);

                result.IsPassed = true;
                result.Data = discount;
                return Ok(result);
            }
            result.IsPassed = false;
            result.Data = "No Discount Exist With This Id";
            return BadRequest(result);
        }

        [HttpGet("discountdetails/{Id}")]
        public IActionResult GetDiscountById(int Id)
        {
            ResultDTO result = new ResultDTO();
            Models.Discount discount = unit.DiscountRepository.GetById(Id, a => a.IsDeleted == false);
            if (discount != null)
            {
                Models.Product product = unit.ProductRepository
                    .GetById(discount.ProductId, a => a.IsDeleted == false);

                ReturnDiscountDTO discountDTO = new ReturnDiscountDTO();

                discountDTO.DiscountValue = discount.Value;
                discountDTO.DiscountStartDate = discount.StartDate;
                discountDTO.DiscountEndDate = discount.EndDate;
                discountDTO.ProductPriceAfterDiscount = discount.ProductPriceAfterDiscount;
                discountDTO.ProductName = discount.ProductName;
                discountDTO.ProductPrice = product.Price;
                discountDTO.ProdutId = discount.ProductId;

                result.IsPassed = true;
                result.Data = discountDTO;
                return Ok(result);
            }
            result.IsPassed = false;
            result.Data = "No Discount Exist With This Id";
            return BadRequest(result);
        }

        [HttpGet("discounts")]
        public IActionResult GetDiscount()
        {
            ResultDTO result = new ResultDTO();
            List<Models.Discount> discounts = unit.DiscountRepository.GetAll(d => d.IsDeleted == false);
            if (discounts != null)
            {
                List<Models.Product> products = new List<Models.Product>();
                List<ReturnDiscountDTO> returnDiscountDTOs = new List<ReturnDiscountDTO>();

                foreach (Models.Discount discount in discounts)
                {
                    Models.Product product = unit.ProductRepository
                        .GetById(discount.ProductId, p => p.IsDeleted == false);
                    ReturnDiscountDTO returnDiscountDTO = new ReturnDiscountDTO();

                    returnDiscountDTO.DiscountValue = discount.Value;
                    returnDiscountDTO.DiscountStartDate = discount.StartDate;
                    returnDiscountDTO.DiscountEndDate = discount.EndDate;
                    returnDiscountDTO.ProductPriceAfterDiscount = discount.ProductPriceAfterDiscount;
                    returnDiscountDTO.ProductName = discount.ProductName;
                    returnDiscountDTO.ProductPrice = product.Price;
                    returnDiscountDTO.ProdutId = discount.ProductId;

                    returnDiscountDTOs.Add(returnDiscountDTO);
                }

                result.IsPassed = true;
                result.Data = returnDiscountDTOs;
                return Ok(result);
            }
            result.IsPassed = false;
            result.Data = "No Discount Exist";
            return BadRequest(result);
        }
    }
}
