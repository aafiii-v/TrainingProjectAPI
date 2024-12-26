using Microsoft.AspNetCore.Mvc;
using TrainingProjectAPI.Models.DB;
using TrainingProjectAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrainingProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }


        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult Get()
        {
            var CustomerList = _customerService.GetListCustomer();
            return Ok(CustomerList);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var CustomerId = _customerService.GetById(id);
            return Ok(CustomerId);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            var insertCustomer = _customerService.CreateCustomer(customer);
            if (insertCustomer)
            {
                return Ok("Insert Customer Succes");
            }
            return BadRequest("insert Customer Failed");
        }

        // PUT api/<CustomerController>/5
        [HttpPut]
        public IActionResult Put(Customer customer)
        {
            try
            {
                var updateCustomer = _customerService.UpdateCustomer(customer);
                if (updateCustomer)
                {
                    return Ok("Update Customer Succes");
                }

                return BadRequest("insert Customer Failed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
                throw;
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var deleteCustomer = _customerService.DeleteCustomer(id);
                if (deleteCustomer)
                {
                    return Ok("Delete Customer Succes");
                }


                return NotFound("Data tidak ditemukan!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
                throw;
            }
        }
    }
}
