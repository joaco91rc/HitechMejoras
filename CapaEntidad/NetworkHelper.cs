using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public static class NetworkHelper
    {
        public static string GetMacAddress()
        {
            var nics = NetworkInterface.GetAllNetworkInterfaces();
            var mac = nics
                .FirstOrDefault(nic => nic.NetworkInterfaceType != NetworkInterfaceType.Loopback && nic.OperationalStatus == OperationalStatus.Up)?
                .GetPhysicalAddress().ToString();
            return mac;
        }
    }
}
