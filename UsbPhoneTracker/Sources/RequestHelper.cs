using System;
using System.Net;

namespace UsbPhoneTracker.Mac
{
	public class RequestHelper
	{
		static public void CheckInDevice (string serverUrl, string deviceId, string macAddress)
		{
			string requestString = "http://" + serverUrl + ":3000/api/checkindevice/" + macAddress + "-" + deviceId;
			Console.WriteLine (requestString);
			WebRequest webRequest = WebRequest.Create (requestString);

			webRequest.GetResponse ();
			webRequest.Abort ();
		}

		static public void CheckInUser (string serverUrl, string macAddress)
		{
			string requestString = "http://" + serverUrl + ":3000/api/checkinuser/" + macAddress;
			Console.WriteLine (requestString);
			WebRequest webRequest = WebRequest.Create (requestString);

			webRequest.GetResponse ();
			webRequest.Abort ();
		}
	}
}