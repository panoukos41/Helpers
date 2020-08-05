// The MIT License(MIT)
// 
// Copyright(c) 2016 James Montemagno
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// https://github.com/jamesmontemagno/PermissionsPlugin/blob/master/src/Plugin.Permissions/Permissions.uwp.cs
//
// Modified by Panagiotis Athanasiou

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.Devices.Enumeration;
using Windows.Devices.Geolocation;

namespace Panoukos41.Helpers.Services
{
    /// <summary>
    /// A service to request permissions and determine if you already have them. <br/>
    /// If your permission doesn't exist in the <see cref="Permissions"/> provide your own method to handle them.
    /// </summary>
    public partial class PermissionsService : IPermissionsService
    {
        private Func<string, Task<bool>> userHandler;

        /// <summary>
        /// Set your own method to handle custom permissions not included in the <see cref="Permissions"/> values.
        /// </summary>
        public PermissionsService SetCustomHandler(Func<string, Task<bool>> handler)
        {
            userHandler = handler;
            return this;
        }

        /// <summary>
        /// For uwp this is the same as <see cref="RequestPermission(string)"/>
        /// Will return true if you were granted the permission otherwise it will return false.
        /// </summary>
        private Task<bool> PlatformHasPermission(string permission)
            => CheckPermission(permission);

        /// <summary>
        /// For uwp this is the same as <see cref="RequestPermission(string[])"/>
        /// Will return a dictionary in which the permission will be key and the value will
        /// be true if you were granted the permission otherwise it will be false.
        /// </summary>
        private async Task<IDictionary<string, bool>> PlatformHasPermission(params string[] permissions)
        {
            IDictionary<string, bool> dict = new Dictionary<string, bool>();
            foreach (var permission in permissions)
            {
                dict.Add(permission, await CheckPermission(permission));
            }
            return dict;
        }

        /// <summary>
        /// Request a permission.
        /// Will return true if you were granted the permission otherwise it will return false.
        /// </summary>
        private Task<bool> PlatformRequestPermission(string permission)
            => HasPermission(permission);

        /// <summary>
        /// Request a list of permissions to.
        /// Will return a dictionary in which the permission will be key and the value will
        /// be true if you were granted the permission otherwise it will be false.
        /// </summary>
        private Task<IDictionary<string, bool>> PlatformRequestPermission(params string[] permissions)
            => HasPermission(permissions);

        private Task<bool> CheckPermission(string permission)
        {
            switch (permission)
            {
                case Permissions.ReadContacts:
                case Permissions.WriteContacts:
                    return CheckContactsAsync();

                case Permissions.AccessFineLocation:
                    return CheckLocationAsync();

                case Permissions.BodySensors:
                    return CheckSensors();

                case Permissions.RecordAudio:
                case Permissions.CallPhone:
                case Permissions.Camera:
                case Permissions.ReadSms:
                case Permissions.WriteSms:
                case Permissions.ReadStorage:
                case Permissions.WriteStorage:
                    return Task.FromResult(true);
            }

            return userHandler != null
                ? userHandler.Invoke(permission)
                : Task.FromResult(false);
        }

        private async Task<bool> CheckContactsAsync()
        {
            var accessStatus = await ContactManager.RequestStoreAsync(ContactStoreAccessType.AppContactsReadWrite);

            return accessStatus != null;
        }

        private async Task<bool> CheckLocationAsync()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();

            return accessStatus == GeolocationAccessStatus.Allowed;
        }

        private Task<bool> CheckSensors()
        {
            // Determine if the user has allowed access to activity sensors
            var deviceAccessInfo = DeviceAccessInformation.CreateFromDeviceClassId(new Guid("9D9E0118-1807-4F2E-96E4-2CE57142E196"));
            return Task.FromResult(deviceAccessInfo.CurrentStatus == DeviceAccessStatus.Allowed);
        }
    }
}