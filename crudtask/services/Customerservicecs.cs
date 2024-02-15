using crudtask.Models;
using crudtask.Repository.interfaces;
using crudtask.services.Interface;

namespace crudtask.services
{
    public class Customerservicecs : ICustomerService

    {
        ICustomerRepo _customerRepo;
        public Customerservicecs(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

       
        public Customer GetCustomerById(string id)
        {
            return _customerRepo.GetCustomerById(id);
        }

        public Customer CreateCustomer(Customer customer)
        {

            return _customerRepo.CreateCustomer(customer);
        }


        public Customer UpdateCustomer(Customer customer, string id)
        {

            return _customerRepo.UpdateCustomer(customer, id);
        }


        public void DeleteCustomer(string id)
        {

            _customerRepo.DeleteCustomer(id);
        }

        public List<string> GetCustomers()
        {

            throw new NotImplementedException();
        }

        object ICustomerService.CreateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }


}

