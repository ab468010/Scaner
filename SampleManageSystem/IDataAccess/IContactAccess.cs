using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IDataAccess
{
     public  interface IContactAccess:IBaseAccess<Model.Contact>
    {
       IList<Contact> Selectcontact(int number);

        bool Incontact(Contact contact);
        bool UpdateContact(Contact contact);
        IList<Contact> Scontact(int customerid);
        long GetContactCount();
    }
}
