using Microsoft.AspNetCore.Mvc;
using TrainingProjectAPI.Models.DB;
using TrainingProjectAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrainingProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }


        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult Get()
        {
            var ItemList = _itemService.GetListItem();
            return Ok(ItemList);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post(Item item)
        {
            var addItem = _itemService.CreateItem(item);
            if (addItem)
            {
                return Ok("Insert Item Succes");
            }
            return BadRequest("insert Item Failed");
        }

        // PUT api/<CustomerController>/5
        [HttpPut]
        public IActionResult Put(Item item)
        {
            try
            {
                var updateItem = _itemService.UpdateItem(item);
                if (updateItem)
                {
                    return Ok("Update Item Succes");
                }

                return BadRequest("insert Item Failed");
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
                var deleteItem = _itemService.DeleteItem(id);
                if (deleteItem)
                {
                    return Ok("Delete Item Succes");
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
