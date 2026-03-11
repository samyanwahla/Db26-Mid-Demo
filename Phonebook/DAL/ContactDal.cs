using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace Phonebook.DAL
{
    public class ContactDal
    {
        private string connectionString = "Server=localhost;Database=phonebook;Uid=root;Pwd=12345;SslMode=None;";

        public List<Contact> GetAllContacts()
        {
            List<Contact> contacts = new List<Contact>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM contacts", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        contacts.Add(new Contact
                        {
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("Name"),
                            PhoneNumber = reader.GetString("PhoneNumber")
                        });
                    }
                }
            }
            return contacts;
        }

        public void AddContact(Contact contact)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO contacts (Name, PhoneNumber) VALUES (@Name, @PhoneNumber)", conn);
                cmd.Parameters.AddWithValue("@Name", contact.Name);
                cmd.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateContact(Contact contact)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE contacts SET Name=@Name, PhoneNumber=@PhoneNumber WHERE Id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", contact.Id);
                cmd.Parameters.AddWithValue("@Name", contact.Name);
                cmd.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteContact(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM contacts WHERE Id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}