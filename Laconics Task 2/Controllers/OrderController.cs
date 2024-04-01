using LaconicsCrm.webapi.Data;
using LaconicsCrm.webapi.Models.Domain;
using LaconicsCrm.webapi.Repositories;
using LaconicsCrm.webapi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaconicsCrm.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly LaconicsDatabaseContext laconicsDatabaseContext;
        private readonly IOrderRepository orderRepository;

        public OrderController(LaconicsDatabaseContext laconicsDatabaseContext, IOrderRepository orderRepository)
        {
            this.laconicsDatabaseContext = laconicsDatabaseContext;
            this.orderRepository = orderRepository;
        }

        //get all orders
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderDomain = await orderRepository.GetAllAsync(); //get data from database
            return Ok(orderDomain);
        }

        [HttpGet]
        [Route("{id:Guid}/orders")]
        public async Task<IActionResult> GetOrdersForCustomer([FromRoute] Guid id)
        {
            var ListOrders = await orderRepository.GetByCustomerIdAsync(id);
            foreach (var elm in ListOrders)
            {
                elm.Products = await orderRepository.GetProductFromOrderIdAsync(elm.orderId);


            }

            if (ListOrders == null)
            {
                return NotFound();
            }

           // char=
         //   var orders = null;// customer.Orders;

            return Ok(ListOrders);
        }


        //get order by id (single order)
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            // var order = laconicsDatabaseContext.Orders.Find(id);
            var order = await orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        //create new order
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            var orderModel = new Order
            {
                date = order.date,
                totalAmount = order.totalAmount
            };
            orderModel = await orderRepository.CreateAsync(orderModel);
            laconicsDatabaseContext.SaveChanges();

            //return CreatedAtAction(nameof(GetByIdAsync), new { id = orderModel.orderId }, orderModel);
            return Ok(orderModel);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] Order order)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    // Add the order to the database
        //    order = await orderRepository.CreateAsync(order);
        //    laconicsDatabaseContext.SaveChanges();

        //    // Return the created order
        //  //  return CreatedAtAction(nameof(GetByIdAsync), new { id = order.orderId }, order);]
        //  return Ok("");
        //}


        //update order
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] Order order)
        {
            var orderModel = new Order
            {
                date = order.date,
                totalAmount = order.totalAmount
            };

            orderModel = await orderRepository.UpdateAsync(id, order);

            if (orderModel == null)
            {
                return NotFound();
            }

            return Ok(orderModel);
        }

        //delete order
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var customerModel = await orderRepository.DeleteAsync(id);

            if (customerModel == null)
            {
                return NotFound();
            }
            return Ok(customerModel);
        }


    }
}
