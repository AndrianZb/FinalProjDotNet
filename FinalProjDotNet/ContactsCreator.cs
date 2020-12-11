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

        override
        public string ToString()
        {
            return  FirstName + " " + LastName + " " + PhoneNum + " " + Email;
        }
        
    }

}
