using MapNotepad.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MapNotepad.Services
{
    class PinService : IPinService
    {
        private readonly IRepositoryService _repositoryService;
        private readonly IUserService _userService;

        public PinService(IRepositoryService repositoryService,
            IUserService userService)
        {
            _repositoryService = repositoryService;
            _userService = userService;
        }

        public async Task<IEnumerable<PinInfo>> GetPinsAsync(string searchQuery = null)
        {
            var pinInfos = await _repositoryService.GetItemsAsync<PinInfo>();
            var curUserPinInfos = pinInfos.Where(x => x.UserId == _userService.CurrentUserId);

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

        public Task<int> UpdatePinInfoAsync(PinInfo pinInfo)
        {
            return _repositoryService.UpdateItemAsync(pinInfo);
        }

        public async Task AddPinInfoAsync(PinInfo pinInfo)
        {
            var pinInfos = await GetPinsAsync();
            var regex = new Regex($@"^{pinInfo.Label}\d?$");
            var sameLabelPinInfos = pinInfos.Where(x => regex.IsMatch(x.Label));

            int counter;

            if (sameLabelPinInfos.Count() == 0)
            {
                await _repositoryService.TryInsertItemAsync(pinInfo);
            }
            else
            {
                counter = pinInfos.Last().Id + 1;
                pinInfo.Label = string.Format("{0} ({1})", pinInfo.Label, counter);
                await _repositoryService.TryInsertItemAsync(pinInfo);
            }
        }

        public Task<int> DeletePinInfoAsync(PinInfo pinInfo)
        {
            return _repositoryService.DeleteItemAsync(pinInfo);
        }

    }
}
