using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Classes
{
    class PhoneNumber
    {
        private string _phoneNumber;

        public string Value
        {
            get { return _phoneNumber; }
        }

        public PhoneNumber(string phoneNumber)
        {
            this._phoneNumber = phoneNumber;
        }
        
    }
}
