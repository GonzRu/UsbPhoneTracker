using System;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Net.Sockets;

namespace UsbPhoneTracker.Mac
{
	public class MacAddressHelper
	{
		static public string GetMacAddress ()
		{
			foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (!String.IsNullOrEmpty (nic.GetPhysicalAddress ().ToString ())) {

					foreach (UnicastIPAddressInformation ip in nic.GetIPProperties().UnicastAddresses)
					{
						if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
						{
							if (!String.IsNullOrEmpty (ip.Address.ToString ())) {
								Console.WriteLine (ip.Address.ToString ());
								return nic.GetPhysicalAddress ().ToString ();
							}
						}
					}
				}
			}

			return String.Empty;
		}
	}
}

