using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjDotNet
{
    class ContactsCreator
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNum { get; set; }
        public string Email { get; set; }

        //private string str;

        public ContactsCreator() { }
        public ContactsCreator(string f, string l, string p, string e)
        {
            FirstName = f;
            LastName = l;
            PhoneNum = p;
            Email = e;
        }

        public string toString()
        {
            return  FirstName + " " + LastName + " " + PhoneNum + " " + Email;
           
        }
        
    }

}
