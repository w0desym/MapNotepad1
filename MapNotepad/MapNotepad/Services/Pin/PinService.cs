using MapNotepad.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad
{
    class PinService : IPinService
    {
        private readonly IRepositoryService _repositoryService;
        private readonly ISettingsManager _settingsManager;

        public PinService(IRepositoryService repositoryService,
            ISettingsManager settingsManager)
        {
            _repositoryService = repositoryService;
            _settingsManager = settingsManager;
        }

        public async Task<IEnumerable<PinInfo>> GetPinsAsync()
        {
            var pinInfos = await _repositoryService.GetItemsAsync<PinInfo>();
            var curUserPinInfos = pinInfos.Where(x => x.UserId == _settingsManager.CurrentUser);

            return curUserPinInfos;
        }
        public async Task<IEnumerable<PinInfo>> GetPinsAsync(string searchQuery)
        {
            var curUserPinInfos = await GetPinsAsync();

            return curUserPinInfos.Where(x => 
                x.Label.ToUpper().Contains(searchQuery.ToUpper()) || 
                x.Description.ToUpper().Contains(searchQuery.ToUpper()) ||
                x.Latitude.ToString().ToUpper().Contains(searchQuery.ToUpper()) ||
                x.Longitude.ToString().ToUpper().Contains(searchQuery.ToUpper()));
        }
        public async Task<int> SavePinInfoAsync(PinInfo pinInfo)
        {
            int result;

            if (pinInfo.Id != 0)
            {
                await _repositoryService.UpdateItemAsync(pinInfo);
                result =  pinInfo.Id;
            }
            else
            {
                result = await _repositoryService.InsertItemAsync(pinInfo);
            }

            return result;
        }
        public async Task<int> DeletePinInfoAsync(PinInfo pinInfo)
        {
            return await _repositoryService.DeleteItemAsync(pinInfo);
        }
    }
}
