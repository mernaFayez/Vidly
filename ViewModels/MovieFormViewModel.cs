using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        //Instead of using Movie model, we added all movie properties here and make them nullable so the form won't be populated 
        //#pure view model 
        public int? Id { get; set; }

        [Required] // Nullable by default
        [StringLength(255)]
        public string Name { get; set; }

        //public Genre Genre { get; set; } we don't need this cause all we capture in the form is the genere id

        [Required]
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

       // public DateTime DateAdded { get; set; } we don'y capture this in the form, it's generated when saving

        [Range(1, 20)]
        [Display(Name = "Number in Stock")]
        [Required]
        public byte? NumInStock { get; set; }

        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Movie" : "Add Movie";
            }
        }

        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            movie.NumInStock = movie.NumInStock;
            GenreId = movie.GenreId;
        }
    }
}