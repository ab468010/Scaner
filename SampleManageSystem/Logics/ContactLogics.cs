using System;
using Model;
using System.Collections.Generic;

namespace Logics
{
    public class ContactLogics:BaseLogics<Model.Contact>
    {
        private const string _Type = "ContactAccess";
        private IDataAccess.IContactAccess _Dal;

        public ContactLogics() : base(_Type)
        {
            _Dal = base.dal as IDataAccess.IContactAccess;

            if (_Dal == null)
            {
                throw new NullReferenceException(_Type);
            }
        }
        public IList<Contact> Selectcontact(int number)
        {
            return _Dal.Selectcontact(number);
        }
      
        public bool Incontact(Contact contact)
        {
            return _Dal.Incontact(contact);
        }
        public bool UpdateContact(Contact contact)
        {
            return _Dal.UpdateContact(contact);
        }
        public IList<Contact> Scontact(int customerid)
        {
            return _Dal.Scontact(customerid);
        }
        public long GetContactCount()
        {
            return _Dal.GetContactCount();
        }
    }
}
