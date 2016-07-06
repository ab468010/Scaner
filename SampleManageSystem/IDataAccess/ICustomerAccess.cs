using System.Collections.Generic;
using Model;

namespace IDataAccess
{
    public  interface ICustomerAccess:IBaseAccess<Model.Customer>
    {
        IList<Customer> Selectcustomer();
        Customer Scount();
        IList<Customer> GoPage(int number);
       
        IList<Customer> ScustomerId();
        bool InCustomer(Customer customer);
        bool SelectContact(int customerid);
        bool DeleteCus(int customerid);
    }
}
