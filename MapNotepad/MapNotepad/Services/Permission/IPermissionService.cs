using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad
{
    public interface IPermissionService
    {
        Task<PermissionStatus> RequestLocationPermissionAsync();
    }
}
