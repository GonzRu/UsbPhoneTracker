using System;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace UsbPhoneTracker.Mac
{
	public class MacAddressHelper
	{
		static public string GetMd5HashFromAllMACAddress ()
		{
			string macAddresses = "";

			foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (!String.IsNullOrEmpty (nic.GetPhysicalAddress ().ToString ()))
					macAddresses += nic.GetPhysicalAddress ();
			}

			return GetMd5FromString (macAddresses);
		}

		static private string GetMd5FromString (string str)
		{
			MD5 md5 = System.Security.Cryptography.MD5.Create();
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(str);
			byte[] hashBytes = md5.ComputeHash(inputBytes);

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hashBytes.Length; i++)
			{
					sb.Append(hashBytes[i].ToString("X2"));
			}
			return sb.ToString();
		}
	}
}

