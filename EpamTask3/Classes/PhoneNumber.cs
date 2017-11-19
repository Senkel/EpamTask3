using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Classes
{
    class PhoneNumber: IEquatable<PhoneNumber>
    {
        private string _phoneNumber;
        
        public PhoneNumber(string phoneNumber)
        {
           _phoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return _phoneNumber;
        }

        public override bool Equals(object o)
        {
            if (o is PhoneNumber)
            {
                return _phoneNumber == ((PhoneNumber)o)._phoneNumber;
            }
            else { return false; }
        }

        public bool Equals(PhoneNumber other)
        {
            return _phoneNumber == other._phoneNumber;
        }

        public override int GetHashCode()
        {
            return _phoneNumber.GetHashCode();
        }
    }
}
