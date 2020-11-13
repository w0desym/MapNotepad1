﻿using MapNotepad.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

        public async Task<int> TrySavePinInfoAsync(PinInfo pinInfo)
        {
            int result = 0;

            if (pinInfo != null)
            {
                if (pinInfo.Id != 0)
                {
                    await _repositoryService.UpdateItemAsync(pinInfo);
                    result = pinInfo.Id;
                }
                else
                {
                    result = await _repositoryService.TryInsertItemAsync(pinInfo);
                }
            }

            return result;
        }

        public async Task SavePinInfoAsync(PinInfo pinInfo)
        {
            var pinInfos = await GetPinsAsync();
            var regex = new Regex($@"^{pinInfo.Label}\d?$");
            var sameLabelPinInfos = pinInfos.Where(x => regex.IsMatch(x.Label));

            int counter;

            if (sameLabelPinInfos.Count() == 0)
            {
                await TrySavePinInfoAsync(pinInfo);
            }
            else
            {
                counter = pinInfos.Last().Id + 1;
                pinInfo.Label = string.Format("{0} ({1})", pinInfo.Label, counter);
                await TrySavePinInfoAsync(pinInfo);
            }
        }

        public Task<int> DeletePinInfoAsync(PinInfo pinInfo)
        {
            return _repositoryService.DeleteItemAsync(pinInfo);
        }

    }
}
