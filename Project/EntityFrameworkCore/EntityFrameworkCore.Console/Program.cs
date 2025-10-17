using EntityFrameworkCore.Data;
using EntityFrameworkCore.Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Transactions;

// first we need an instance of DbContext
var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var dbPath = Path.Combine(path, "FootballLeague_EfCore.db");
var connectionString = $"Data Source ={dbPath}";

var optionsBuilder = new DbContextOptionsBuilder<FootballLeagueDbContext>();
optionsBuilder.UseSqlite(connectionString);

using var context = new FootballLeagueDbContext(optionsBuilder.Options);


await context.Database.MigrateAsync(); // -- applies the pending migrations to database


// "using" -- used to make the context to be used only for current execution scope and kill when program finished

// Select all teams
// GetAllTeams();

// void GetAllTeams()
{
    // // SELECT * FROM Teams
    // var teams = context.Team.ToList();

    // foreach (var t in teams)
    // {
    //     Console.WriteLine(t.Name);
    // }
}


// Select one Team
// GetOneTeam();

// async void GetOneTeam()
{
    // Selecting a single record - First one in the list
    // var teamOne = await context.Team.FirstAsync();

    // Selecting a single record - First one in the list that meets a condition
    // var teamTwo = await context.Team.FirstAsync(team => team.Id == 1 );

    // Selecting single record from empty Table
    // var coach = await context.Coach.FirstOrDefaultAsync();

    // Select single record -- only one record must be present in Team, else exception returned 
    // var teamThree = await context.Team.SingleAsync();

    // Select single record -- only one record with Id=2 must be present in Team, else exception returned 
    // var teamFour = await context.Team.SingleAsync(team => team.Id == 2);

    // Find based on Id
    // var teamBasedOnId = await context.Team.FindAsync(3);
    // if (teamBasedOnId != null)
    // {
    //     Console.WriteLine(teamBasedOnId.Name);
    // }
}


// Filters

// Select all record that meet a condition
// await GetFilteredTeams();

// async Task GetFilteredTeams()
{
    // Console.WriteLine("Enter Desired Team:");
    // var filter = Console.ReadLine();

    // var teamsFiltered = await context.Team.Where(team => team.Name == filter).ToListAsync();

    // foreach (var team in teamsFiltered)
    // {
    //     Console.WriteLine(team.Name);
    // }


    //Partial Matching

    // Console.WriteLine("Enter Search Term:");
    // var searchTerm = Console.ReadLine();

    // var partialMatches = await context.Team.Where(team => team.Name.Contains(searchTerm)).ToListAsync();

    //SELECT * FROM Team WHERE Name LIKE '%serachTerm%'
    // var partialMatches = await context.Team.Where(team => EF.Functions.Like(team.Name, $"%{searchTerm}%")).ToListAsync();

    // foreach (var team in partialMatches)
    // {
    //     Console.WriteLine(team.Name);
    // }
}



/// Get all rows

// await GetAllTeams();

// async Task GetAllTeams()
{
    //select all
    // var teams = await (from team in context.Team select team).ToListAsync();
    //OR
    // var teams = await context.Team.ToListAsync();

    // foreach (var team in teams)
    // {
    //     Console.WriteLine(team.Name);
    // }
}




/// Aggregate Methods

// await AggregateMethods();

// async Task AggregateMethods()
{
    // var numberOfTeams = await context.Team.CountAsync();
    // Console.WriteLine($"No. of teams: {numberOfTeams}");

    // var numberOfTeamsWithCondition = await context.Team.CountAsync(team => team.Id == 1);
    // Console.WriteLine($"No. of teams with Condition: {numberOfTeamsWithCondition}");

    // // Max
    // var maxTeams = await context.Team.MaxAsync(team => team.Id);
    // //Min
    // var minTeam = await context.Team.MinAsync(team => team.Id);
    // //Avg
    // var avgTeams = await context.Team.AverageAsync(team => team.Id);
    // //Sum
    // var sumTeam = await context.Team.SumAsync(team => team.Id);

    // Console.WriteLine($"MaxTeams:{maxTeams}, MinTeams:{minTeam}, AvgTeam:{avgTeams}, SumTeam:{sumTeam}");
}





