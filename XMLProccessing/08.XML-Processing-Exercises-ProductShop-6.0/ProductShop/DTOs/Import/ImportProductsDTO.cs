using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
/*
 * <Product>
        <name>Intex</name>
        <price>3.00</price>
        <sellerId>0</sellerId>
        <buyerId>0</buyerId>
    </Product>
 */
namespace ProductShop.DTOs.Import
{
    [XmlType("Product")]
    public class ImportProductsDTO
    {
        [Required]
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("price")]
        public decimal Price { get; set; }
        [Required]
        [XmlElement("sellerId")]
        public int SellerId { get; set; }
        [XmlElement("buyerId")]
        public int BuyerId { get; set; }
    }
}
