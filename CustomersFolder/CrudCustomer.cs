using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheHotel.InterfaceFolder;
using TheHotel.MenuFolder;
using TheHotel.TablesFolder;

namespace TheHotel.CustomersFolder
{
    public class CrudCustomer : ICrudOperations
    {
        CustomerInformationMenu customerInfoMenu = new CustomerInformationMenu();
        public void Create()
        {
            customerInfoMenu.ShowCustomerInfoMenu();
        }
        public void Read()
        {

        }
        public void Update()
        {

        }
        public void Delete()
        {

        }
        //public int CustomerId { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public int SSN { get; set; }
        //public int PhoneNumber { get; set; }
        //public string Email { get; set; }
        //public string? StreetAddress { get; set; }
        //public int? PostalCode { get; set; }
        //public string? City { get; set; }
        //public string? Country { get; set; }
    }
}
