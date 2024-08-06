using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Linq;

namespace CapaNegocio.Services
{
    public class SucursalService
    {
        private readonly Dictionary<string, int> _sucursales;

        public SucursalService()
        {
            _sucursales = new Dictionary<string, int>();
            LoadSucursalSettings();
        }

        private void LoadSucursalSettings()
        {
            foreach (string key in ConfigurationManager.AppSettings.AllKeys)
            {
                if (int.TryParse(ConfigurationManager.AppSettings[key], out int sucursalId))
                {
                    _sucursales[key] = sucursalId;
                }
            }
        }

        public int ObtenerSucursalId()
        {
            var macAddress = GetMacAddress();
            return _sucursales.TryGetValue(macAddress, out var sucursalId) ? sucursalId : -1;
        }

        private string GetMacAddress()
        {
            var nics = NetworkInterface.GetAllNetworkInterfaces();
            var mac = nics
                .FirstOrDefault(nic => nic.NetworkInterfaceType != NetworkInterfaceType.Loopback && nic.OperationalStatus == OperationalStatus.Up)?
                .GetPhysicalAddress().ToString();
            return mac;
        }
    }
}