/// Grouping and Aggregating 

// -- always do ToList() when grouping so that EF Core wont load data on demand and do it only once -- which prevents performance issues


// GroupTeams();

// void GroupTeams()
{
    // // var groupedTeams = context.Team.GroupBy(team => team.CreatedDate.Date).ToList();
    // //OR
    // // var groupedTeams = context.Team.GroupBy(team => new { team.CreatedDate.Date }).ToList(); // creates a new object 


    // var groupedTeams = context.Team
    //                                 // .Where(team => team.Name =='') // translates to WHERE clause in SQL
    //                                 .GroupBy(team => new { team.CreatedDate.Date }).ToList();
    // // .Where() // translates to HAVING clause in SQL

    // foreach (var group in groupedTeams)
    // {
    //     Console.WriteLine($"Group  {group.Key}:");
    //     foreach (var team in group)
    //     {
    //         Console.WriteLine(team.Name);
    //     }
    // }
}




/// Order By

// await OrderTeams();

// async Task OrderTeams()
{
    // var orderedTeams = await context.Team.OrderBy(team => team.Name).ToListAsync();

    // foreach (var team in orderedTeams)
    // {
    //     Console.WriteLine(team.Name);
    // }

    // var descOrderedTeams = await context.Team.OrderByDescending(team => team.Name).ToListAsync();

    // foreach (var team in descOrderedTeams)
    // {
    //     Console.WriteLine(team.Name);
    // }

    // var maxBy = context.Team.MaxBy(team => team.Id);
    // Console.WriteLine($"MaxBy: {maxBy}");

    // var minBy = context.Team.MinBy(team => team.Id);
    // Console.WriteLine($"MaxBy: {maxBy}");
}




// Current Database Path 
// Console.WriteLine(context.DbPath);
// C:\Users\E272830\AppData\Local\FootballLeague_EfCore.db



/// Skip and Take

// await SkipAndTake();

// async Task SkipAndTake()
{
    // var recordCount = 3; // -- retrieve 3 items at each query
    // var page = 0;
    // bool next = true;

    // while (next)
    // {
    //     var teams = await context.Team.Skip(page * recordCount).Take(recordCount).ToListAsync();
    //     foreach (var team in teams)
    //     {
    //         Console.WriteLine(team.Name);
    //     }
    //     Console.WriteLine("Enter 'true' for next set of records, or 'false' for exit:");
    //     next = Convert.ToBoolean(Console.ReadLine());

    //     if (!next) break;
    //     page += 1;
    // }
}



/// Select and Projections -- more precise queries

// await SelectAndProject();

// async Task SelectAndProject()
{
    // var teamNames = await context.Team.Select(team => team.Name).ToListAsync();

    // var teamData = await context.Team.Select(team => new { team.Name, team.Id }).ToListAsync();

    // foreach (var team in teamData)
    // {
    //     Console.WriteLine($"Name: {team.Name}, Id: {team.Id}");
    // }


    // // Using a new Custom DataType -- TeamInfo
    // var teamInfo = await context.Team
    //                         .Select(team => new TeamInfo { Id = team.Id, Name = team.Name }).ToListAsync();

}

// class TeamInfo
{
    // public int Id { get; set; }
    // public string? Name { get; set; }
}


/// No Tracking

// var teams = await context.Team
//             .AsNoTracking()
//             .ToListAsync();

// foreach (var team in teams)
// {
//     Console.WriteLine(team.Name);
// }


/// IQueryables and Lists

// IQueryablesAndLists();

