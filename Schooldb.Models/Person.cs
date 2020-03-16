using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Schooldb.Models
{
    public class Person
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name ="Geboortedatum")]
        [DataType(DataType.Date, ErrorMessage = "Dit is geen geldige datum")]
        public DateTime? DateOfBirth { get; set; } // Datetime kan null zijn
        
        [Display(Name= "Naam")]
        [Required(ErrorMessage = "Naam is verplicht")]
        public string Name { get; set; }
/*
        [Display(Name = "Geslacht")]
        [Required(ErrorMessage = "Verplichte keuze.")]
        [EnumDataType(typeof(GenderType), ErrorMessage = "{0}")]*/
        [Display(Name = "Geslacht")]
        [EnumDataType(typeof(GenderType), ErrorMessage = "{0} is geen geldige keuze.")]
        [Range(0, 1, ErrorMessage = "ongeldige keuze")]
        public GenderType Gender { get; set; }
        //concrete type for Student and Teacher
        public enum GenderType
        {
            [Display(Name ="Mannelijk")]
            Male = 0,
            [Display(Name = "Vrouwelijk")]
            Female = 1
        }
        [ScaffoldColumn(false)]
        public string ImageUrl { get { return $"./images/{this.Name}.jpg"; } }
    }
}
