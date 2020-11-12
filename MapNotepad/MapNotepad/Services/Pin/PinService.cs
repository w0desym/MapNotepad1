using MapNotepad.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services
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

        public async Task<IEnumerable<PinInfo>> GetPinsAsync(string searchQuery = null)
        {
            var pinInfos = await _repositoryService.GetItemsAsync<PinInfo>();
            var curUserPinInfos = pinInfos.Where(x => x.UserId == _settingsManager.CurrentUser);

            if (!string.IsNullOrEmpty(searchQuery))
            {
                curUserPinInfos = curUserPinInfos.Where(x =>
                x.Label.ToUpper().Contains(searchQuery?.ToUpper()) ||
                x.Description.ToUpper().Contains(searchQuery?.ToUpper()) ||
                x.Latitude.ToString().ToUpper().Contains(searchQuery?.ToUpper()) ||
                x.Longitude.ToString().ToUpper().Contains(searchQuery?.ToUpper()));
            }

            return curUserPinInfos;
        }

        public async Task<int> TrySavePinInfoAsync(PinInfo pinInfo)
        {
            int result;

            if (pinInfo.Id != 0)
            {
                await _repositoryService.UpdateItemAsync(pinInfo);
                result = pinInfo.Id;
            }
            else
            {
                result = await _repositoryService.TryInsertItemAsync(pinInfo);
            }

            return result;
        }

        public async Task SavePinInfoAsync(PinInfo pinInfo)
        {
            var label = pinInfo.Label;
            int counter = 0;
            
            while (await TrySavePinInfoAsync(pinInfo) != 1)
            {
                counter++;
                pinInfo.Label = string.Format("{0} ({1})", label, counter);
            }
        }

        public Task<int> DeletePinInfoAsync(PinInfo pinInfo)
        {
            return _repositoryService.DeleteItemAsync(pinInfo);
        }

    }
}
