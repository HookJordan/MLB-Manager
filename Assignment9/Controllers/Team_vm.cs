using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment9.Controllers
{
    public class TeamAdd
    { 
        [Display(Name = "Team Name")]
        public string Name { get; set; }

        [Display(Name = "League")]
        [Required, StringLength(2)]
        public string League { get; set; }

        [Display(Name = "Games Played")]
        public int Games { get; set; }

        [Display(Name = "Home Runs")]
        public int HR { get; set; }

        [Display(Name = "Runs Batted In")]
        public int RBI { get; set; }

        [Display(Name = "Strike Outs")]
        public int StrikeOut { get; set; }

        [Display(Name = "Batting Average")]
        public double BattingAvg { get; set; }

        public List<PlayerBase> Players { get; set; }
        public List<CoachBase> Coaches { get; set; }

        public TeamAdd()
        {
            Players = new List<PlayerBase>();
            Coaches = new List<CoachBase>(); 
        }
    }

    public class TeamBase : TeamAdd
    {
        [Key]
        public int Id { get; set; }
    }

    public class TeamSearchForm
    {
        [Required, StringLength(200)]
        [Display(Name = "All or part of team name")]
        public string SearchName { get; set; }
    }

    public class TeamAddForm
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Team Name")]
        public string Name { get; set; }

        [Display(Name = "League")]
        [Required, StringLength(2)]
        public string League { get; set; }

        [Display(Name = "Games Played")]
        public int Games { get; set; }

        [Display(Name = "Home Runs")]
        public int HR { get; set; }

        [Display(Name = "Runs Batted In")]
        public int RBI { get; set; }

        [Display(Name = "Strike Outs")]
        public int StrikeOut { get; set; }

        [Display(Name = "Batting Average")]
        public double BattingAvg { get; set; }
    }
}