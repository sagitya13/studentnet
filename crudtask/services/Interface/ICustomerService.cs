using crudtask.Models;

namespace crudtask.services.Interface
{
    public interface ICustomerService
    {
        List<string> GetCustomers();
        Customer GetCustomerById(string id);
        object CreateCustomer(Customer customer);

        Customer UpdateCustomer(Customer customer, string id);
        void DeleteCustomer(string id);
    }
}
