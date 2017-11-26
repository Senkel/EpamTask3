using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask3.Classes
{
    public struct PhoneNumber : IEquatable<PhoneNumber>
    {
        private string _phoneNumber;

        public string Phone { get { return _phoneNumber; } }

        public PhoneNumber(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return Phone;
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

        public static bool operator ==(PhoneNumber p1, PhoneNumber p2)
        {
            return (p1 as IEquatable<PhoneNumber>).Equals(p2);
        }

        public static bool operator !=(PhoneNumber p1, PhoneNumber p2)
        {
            return !(p1 == p2);
        }

        public override int GetHashCode()
        {
            return _phoneNumber.GetHashCode();
        }
    }
}
