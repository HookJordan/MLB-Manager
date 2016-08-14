using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment9.Controllers
{
    public class CoachAdd
    {

        [Required]
        [Display(Name = "Jersey Number")]
        public int Number { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Coaches name")]
        public string Name { get; set; }

        [Display(Name = "Coach position")]
        public string Position { get; set; }

        [Display(Name = "Team name")]
        public string TeamName { get; set; }
    }

    public class CoachBase : CoachAdd
    {
        [Key]
        public int Id { get; set; }
    }

    public class CoachSearchForm
    {
        [Required, StringLength(200)]
        [Display(Name = "All or part of coaches name")]
        public string SearchName { get; set; }

    }

    public class CoachAddForm
    {
        [Key]
        public int Id { get; set; }

        public int TeamId { get; set; }
        public string TeamName { get; set; }

        [Required]
        [Display(Name = "Jersey Number")]
        public int Number { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Coaches name")]
        public string Name { get; set; }

        [Display(Name = "Coach position")]
        public string Position { get; set; }

    }

}