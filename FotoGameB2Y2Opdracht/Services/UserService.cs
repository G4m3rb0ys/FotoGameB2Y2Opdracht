using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FotoGameB2Y2Opdracht.MVVM.Models;

namespace FotoGameB2Y2Opdracht.Services
{
    public class UserService
    {
        private readonly LocalDbService _dbService;
        private User _currentUser;

        public UserService(LocalDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            var user = await _dbService.GetUser(username, password);
            if (user != null)
            {
                _currentUser = user;
            }
            return user;
        }

        public User GetCurrentUser()
        {
            return _currentUser;
        }

        public void Logout()
        {
            _currentUser = null;
        }

        public bool IsLoggedIn()
        {
            return _currentUser != null;
        }
    }
}
