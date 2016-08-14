using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    public class PlayerAdd
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Player Number")]
        public int Number { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Player Name")]
        public string Name { get; set; }

        [Required, StringLength(3)]
        [Display(Name = "Bats/Throws")]
        public string BT { get; set; }

        [Display(Name = "Players Height")]
        public string Height { get; set; }

        [Display(Name = "Players Weight")]
        public int Weight { get; set; }

        [Display(Name = "Position")]
        public string Position { get; set; }

        [Display(Name = "Team name")]
        public string TeamName { get; set; }

        public PlayerAdd()
        {
            Number = 0;
            Name = "";
            BT = "/";
            Height = "";
            Weight = 0;
            Position = "";
            TeamName = ""; 
        }
    }

    public class PlayerBase : PlayerAdd
    {
        [Key]
        public int Id { get; set; }
    }

    public class PlayerSearchForm
    {
        [Required, StringLength(200)]
        [Display(Name = "All or part of player name")]
        public string SearchName { get; set; }

    }

    public class PlayerAddForm
    {
        [Key]
        public int Id { get; set; }

        public int TeamId { get; set; }
        public string TeamName { get; set; }


        [Required]
        [Display(Name = "Player Number")]
        public int Number { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Player Name")]
        public string Name { get; set; }

        [Required, StringLength(3)]
        [Display(Name = "Bats/Throws")]
        public string BT { get; set; }

        [Required, Display(Name = "Players Height")]
        public string Height { get; set; }

        [Required, Display(Name = "Players Weight")]
        public int Weight { get; set; }

        [Required, Display(Name = "Position")]
        public string Position { get; set; }

    }
}