using System;
using System.Collections.Generic;
using Model;

namespace Logics
{
    public class CustomerLogics:BaseLogics<Model.Customer>
    {
        private const string _Type = "CustomerAccess";
        private IDataAccess.ICustomerAccess _Dal;

        public CustomerLogics() : base(_Type) 
        {
            _Dal = base.dal as IDataAccess.ICustomerAccess;

            if (_Dal == null)
            {
                throw new NullReferenceException(_Type);
            }
        }
        public IList<Customer> Selectcustomer()
        {
            return _Dal.Selectcustomer();
        }
 
        public Customer Scount()
        {
            return _Dal.Scount();
        }
        
        public IList<Customer> GoPage(int number)
        {
            return _Dal.GoPage(number);
        }
      
      public IList<Customer> ScustomerId()
        {
            return _Dal.ScustomerId();
        }
        public bool InCustomer(Customer customer)
        {
            return _Dal.InCustomer(customer);
        }
        public bool DeleteCustomer(int customerid)
        {
            if (_Dal.SelectContact(customerid))
            {
                return false;
            }
            else
            {
                return _Dal.DeleteCus(customerid);
            }
        }
    }
}
