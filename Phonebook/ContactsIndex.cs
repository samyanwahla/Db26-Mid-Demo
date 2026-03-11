using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phonebook
{
    public partial class ContactsIndex : Form
    {
        private BindingList<Contact> contacts;

        public ContactsIndex()
        {
            InitializeComponent();
        }

        private void ContactsIndex_Load(object sender, EventArgs e)
        {
            // Demo data
            contacts = new BindingList<Contact> {
                new Contact { FirstName = "John", LastName = "Doe", Email = "john@example.com", PhoneNumber = "1234567890", Address = "123 Main St" },
                new Contact { FirstName = "Jane", LastName = "Smith", Email = "jane@example.com", PhoneNumber = "0987654321", Address = "456 Elm St" }
            };
            dgvContacts.DataSource = contacts;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.ToLower();
            var filteredList = contacts.Where(c => c.FirstName.ToLower().Contains(searchTerm) ||
                                                   c.LastName.ToLower().Contains(searchTerm) ||
                                                   c.Email.ToLower().Contains(searchTerm) ||
                                                   c.PhoneNumber.Contains(searchTerm) ||
                                                   c.Address.ToLower().Contains(searchTerm)).ToList();
            dgvContacts.DataSource = new BindingList<Contact>(filteredList);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            dgvContacts.DataSource = contacts;
        }
    }

    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}