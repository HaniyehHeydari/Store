﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Project2_Api.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ForeignKey("Basket")]
        public int BasketId { get; set; }
        public int Count { get; set; }
        public int price { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User User { get; set; } = default!;
        public virtual Product Product { get; set; } = default!;
        public virtual Basket Basket { get; set; } = default!;

    }
}