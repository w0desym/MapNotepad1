using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad
{
    class PermissionService : IPermissionService
    {
        private readonly IPermissions _permissions;
        public PermissionService(IPermissions permissions)
        {
            _permissions = permissions;
        }
        public Task<PermissionStatus> RequestLocationPermissionAsync()
        {
            return _permissions.RequestPermissionAsync<LocationPermission>();
        }
    }
}
