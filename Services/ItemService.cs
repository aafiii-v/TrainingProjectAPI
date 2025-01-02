using TrainingProjectAPI.Models;
using TrainingProjectAPI.Models.DB;

namespace TrainingProjectAPI.Services
{
    public class ItemService
    {
        private readonly ApplicationContext _context;

        public ItemService(ApplicationContext context)
        {
            _context = context;
        }

        public List<Item> GetListItem()
        {
            var itemDatas = _context.Items.ToList();
            return itemDatas;
        }

        public bool CreateItem(Item item)
        {
            try
            {
                _context.Items.Add(item);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateItem(Item item)
        {
            try
            {
                var itemDataOld = _context.Items.Where(update => update.Id == item.Id).FirstOrDefault();
                if (itemDataOld != null)
                {
                    itemDataOld.NamaItem = item.NamaItem;
                    itemDataOld.QTY = item.QTY;
                    itemDataOld.TglExpired = item.TglExpired;
                    itemDataOld.Supplier = item.Supplier;
                    itemDataOld.AlamatSupplier = item.AlamatSupplier;

                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteItem(int id)
        {
            try
            {
                var delItem = _context.Items.FirstOrDefault(Del => Del.Id == id);
                if (delItem != null)
                {
                    _context.Items.Remove(delItem);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
