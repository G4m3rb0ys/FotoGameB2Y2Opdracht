using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FotoGameB2Y2Opdracht.MVVM.Models;
using SQLite;

namespace FotoGameB2Y2Opdracht.Services
{
    public class LocalDbService
    {
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(
                Constants.DBPath,
                Constants.Flags
                );
        }

        public async Task InitializeDatabase()
        {
            try
            {
                await _connection.CreateTableAsync<User>();
                await _connection.CreateTableAsync<Tasks>();
                await _connection.CreateTableAsync<WeeklyTaskGoal>();
                await _connection.CreateTableAsync<Claim>();
                await _connection.CreateTableAsync<ClaimPhoto>();
                await _connection.CreateTableAsync<ClaimWeeklyGoal>();
                Console.WriteLine("Database and User table created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database initialization failed: {ex.Message}");
            }
        }

        // CRUD-methodes
        public async Task<List<User>> GetUsers()
        {
            return await _connection.Table<User>().ToListAsync();
        }

        public async Task<User> GetUser(string username, string password)
        {
            return await _connection.Table<User>().FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<User> CreateUser(User user)
        {
            await _connection.InsertAsync(user);
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            await _connection.UpdateAsync(user);
            return user;
        }

        public async Task<User> DeleteUser(User user)
        {
            await _connection.DeleteAsync(user);
            return user;
        }

        public async Task<(bool usernameExists, bool emailExists)> UserExists(string username, string email)
        {
            var usernameExists = await _connection.Table<User>()
                .CountAsync(u => u.Username == username) > 0;

            var emailExists = await _connection.Table<User>()
                .CountAsync(u => u.Email == email) > 0;

            return (usernameExists, emailExists);
        }

        public async Task<List<Tasks>> GetTasks()
        {
            return await _connection.Table<Tasks>().ToListAsync();
        }

        public async Task CreateTask(Tasks task)
        {
            await _connection.InsertAsync(task);
        }
        public async Task AddWeeklyGoals(List<WeeklyTaskGoal> goals)
        {
            foreach (var goal in goals)
            {
                await _connection.InsertAsync(goal);
            }
        }

        public async Task<Tasks> GetTaskById(int taskId)
        {
            return await _connection.Table<Tasks>().FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task<Claim> GetClaimByUserAndTask(int userId, int taskId)
        {
            return await _connection.Table<Claim>().FirstOrDefaultAsync(c => c.UserId == userId && c.TaskId == taskId);
        }

        public async Task<List<Claim>> GetClaimsByUserId(int userId)
        {
            return await _connection.Table<Claim>().Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<List<Claim>> GetAllClaims()
        {
            return await _connection.Table<Claim>().ToListAsync();
        }


        public async Task AddClaim(Claim claim)
        {
            await _connection.InsertAsync(claim);
        }

        public async Task AddClaimPhoto(ClaimPhoto photo)
        {
            await _connection.InsertAsync(photo);
        }

        public async Task<Claim> GetClaimByIdAsync(int claimId)
        {
            return await _connection.FindAsync<Claim>(claimId);
        }

        public async Task AddPhotoToClaimAsync(int claimId, string photoUrl)
        {
            var claim = await GetClaimByIdAsync(claimId);
            if (claim != null)
            {
                claim.PhotoUrls.Add(photoUrl);
                await _connection.UpdateAsync(claim);
            }
        }

        public async Task<List<ClaimWeeklyGoal>> GetWeeklyGoalsForClaim(int claimId)
        {
            return await _connection.Table<ClaimWeeklyGoal>().Where(w => w.ClaimId == claimId).ToListAsync();
        }

        public async Task AddWeeklyGoalsToClaim(int claimId, List<ClaimWeeklyGoal> weeklyGoals)
        {
            foreach (var goal in weeklyGoals)
            {
                goal.ClaimId = claimId;
                await _connection.InsertAsync(goal);
            }
        }
        public async Task DeleteTask(Tasks task)
        {
            await _connection.DeleteAsync(task);
        }





    }
}
