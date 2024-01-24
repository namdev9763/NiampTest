using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NiampList.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
       
        [Required]
        public string CategoryName { get; set; }

    }
}