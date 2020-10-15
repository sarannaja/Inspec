// Copyright © 2020 Ken Haggerty (https://kenhaggerty.com)
// Licensed under the MIT License.

using System.Net;
using System.Net.Sockets;

public static class AnonymizeIpAddressExtention
{
    //const string IPV4_NETMASK = "255.255.255.0";
    //const string IPV6_NETMASK = "ffff:ffff:ffff:0000:0000:0000:0000:0000";

    /// <summary>
    /// Removes the unique part of an <see cref="IPAddress" />.
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <returns><see cref="string" /></returns>
    public static string AnonymizeIP(this IPAddress ipAddress)
    {
        string ipAnonymizedString;
        if (ipAddress != null)
        {
            if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
            {
                var ipString = ipAddress.ToString();
                string[] octets = ipString.Split('.');
                octets[3] = "0";
                ipAnonymizedString = string.Join(".", octets);
            }
            else if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
            {
                var ipString = ipAddress.ToString();
                string[] hextets = ipString.Split(':');
                var hl = hextets.Length;
                if (hl > 3) { for (var i = 3; i < hl; i++) { if (hextets[i].Length > 0) { hextets[i] = "0"; } } }
                ipAnonymizedString = string.Join(":", hextets);
            }
            else { ipAnonymizedString = $"Not Valid - {ipAddress.ToString()}"; }
        }
        else { ipAnonymizedString = "Is Null"; }

        return ipAnonymizedString;
    }
}