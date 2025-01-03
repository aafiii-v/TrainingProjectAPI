using Microsoft.AspNetCore.Mvc;
using TrainingProjectAPI.Models.DB;
using TrainingProjectAPI.Services;

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

        // GET: api/<ItemController>
        [HttpGet]
        public ActionResult GetItem()
        {
            var ItemList = _itemService.GetListItem();
            return Ok(ItemList);
        }

        // POST api/<ItemController>
        [HttpPost]
        public IActionResult CreateItem([FromBody] Item item)
        {
            if (string.IsNullOrWhiteSpace(item.NamaItem))
                return BadRequest("Nama Item tidak boleh kosong atau hanya spasi");

            if (item.NamaItem.Length > 100)
                return BadRequest("Nama Item Maksimal 100 karakter");

            if (item.QTY <= 0)
                return BadRequest("QTY harus lebih dari 0");
            
            if (item.TglExpired <= DateTime.Now)
                return BadRequest("Tanggal harus lebih besar dari hari ini");

            if (string.IsNullOrWhiteSpace(item.Supplier))
                return BadRequest("Supplier tidak boleh kosong atau hanya spasi");

            if (item.Supplier.Length > 100)
                return BadRequest("Supplier Maksimal 100 karakter");

            if (item.AlamatSupplier.Length > 100)
                return BadRequest("Alamat Supplier Maksimal 100 Karakter!");

            var cekNamaItem = _itemService.GetListItem().FirstOrDefault(x => x.NamaItem == item.NamaItem);
            if (cekNamaItem != null)
                return BadRequest("Nama Item sudah ada");

            var insertItem = _itemService.CreateItem(item);
            if (insertItem)
            {
                return Ok("Insert Item Success");
            }
            return BadRequest("Insert Item Failed");
        }

        // PUT api/<ItemController>
        [HttpPut]
        public IActionResult UpdateItem([FromBody] Item item)
        {
            if (string.IsNullOrWhiteSpace(item.NamaItem))
                return BadRequest("Nama Item tidak boleh kosong atau hanya spasi");

            if (item.NamaItem.Length > 100)
                return BadRequest("Nama Item tidak boleh lebih dari 100 karakter");

            if (item.QTY < 0)
                return BadRequest("QTY harus lebih dari 0");

            if (item.TglExpired <= DateTime.Now)
                return BadRequest("Tanggal kadaluarsa harus lebih besar dari hari ini");

            if (string.IsNullOrWhiteSpace(item.Supplier))
                return BadRequest("Supplier tidak boleh kosong atau hanya spasi");

            if (item.Supplier.Length > 100)
                return BadRequest("Supplier Maksimal 100 karakter");

            var cekNamaItem = _itemService.GetListItem().FirstOrDefault(x => x.NamaItem == item.NamaItem && x.Id != item.Id);
            if (cekNamaItem != null)
                return BadRequest("Nama Item sudah ada");

            var existingItem = _itemService.GetListItem().FirstOrDefault(existId => existId.Id == item.Id);
            if (existingItem == null)
                return NotFound("Item dengan ID tersebut tidak ditemukan");

            try
            {
                var updateItem = _itemService.UpdateItem(item);
                if (updateItem)
                {
                    return Ok("Update Item Success");
                }

                return BadRequest("Update Item Failed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
                throw;
            }
        }

        // DELETE api/<ItemController>/5
        [HttpDelete]
        public IActionResult DeleteItem(int id)
        {
            if (id <= 0)
                return BadRequest("ID tidak valid");

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