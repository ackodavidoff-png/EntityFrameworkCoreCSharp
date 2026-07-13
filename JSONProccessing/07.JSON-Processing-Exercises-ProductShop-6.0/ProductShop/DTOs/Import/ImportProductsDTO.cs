using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductShop.DTOs.Import
{
    public class ImportProductsDTO
    {
        [Required]
        [JsonProperty("Name")]
        public string Name { get; set; } = null!;
        [JsonRequired]
        [JsonProperty("Price")]
        public decimal Price { get; set; }
        [JsonRequired]
        [JsonProperty("SellerId")]
        public int SellerId { get; set; }
        [JsonProperty("BuyerId")]
        public int? BuyerId { get; set; }
    }
}
