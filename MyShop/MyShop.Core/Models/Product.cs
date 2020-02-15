using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Product
    {
        public string Id { get; set; }
        [StringLength(50)]
        [Required]
        [DisplayName("Nombre Producto")]
        public string Name { get; set; }
        [StringLength(450)]
        [DisplayName("Descripcion")]
        public string Description { get; set; }
        [DisplayName("Precio")]
        [Range(0,99999999.9999)]
        public decimal Price { get; set; }
        [DisplayName("Categoria")]
        public string Category { get; set; }
        [DisplayName("Image Path")]
        public string Image { get; set; }

        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }

    }
}
