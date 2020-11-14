using Xamarin.Essentials;
using System.Threading.Tasks;

namespace MapNotepad.Services
{
    class PermissionService : IPermissionService
    {
        public PermissionService()
        {
        }

        public Task<PermissionStatus> CheckPermissionAsync<T>() where T : Permissions.BasePermission, new()
        {
            return Permissions.CheckStatusAsync<T>();
        }
        public async Task<PermissionStatus> RequestPermissionAsync<T>() where T : Permissions.BasePermission, new()
        {
            var permissionStatus = await CheckPermissionAsync<T>();
            if (permissionStatus != PermissionStatus.Granted)
            {
                permissionStatus = await Permissions.RequestAsync<T>();
            }

            return permissionStatus;
        }
    }
}