// async void IQueryablesAndLists()
{
    // Console.WriteLine("Enter 1 for Team with Id 1 and 2 for Teams that contain FC:");
    // var option = Convert.ToInt32(Console.ReadLine());


    // // Lists: All Records are loaded into memory, then any operation is done in memory
    // // var teamsList = await context.Team.ToListAsync();
    // // if (option == 1)
    // // {
    // //     teamsList = teamsList.Where(team => team.Id == 1).ToList();
    // // }
    // // else if (option == 2)
    // // {
    // //     teamsList = teamsList.Where(team => team.Name.Contains("FC")).ToList();
    // // }


    // // IQueryable: Filtered records are loaded directly
    // var teamsAsQueryable = context.Team.AsQueryable();
    // if (option == 1)
    // {
    //     teamsAsQueryable = teamsAsQueryable.Where(team => team.Id == 1);
    // }
    // else if (option == 2)
    // {
    //     teamsAsQueryable = teamsAsQueryable.Where(team => team.Name.Contains("FC"));
    // }

    // foreach (var team in teamsAsQueryable)
    // {
    //     Console.WriteLine(team.Name);
    // }
}



/// Inserting Data

// INSERT INTO Coach(cols) VALUES (values)
{
    // var newCoach = new Coach
    // {
    //     Name = "Jose Mourinho",
    //     CreatedDate = DateTime.Now
    // };


    // await context.Coach.AddAsync(newCoach);
    // await context.SaveChangesAsync();

    // Loop Insert 
    // var newCoach2 = new Coach
    // {
    //     Name = "Theodore Whitmore",
    //     CreatedDate = DateTime.Now
    // };

    // var newCoach3 = new Coach
    // {
    //     Name = "Pele",
    //     CreatedDate = DateTime.Now
    // };

    // List<Coach> coachList = new List<Coach>
    // {
    //     newCoach2,
    //     newCoach3
    // };

    // // foreach (var coach in coachList)
    // // {
    // //     await context.Coach.AddAsync(coach);
    // // }

    // // await context.SaveChangesAsync();
    // // Console.WriteLine(context.ChangeTracker.DebugView.LongView);


    // // Batch Insert

    // await context.Coach.AddRangeAsync(coachList);
    // await context.SaveChangesAsync();
}

/// Update Record
{
    // With Tracking
    // var coachPele = await context.Coach.FindAsync(7);
    // coachPele.Name = "Pele Legend"; // this will work as there's tracking enabled by default in EF 
    // await context.SaveChangesAsync();


    // Without Tracking
    // var coachTest = await context.Coach.AsNoTracking().FirstOrDefaultAsync(coach => coach.Id == 7);
    // coachTest.Name = "Test No Tracking";
    // context.Update(coachTest);
    // await context.SaveChangesAsync();
}


/// Delete Record

// var coach = context.Coach.FindAsync(7);
// context.Remove(coach);
// await context.SaveChangesAsync();


/// ExecuteDelete & ExecuteUpdate
{
    // await context.Coach.Where(coach => coach.Name == "Theodore Whitmore").ExecuteDeleteAsync();

    // await context.Coach.Where(coach => coach.Name == "Jose Mourinho")
    //                     .ExecuteUpdateAsync(set => set
    //                                                 .SetProperty(prop => prop.Name, "Jose")
    //                                                 .SetProperty(prop => prop.CreatedDate, DateTime.Now)
    //                                         );  
}



//////// Section 7 /////////

//// Inserting Related Data

/// Insert record with FK
{
    // var match = new Match
    // {
    //     AwayTeamId = 1,
    //     HomeTeamId = 2,
    //     HomeTeamScore = 0,
    //     AwayTeamScore = 0,
    //     CreatedDate = new DateTime(2023, 10, 1),
    //     TicketPrice = 20
    // };

    // await context.AddAsync(match);
    // await context.SaveChangesAsync();
}


