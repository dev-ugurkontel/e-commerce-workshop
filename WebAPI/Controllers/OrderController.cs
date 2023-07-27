using Business.Abstract;
using Core.Utils;
using Entities.Surrogate.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v1/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("List_Order")]
        public IActionResult List_Order()
        {
            var result = _orderService.GetAll();
            if (result.Status == ResultStatus.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Route("Save_Order")]
        [HttpPost]
        public IActionResult Save_Order(OrderRequest order)
        {
            _orderService.Add(order);
            return Ok(order);
        }

        [Route("Find_Order/{id}")]
        [HttpGet]
        public IActionResult Find_Order(int id)
        {
            var order = _orderService.Get(id);
            if (order.Status == ResultStatus.Error)
            {
                return NotFound();
            }
            else if (order.Status == ResultStatus.Success)
            {
                return Ok(order);
            }
            else
            {
                return BadRequest();
            }

        }

        [Route("Update_Order/{id}")]
        [HttpPut]
        public IActionResult Update_Order(int id, OrderRequest order)
        {
            _orderService.Update(id, order);
            return Ok(order);
        }

        [Route("Delete_Order/{id}")]
        [HttpDelete]
        public IActionResult Delete_Order(int id)
        {
            _orderService.Delete(id);
            return NoContent();
        }
    }
}
