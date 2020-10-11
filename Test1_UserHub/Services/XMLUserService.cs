using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Test1_UserHub.Models;

namespace Test1_UserHub.Services
{
    public class XMLUserService
    {
        public XMLUserService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public string userFile
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "XMLStorageFile.xml"); }
        }   
        public IEnumerable<User> getAllUsers()
        {
            List<User> exportUser = new List<User>();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(userFile);
            if (dataSet.Tables.Count > 0)
            {
                exportUser = dataSet.Tables[0].AsEnumerable()
                .Select(dataRow => new User
                {
                    ID = dataRow.Field<string>("id"),
                    Name = dataRow.Field<string>("name"),
                    Surname = dataRow.Field<string>("surname"),
                    Email = dataRow.Field<string>("email"),
                    Cellphone = dataRow.Field<string>("cellphone"),
                }).ToList();
            }
            return exportUser;
        }
        public void addUser(User user)
        {
            Guid obj = Guid.NewGuid();
            XDocument xDocument = XDocument.Load(userFile);
            XElement xElement = new XElement("user",
                new XElement("id",obj.ToString()),
                new XElement("name",user.Name),
                new XElement("surname",user.Surname),
                new XElement("email",user.Email),
                new XElement("cellphone",user.Cellphone)
                );
            xDocument.Root.Add(xElement);
            xDocument.Save(userFile);
        }

        public void removeUser(string id)
        {
            XDocument xDocument = XDocument.Load(userFile);
            XElement xElement = xDocument.Descendants("user").FirstOrDefault(i => i.Element("id").Value == id);
            if(xElement != null)
            {
                xElement.Remove();
                xDocument.Save(userFile);
            }
        }
        
        public void editUser(User user)
        {
            XDocument xDocument = XDocument.Load(userFile);
            XElement xElement = xDocument.Descendants("user").FirstOrDefault(i => i.Element("email").Value == user.Email);
            if (xElement != null)
            {
                xElement.Element("name").Value = user.Name;
                xElement.Element("surname").Value = user.Surname;
                xElement.Element("email").Value = user.Email;
                xElement.Element("cellphone").Value = user.Cellphone;
                xDocument.Save(userFile);
            }
        }
        public User getUser(string id)
        {
            User user = new User();
            XDocument xDocument = XDocument.Load(userFile);
            XElement xElement = xDocument.Descendants("user").FirstOrDefault(i => i.Element("id").Value == id);
            if (xElement != null)
            {
                user.ID = xElement.Element("id").Value.ToString();
                user.Name = xElement.Element("name").Value.ToString();
                user.Surname = xElement.Element("surname").Value.ToString();
                user.Email = xElement.Element("email").Value.ToString();
                user.Cellphone = xElement.Element("cellphone").Value.ToString();
                return user;
            }
            return null;
        }
    }
}