/// Insert Parent/Child
{
    // var coach = new Coach
    // {
    //     Name = "New Coach"
    // };

    // var team = new Team
    // {
    //     Name = "New Team",
    //     Coach = coach
    // };

    //OR

    // var team = new Team
    // {
    //     Name = "New Team",
    //     Coach = new Coach
    //     {
    //         Name = "New Coach"
    //     }
    // };

    // await context.AddAsync(team);
    // await context.SaveChangesAsync();

}


/// Insert Parent with all children list
{
    // var league = new League
    // {
    //     Name = "Series A",
    //     Teams = new List<Team>
    //     {
    //         new Team
    //         {
    //             Name = "Juventus",
    //             Coach = new Coach
    //             {
    //                 Name = "Juve Coach"
    //             }
    //         },
    //         new Team
    //         {
    //             Name = "AC Milan",
    //             Coach = new Coach
    //             {
    //                 Name = "Milan Coach"
    //             }
    //         },
    //         new Team
    //         {
    //             Name = "AS Roma",
    //             Coach = new Coach
    //             {
    //                 Name = "Roma Coach"
    //             }
    //         }
    //     }
    // };

    // await context.AddAsync(league);
    // await context.SaveChangesAsync();
}




//// Loading Related Data

/// Eager Loading
{
    // Using Include() to eager load 
    // var leagues = await context.League
    //             // .Include("Team") 
    //             .Include(league => league.Teams) // Include Teams linked to Leagues listed
    //             .ThenInclude(team => team.Coach) // Include Coaches linked to listed team
    //             .ToListAsync();

    // foreach (var league in leagues)
    // {
    //     Console.WriteLine(league.Name);

    //     foreach (var team in league.Teams)
    //     {
    //         Console.WriteLine($"{team.Name} -- {team.Coach.Name}");
    //     }
    // }
}

/// Explicit Loading
{
    // var league = await context.FindAsync<League>(4);

    // if (league == null)
    // {
    //     Console.WriteLine("League not found");
    //     return; // or handle this case appropriately
    // }

    // if (league.Teams == null || !league.Teams.Any())
    // {
    //     Console.WriteLine("Teams have not been loaded");
    // }


    // await context.Entry(league)
    //     .Collection(l => l.Teams)
    //     .LoadAsync(); // code to load -- stated explicitly 

    // if (league.Teams != null && league.Teams.Any())
    // {
    //     foreach (var team in league.Teams)
    //     {
    //         Console.WriteLine($"{team.Name}");
    //     }
    // }
}


/// Lazy Loading
{
    // var league = await context.FindAsync<League>(4);
    // foreach (var team in league.Teams)
    // {
    //     Console.WriteLine($"{team.Name}");
    // }


    // foreach (var league in context.League)
    // {
    //     foreach (var team in league.Teams)
    //     {
    //         Console.WriteLine($"{team.Name} -- {team.Coach.Name}");
    //     }
    // }
}


//// Filtering on Related Records
{
    // Adding some more entries for filter operation
    {
        // var match1 = new Match
        // {
        //     AwayTeamId = 2,
        //     HomeTeamId = 3,
        //     HomeTeamScore = 1,
        //     AwayTeamScore = 0,
        //     Date = new DateTime(2023, 01, 1),
        //     TicketPrice = 20,
        // };
        // var match2 = new Match
        // {
        //     AwayTeamId = 2,
        //     HomeTeamId = 1,
        //     HomeTeamScore = 1,
        //     AwayTeamScore = 0,
        //     Date = new DateTime(2023, 01, 1),
        //     TicketPrice = 20,
        // };
        // var match3 = new Match
        // {
        //     AwayTeamId = 1,
        //     HomeTeamId = 3,
        //     HomeTeamScore = 1,
        //     AwayTeamScore = 0,
        //     Date = new DateTime(2023, 01, 1),
        //     TicketPrice = 20,
        // };
        // var match4 = new Match
        // {
        //     AwayTeamId = 4,
        //     HomeTeamId = 3,
        //     HomeTeamScore = 0,
        //     AwayTeamScore = 1,
        //     Date = new DateTime(2023, 01, 1),
        //     TicketPrice = 20,
        // };
        // await context.AddRangeAsync(match1,match2,match3,match4);
        // await context.SaveChangesAsync();
    }

    // Qureying filtered records to get only those team's score records who had scored > 0 in one or more matches in which they were HomeTeam
    {
        // var teams = await context.Team
        //     .Include("Coach")
        //     .Include(t => t.HomeMatches.Where(HomeMatch => HomeMatch.HomeTeamScore > 0))
        //     .ToListAsync();

        // foreach (var team in teams)
        // {
        //     Console.WriteLine($"{team.Name} -- {team.Coach.Name}");
        //     foreach (var match in team.HomeMatches)
        //     {
        //         Console.WriteLine($"Score {match.HomeTeamScore}");
        //     }
        // }
    }
}


