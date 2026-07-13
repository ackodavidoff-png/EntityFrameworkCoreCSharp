using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductShop.DTOs.Import
{
    public class ImportCategoriesDTO
    {
        [JsonProperty("Name")]
        [Required]
        public string Name { get; set; } = null!;
    }
}
