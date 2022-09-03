﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }

        [Range(1, 100, ErrorMessage = "Count msut be less then 100")]
        [Required]
        public int Count { get; set; }

        public string UserId { get; set; }

    }
}
