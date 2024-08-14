using Crudd2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Crudd2.ViewModels
{
    public class BookFormViewModel
    {

        public int Id { set; get; }

        [Required]
        [MaxLength(250)]
        public string Title { set; get; }

        [Required]
        [MaxLength(128)]
        public string Author { set; get; }

        [Required]
        [MaxLength(1000)]
        public string Description { set; get; }

        //to relation 
        [Display(Name ="Category")]
        public byte CategoryId { set; get; }

        public IEnumerable<Category> Categories { get; set; }
    }
}