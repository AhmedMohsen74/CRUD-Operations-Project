using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Crudd2.Models
{
    public class Book
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
        public byte CategoryId { set; get; }
        public Category Category { set; get; }

        public DateTime AddedOn { set; get; }
            
        public Book()
        {
            AddedOn = DateTime.Now;
        }
    
    }


}