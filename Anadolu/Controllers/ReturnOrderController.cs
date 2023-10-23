using Anadolu.DTO;
using Anadolu.Models;
using Anadolu.Repository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anadolu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnOrderController : ControllerBase
    {

        private readonly IUnitOfWork unit;
        public ReturnOrderController(IUnitOfWork _unit)
        {
            unit = _unit;
        }

        [HttpGet("ReturnOrders")]
        public IActionResult GetAllReturnOrders()
        {

            ResultDTO result = new ResultDTO();
            List<ReturnOrder> returnOrders = unit.ReturnOrderRepository
                .GetAll(o => o.IsDeleted == false);

            if (returnOrders != null)
            {
                List<ReturnOrderDTO> returnOrderDTOs = new List<ReturnOrderDTO>();
                foreach (ReturnOrder returnOrder in returnOrders)
                {
                    ReturnOrderDTO dto = new ReturnOrderDTO();
                    dto.OrderId = returnOrder.Id;/// OrderId  كانت كدا وغيرتها عشان عامله ايرور
                    dto.UserId = returnOrder.UserId;
                    dto.TotalPrice = returnOrder.TotalPrice;
                    dto.CustomerName = returnOrder.Name;
                    dto.Date = dto.Date;

                    returnOrderDTOs.Add(dto);
                }
                result.IsPassed = true;
                result.Data = returnOrderDTOs;
                return Ok(result);
            }
            result.IsPassed = false;
            result.Data = "No ReturnOrders Exist";
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddReturnOrders()
        {
            return Ok();
        }
    }
}
