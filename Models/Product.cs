namespace ecomapp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        public long ProductId { get; set; }

        public long? CategoryId { get; set; }

        [StringLength(250)]
        public string ProductName { get; set; }

        public decimal? ProductPrice { get; set; }

        public virtual Category Category { get; set; }
        public string ProductImagePath { get; set; }
    }
}
