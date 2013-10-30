using System;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Net.Sockets;

namespace UsbPhoneTracker.Mac
{
	public class MacAddressHelper
	{
		static private string MacAddress = null;

		static public string GetMacAddress ()
		{
			if (MacAddress != null)
				return MacAddress;

			foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (!String.IsNullOrEmpty (nic.GetPhysicalAddress ().ToString ())) {

					foreach (UnicastIPAddressInformation ip in nic.GetIPProperties().UnicastAddresses)
					{
						if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
						{
							if (!String.IsNullOrEmpty (ip.Address.ToString ())) {
								Console.WriteLine (ip.Address.ToString ());
								MacAddress = nic.GetPhysicalAddress ().ToString ();
								return MacAddress;
							}
						}
					}
				}
			}

			throw new ActiveNetworkInterfaceNotFoundException ();
		}
	}

	public class ActiveNetworkInterfaceNotFoundException : Exception
	{
		public ActiveNetworkInterfaceNotFoundException() : base() {}

		public ActiveNetworkInterfaceNotFoundException(string message) : base(message) {}
	}
}