/// Projects and Anonymous Types
{
    // var teams = await context.Team
    // .Select(team => new TeamDetails
    // {
    //     TeamId = team.Id,
    //     TeamName = team.Name,
    //     CoachName = team.Coach.Name,
    //     TotalHomeGoals = team.HomeMatches.Sum(m => m.HomeTeamScore),
    //     TotalAwayGoals = team.AwayMatches.Sum(m => m.AwayTeamScore)
    // })
    // .ToListAsync();

    // foreach (var team in teams)
    // {
    //     Console.WriteLine($"{team.TeamName} -- {team.CoachName} | HomeGoals: {team.TotalHomeGoals} | AwayGoals: {team.TotalAwayGoals}");
    // }
}

// class TeamDetails
// {
//     public int TeamId { get; set; }
//     public string TeamName { get; set; }
//     public string CoachName { get; set; }

//     public int TotalHomeGoals { get; set; }
//     public int TotalAwayGoals { get; set; }
// }


/// Querying Keyless Entities (Like Views)
{
    // var details = await context.TeamsAndLeaguesView.ToListAsync();
    // foreach (var data in details)
    // {
    //     Console.WriteLine($"Team Name: {data.Name}");
    //     Console.WriteLine($"League Name: {data.LeagueName}");
    // }
}


//// Querying Raw SQL


/// FromSqlRaw() -- requires user input parameterisation
{
    // Console.WriteLine("Enter Team Name:");
    // var teamName = Console.ReadLine();
    // var teamNameParam = new SqliteParameter("teamName", teamName); //parameterising user input to avoid SQL Injection
    // var teams = context.Team.FromSqlRaw("SELECT * FROM Team WHERE Name = @teamName", teamNameParam);
    // foreach (var team in teams)
    // {
    //     Console.WriteLine($"Team Name: {team.Name}");
    // }
}

// OR

/// FromSql()
{
    // Console.WriteLine("Enter Team Name:");
    // var teamName2 = Console.ReadLine();
    // var teams2 = context.Team.FromSql($"SELECT * FROM Team WHERE Name = {teamName2}");
    // foreach (var team in teams2)
    // {
    //     Console.WriteLine($"Team Name: {team.Name}");
    // }
}

// OR

/// FromSqlInterpolated()
{
    // Console.WriteLine("Enter Team Name:");
    // var teamName3 = Console.ReadLine();
    // var teams3 = context.Team.FromSqlInterpolated($"SELECT * FROM Team WHERE Name = {teamName3}");
    // foreach (var team in teams3)
    // {
    //     Console.WriteLine($"Team Name: {team.Name}");
    // }
}


/// Mixing with SQL
{
    // var teamsList = await context.Team.FromSql($"SELECT * FROM Team")
    //     .Where(team => team.Id == 1)
    //     .OrderBy(t => t.Id)
    //     .Include("League")
    //     .ToListAsync();

    // foreach (var team in teamsList)
    // {
    //     Console.WriteLine(team.Name);
    // }
}

