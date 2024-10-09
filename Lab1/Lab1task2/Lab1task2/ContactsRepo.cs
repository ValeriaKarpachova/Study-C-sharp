using System.Collections.Generic;
using System.IO;
using Lab1task2.Models;
using Newtonsoft.Json;

namespace Lab1task2
{
    internal class ContactsRepo
    {

        private List<Contact> contacts = new List<Contact>();

        public List<Contact> GetAll() => contacts;

        public void Add(Contact contact)
        {
            contacts.Add(contact);
        }

        public void SaveToFile(string filePath)
        {
            var json = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public void LoadFromFile(string filePath)
        {
            string json = File.ReadAllText(filePath);
            contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
        }
    }
}
