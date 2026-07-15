using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProductShop.DTOs.Import
{
    [XmlType("User")]
    public class ImportUsersDTO
    {
        [Required]
        [XmlElement("firstName")]
        public string FirstName { get; set; }
        [Required]
        [XmlElement("lastName")]
        public string LastName { get; set; }
        [XmlElement("age")]
        public int Age { get; set; }
    }
}
