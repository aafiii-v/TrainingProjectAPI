using TrainingProjectAPI.Models;
using TrainingProjectAPI.Models.DB;

namespace TrainingProjectAPI.Services
{
    public class CustomerService
    {
        private readonly ApplicationContext _context;

        public CustomerService(ApplicationContext context)
        {
            _context = context;
        }

        public List<Customer> GetListCustomer()
        {
            var datas = _context.Customers.ToList();
            return datas;
        }
    }
}
