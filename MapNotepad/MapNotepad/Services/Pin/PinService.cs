using MapNotepad.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

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

        public List<PinInfo> GetPins()
        {
            return _repositoryService.GetItems<PinInfo>();
        }
        public IEnumerable<PinInfo> GetPins(string searchQuery)
        {
            return _repositoryService.GetItems<PinInfo>().Where(x => x.UserId == _settingsManager.CurrentUser).Where(x => 
                x.Label.ToUpper().Contains(searchQuery.ToUpper()) || 
                x.Description.ToUpper().Contains(searchQuery.ToUpper()) ||
                x.Latitude.ToString().ToUpper().Contains(searchQuery.ToUpper()) ||
                x.Longitude.ToString().ToUpper().Contains(searchQuery.ToUpper()));
        }
        public int SavePinInfo(PinInfo item)
        {
            if (item.Id != 0)
            {
                _repositoryService.UpdateItem(item);
                return item.Id;
            }
            else
            {
                return _repositoryService.InsertItem(item);
            }
        }
        public int DeletePinInfo(PinInfo item)
        {
            return _repositoryService.DeleteItem(item);
        }
    }
}
