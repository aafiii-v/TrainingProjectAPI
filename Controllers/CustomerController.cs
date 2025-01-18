using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TrainingProjectAPI.Models;
using TrainingProjectAPI.Models.DB;
using TrainingProjectAPI.Models.DTO;
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

        [HttpGet("GetListCustomer")]
        public IActionResult Get()
        {
            try
            {
                var CustomerList = _customerService.GetListCustomer();
                var Response = new GeneralResponse
                {
                    StatusCode = "01",
                    StatusDesc = "Success",
                    Data = CustomerList
                };
                return Ok(Response);
            }
            catch (Exception ex)
            {
                var Response = new GeneralResponse
                {
                    StatusCode = "99",
                    StatusDesc = "Failed | " + ex.Message.ToString(),
                    Data = null
                };
                return BadRequest(Response);
            }
        }

        // GET api/<CustomerController>/5
        [HttpGet]
        [Route("GetCustomerById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var CustomerId = _customerService.GetById(id);
                var Response = new GeneralResponse
                {
                    StatusCode = "01",
                    StatusDesc = "Success",
                    Data = CustomerId
                };
                return Ok(Response);
            }
            catch (Exception ex)
            {
                var Response = new GeneralResponse
                {
                    StatusCode = "01",
                    StatusDesc = "Failed | " + ex.Message.ToString(),
                    Data = null
                };
                return BadRequest(Response);
            }
        }

        // POST api/<CustomerController>
        [HttpPost("InsertDataCustomer")]
        public IActionResult Post(CustomerRequestDTO customer)
        {
            try
            {
                var insertCustomer = _customerService.CreateCustomer(customer);
                if (insertCustomer)
                {
                    var ResponseSuccess = new GeneralResponse
                    {
                        StatusCode = "01",
                        StatusDesc = "Insert Customer Succes",
                        Data = customer
                    };
                    return Ok(ResponseSuccess);
                }

                var ResponseFailed = new GeneralResponse
                {
                    StatusCode = "02",
                    StatusDesc = "Insert Customer Failed",
                    Data = customer
                };
                return BadRequest(ResponseFailed);
            }
            catch (Exception ex)
            {
                var ResponseFailed = new GeneralResponse
                {
                    StatusCode = "99",
                    StatusDesc = "Failed | " + ex.Message.ToString(),
                    Data = null
                };
                return BadRequest(ResponseFailed);
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut("UpdateCustomer")]
        public IActionResult Put(int Id, CustomerRequestDTO customer)
        {
            try
            {
                var updateCustomer = _customerService.UpdateCustomer(Id, customer);
                if (updateCustomer)
                {
                    var ResponseSuccess = new GeneralResponse
                    {
                        StatusCode = "01",
                        StatusDesc = "Update Customer Succes",
                        Data = customer
                    };
                    return Ok(ResponseSuccess);
                }

                var ResponseFailed = new GeneralResponse
                {
                    StatusCode = "02",
                    StatusDesc = "insert Customer Failed",
                    Data = customer
                };
                return BadRequest(ResponseFailed);
            }
            catch (Exception ex)
            {
                var ResponseFailed = new GeneralResponse
                {
                    StatusCode = "99",
                    StatusDesc = "Failed | " + ex.Message.ToString(),
                    Data = null
                };
                return BadRequest(ResponseFailed);
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("DeleteCustomer")]
        public IActionResult Delete(int id)
        {
            try
            {
                var deleteCustomer = _customerService.DeleteCustomer(id);
                if (deleteCustomer)
                {
                    var ResponseSuccess = new GeneralResponse
                    {
                        StatusCode = "01",
                        StatusDesc = "Delete Customer Succes",
                        Data = deleteCustomer
                    };
                    return Ok(ResponseSuccess);
                }
                var ResponseFailed = new GeneralResponse
                {
                    StatusCode = "02",
                    StatusDesc = "Data tidak ditemukan!",
                    Data = deleteCustomer
                };
                return NotFound(ResponseFailed);
            }
            catch (Exception ex)
            {
                var ResponseFailed = new GeneralResponse
                {
                    StatusCode = "99",
                    StatusDesc = "Failed | " + ex.Message.ToString(),
                    Data = null
                };
                return BadRequest(ResponseFailed);
            }
        }
    }
}
