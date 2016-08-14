using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace Assignment9.Models
{
    // Add your design model classes below

    // Follow these rules or conventions:

    // To ease other coding tasks, the name of the 
    //   integer identifier property should be "Id"
    // Collection properties (including navigation properties) 
    //   must be of type ICollection<T>
    // Valid data annotations are pretty much limited to [Required] and [StringLength(n)]
    // Required to-one navigation properties must include the [Required] attribute
    // Do NOT configure scalar properties (e.g. int, double) with the [Required] attribute
    // Initialize DateTime and collection properties in a default constructor

    public class RoleClaim
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(2)]
        public string League { get; set; }
        public int Games { get; set; }
        public int HR { get; set; }
        public int RBI { get; set; }
        public int StrikeOut { get; set; }
        public double BattingAvg { get; set; }

        public ICollection<Player> Players { get; set; }
        public ICollection<Coach> Coaches { get; set; }

        public Team()
        {
            Players = new List<Player>();
            Coaches = new List<Coach>();
        }
    }

    public class Player
    {
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }
        [Required, StringLength(200)]
        public string Name { get; set; }
        [Required, StringLength(3)]
        public string BT { get; set; }
        public string Height { get; set; }
        public int Weight { get; set; }
        public string Position { get; set; }

        public string TeamName { get; set; } 

        public Player()
        {
        }
    }

    public class Coach
    {
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }
        [Required, StringLength(200)]
        public string Name { get; set; }
        public string Position { get; set; }

        public string TeamName { get; set; } 
    }
}
