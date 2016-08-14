using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Assignment9.Models;
using System.Security.Claims;
using System.IO;
using Excel;
using System.Data;
using System.Reflection;

namespace Assignment9.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // Declare a property to hold the user account for the current request
        // Can use this property here in the Manager class to control logic and flow
        // Can also use this property in a controller 
        // Can also use this property in a view; for best results, 
        // near the top of the view, add this statement:
        // var userAccount = new ConditionalMenu.Controllers.UserAccount(User as System.Security.Claims.ClaimsPrincipal);
        // Then, you can use "userAccount" anywhere in the view to render content
        public UserAccount UserAccount { get; private set; }

        public Manager()
        {
            // If necessary, add constructor code here

            // Initialize the UserAccount property
            UserAccount = new UserAccount(HttpContext.Current.User as ClaimsPrincipal);

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;

            //ds.Database.CreateIfNotExists();
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        public IEnumerable<PlayerBase> PlayersGetAll()
        {
            var players = ds.Players.OrderBy(name => name.TeamName).ThenBy(name => name.Name);

            return (players == null) ? null : Mapper.Map<IEnumerable<PlayerBase>>(players);
        }

        public PlayerBase PlayerGetById(int id)
        {
            var player = ds.Players.SingleOrDefault(pid => pid.Id == id);

            return (player == null) ? null : Mapper.Map<PlayerBase>(player);
        }

        public IEnumerable<PlayerBase> PlayerGetByName(string name)
        {
            var players = ds.Players.Where(p => p.Name.ToLower().Contains(name)).OrderBy(n => n.Name);

            return (players == null) ? null : Mapper.Map<IEnumerable<PlayerBase>>(players); 
        }

        public PlayerBase PlayerAdd(PlayerAdd player)
        {
            var team = ds.Teams.Include("Players").Include("Coaches").SingleOrDefault(n => n.Name == player.TeamName);

            if (team == null)
                return null; 
            else
            {
                //add the player to the database... 
                var newPlayer = ds.Players.Add(Mapper.Map<Player>(player));
                team.Players.Add(newPlayer); //add the player to the team

                ds.SaveChanges();

                return (newPlayer == null) ? null : Mapper.Map<PlayerBase>(newPlayer);
            }
        }

        public IEnumerable<TeamBase> TeamsGetAll()
        {
            var team = ds.Teams.Include("Players").Include("Coaches").OrderBy(t => t.League).ThenBy(t => t.Name);

            return (team == null) ? null : Mapper.Map<IEnumerable<TeamBase>>(team);
        }

        public TeamBase TeamGetById(int id)
        {
            var team = ds.Teams.Include("Players").Include("Coaches").SingleOrDefault(tid => tid.Id == id);

            return (null == team) ? null : Mapper.Map<TeamBase>(team);
        }

        public TeamBase TeamAdd(TeamAdd team)
        {
            var teamAdd = ds.Teams.Add(Mapper.Map<Team>(team));
            ds.SaveChanges(); 

            return (teamAdd == null) ? null : Mapper.Map<TeamBase>(teamAdd);  
        }

        public IEnumerable<TeamBase> TeamGetByName(string name)
        {
            var teams = ds.Teams.Include("Players").Where(n => n.Name.ToLower().Contains(name));

            return (teams == null) ? null : Mapper.Map<IEnumerable<TeamBase>>(teams); 
        }

        public IEnumerable<CoachBase> CoachesGetAll()
        {
            var coach = ds.Coaches.OrderBy(c => c.TeamName).ThenBy(n => n.Name);

            return (coach == null) ? null : Mapper.Map<IEnumerable<CoachBase>>(coach); 
        }

        public CoachBase CoachesGetById(int id)
        {
            var coach = ds.Coaches.SingleOrDefault(c => c.Id == id);

            return (coach == null) ? null : Mapper.Map<CoachBase>(coach);
        }

        public CoachBase CoachAdd(CoachAdd coach)
        {
            var team = ds.Teams.Include("Players").Include("Coaches").SingleOrDefault(n => n.Name == coach.TeamName);

            if (team == null)
                return null;
            else
            {
                //add the new coach 
                var newCoach = ds.Coaches.Add(Mapper.Map<Coach>(coach));
                team.Coaches.Add(newCoach); //add the coach 
                ds.SaveChanges();

                return (newCoach == null) ? null : Mapper.Map<CoachBase>(newCoach);
            }
        }

        public IEnumerable<CoachBase> CoachGetByName(string name)
        {
            var coaches = ds.Coaches.Where(p => p.Name.ToLower().Contains(name)).OrderBy(n => n.Name);

            return (coaches == null) ? null : Mapper.Map<IEnumerable<CoachBase>>(coaches);
        }

        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;


            // Monitor the progress
            bool done = false;

            // ############################################################
            // Genre

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims
                ds.RoleClaims.Add(new RoleClaim() { Id = 0, Name = "Team Captain" });
                ds.RoleClaims.Add(new RoleClaim() { Id = 1, Name = "Umpire" });
                ds.RoleClaims.Add(new RoleClaim() { Id = 2, Name = "Coach" });
                ds.RoleClaims.Add(new RoleClaim() { Id = 3, Name = "Manager" });

                ds.SaveChanges();
                done = true;
            }

            if(ds.Teams.Count() == 0)
            {
                //the path of the bulk data file 
                var path = HttpContext.Current.Server.MapPath("~/App_Data/A9Bulk.xlsx");

                //open the stream for the bulk data 
                var stream = File.Open(path, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                //IExcelDataReader reader = ExcelReaderFactory.CreateBinaryReader(stream);
                reader.IsFirstRowAsColumnNames = true;
                DataSet sourceData = reader.AsDataSet(true);
                reader.Close();

                //Get a list of each page in the sheet  
                List<TeamAdd> teams = sourceData.Tables["Teams"].DataTableToList<TeamAdd>();
                List<PlayerAdd> players = sourceData.Tables["Players"].DataTableToList<PlayerAdd>();
                List<CoachAdd> coaches = sourceData.Tables["Coaches"].DataTableToList<CoachAdd>();

                foreach (var team in teams)
                {
                    //team.Players = PlayersGetByTeam(team.Name).ToList();
                    team.Players = new List<PlayerBase>();
                    team.Coaches = new List<CoachBase>();

                    //based on the players from the table... 
                    foreach(PlayerAdd p in players.Where(pp => pp.TeamName == team.Name))
                    {
                        //add each player to the team 
                        team.Players.Add(new PlayerBase
                        {
                            Name = p.Name,
                            TeamName = p.TeamName,
                            BT = p.BT,
                            Height = p.Height,
                            Weight = p.Weight,
                            Number = p.Number,
                            Position = p.Position
                        });
                    }

                    //Based on the coaches table... 
                    foreach (CoachAdd coach in coaches.Where(cc => cc.TeamName == team.Name))
                    {
                        //add each coach... 
                        team.Coaches.Add(new CoachBase
                        {
                            Name = coach.Name,
                            Number = coach.Number, 
                            Position = coach.Position,
                            TeamName = coach.TeamName
                        });
                    }

                    //finally... map the team with it's coaches and players 
                    ds.Teams.Add(Mapper.Map<Team>(team));
                }
                ds.SaveChanges();
                stream.Dispose();
                //save changes 
                done = true;
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach(var p in ds.Players)
                {
                    ds.Entry(p).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach(var t in ds.Teams)
                {
                    ds.Entry(t).State = System.Data.Entity.EntityState.Deleted;
                }

                ds.SaveChanges();

                foreach(var c in ds.Coaches)
                {
                    ds.Entry(c).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "UserAccount" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it
    public class UserAccount
    {
        // Constructor, pass in the security principal
        public UserAccount(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        // Add other role-checking properties here as needed
        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

    public static class Helper
    {
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

    } // public static class Helper

}