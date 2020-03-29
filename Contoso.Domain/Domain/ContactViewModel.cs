using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Domain
{
    public enum ContactStatus
    {
        Active = 0,
        Inactive = 1
    }

    public class ContactViewModel : BaseDomain
    {

        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int Status { get; set; }

        public string StatusDisplay
        {
            get
            {
                return ((ContactStatus)Status).ToString();
            }
        }
    }
}
