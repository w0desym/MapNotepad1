using MapNotepad.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad
{
    class AuthorizationService : IAuthorizationService
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IRepositoryService _repositoryService;
        public AuthorizationService(ISettingsManager settingsManager,
            IRepositoryService repositoryService)
        {
            _settingsManager = settingsManager;
            _repositoryService = repositoryService;
        }
        public int Authorize(int id)
        {
            return _settingsManager.CurrentUser = id;
        }
        public async Task<int> RegisterAsync(User user)
        {
            if (user.Id != 0)
            {
                await _repositoryService.UpdateItemAsync(user);
                return user.Id;
            }
            else
            {
                return await _repositoryService.InsertItemAsync(user);
            }
        }
    }
}