/// Executing Stored Procedures -- Doesnt work for Sqlite
{
    // var leagueId = 1;
    // var league = context.League.FromSqlInterpolated($"EXEC dbo.StoredProcedureToGetLeaugeNameHere {leagueId}");
}

/// Non-querying statements -- dont execute, just given for example
{
    // var someNewTeamName = "New Team Name Here";
    // var success = context.Database.ExecuteSqlInterpolated($"UPDATE Team SET Name = {someNewTeamName}");

    // var teamToDeleteId = 1;
    // var teamDeleteSuccess = context.Database.ExecuteSqlInterpolated($"EXEC dbo.DeleteTeam {teamToDeleteId} ");
}


/// Query Scalar or Non-Entity Types
{
    // var leagueIds = context.Database.SqlQuery<int>($"SELECT Id from League").ToList();
    // foreach (var id in leagueIds)
    // {
    //     Console.WriteLine(id);
    // }
}


/// Executing User Defined Functions -- Sqlite doesnt support function so dont execute below
{
    // Database will contain following function

    /*  
    CREATE FUNCTION [dbo].[GetEarliestMatch] (@teamId int)
        RETURNS datetime
        BEGIN 
            DECLARE @result datetime
            SELECT TOP 1 @result = date
            FROM Match [dbo].[Match]
            ORDER BY Date
            return @result
        END
    */

    // Executing the function   
    // var earliestMatch = context.GetEarliestTeamMatch(1);
}



/// Manipulating Entries Before Save Changes
{
    // await InsertRecordWithAudit();

    // async Task InsertRecordWithAudit()
    // {
    //     var newLeague = new League
    //     {
    //         Name = "New League With Audit"
    //     };

    //     await context.AddAsync(newLeague);

    //     await context.SaveChangesAsync();

    // }
}


/// Combining Save Changes into a transaction -- helpful to rollback
{
    // // begin transaction
    // var transaction = context.Database.BeginTransaction();

    // var league = new League
    // {
    //     Name = "Testing Transaction"
    // };

    // await context.AddAsync(league);
    // await context.SaveChangesAsync();

    // // Add Checkpoint
    // transaction.CreateSavepoint("CreatedLeague");

    // var coach = new Coach
    // {
    //     Name = "Transaction Coach"
    // };

    // await context.AddAsync(coach);
    // await context.SaveChangesAsync();

    // var team =
    //     new Team
    //     {
    //         Name = "Transaction Team 1",
    //         CoachId = coach.Id,
    //         LeagueId = league.Id
    //     };

    // await context.AddAsync(team);
    // await context.SaveChangesAsync();

    // try
    // {
    //     transaction.Commit();
    // }
    // catch (Exception)
    // {
    //     // transaction.Rollback(); //rollback complete transaction
    //     transaction.RollbackToSavepoint("CreatedLeague");
    //     throw;
    // }
}


/// Versioning Changes -- Resolving Concurrency Conflicts
{
    // var team = context.Team.Find(1);
    // team.Name = "New Team with Concurrency Check 1";
    // try
    // {
    //     await context.SaveChangesAsync();
    // }
    // catch (Exception ex)
    // {
    //     Console.WriteLine(ex);
    //     //throw;
    // }
}


/// Soft Delete 
{
    // // var leagues = context.League.ToList();
    // // Console.WriteLine("Before delete:");
    // // foreach (var l in leagues)
    // // {
    // //     Console.WriteLine(l.Name);
    // // }

    // // var leagueToDelete = context.League.Find(1);
    // // leagueToDelete.IsDeleted = true;

    // // context.SaveChanges();


    // // leagues = context.League.ToList();
    // // Console.WriteLine("After delete:");
    // // foreach (var l in leagues)
    // // {
    // //     Console.WriteLine(l.Name);
    // // }

    // var leagues = context.League
    //     .IgnoreQueryFilters()
    //     .ToList();
    // foreach (var l in leagues)
    // {
    //     Console.WriteLine(l.Name);
    // }
}


