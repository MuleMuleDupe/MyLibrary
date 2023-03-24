using MyLibrary.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }

        [Display(Name = "ReleaseDate")]
        [Required(ErrorMessage = "ReleaseDate is required!")]
        public int ReleaseDate { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required!")]

        public double Price { get; set; }

        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Genre is required!")]
        public Genre Genre { get; set; }

        [Display(Name = "ImageURL")]
        [Required(ErrorMessage = "ImageURL is required!")]
        public string ImageURL { get; set; }

        //Author Relationship
        [Display(Name="AuthorId")]
        [Required(ErrorMessage = "AuthorId is required!")]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}
