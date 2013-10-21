using System;
using System.Net;

namespace UsbPhoneTracker.Mac
{
	public class RequestHelper
	{
		private const string SERVER_API_URL = "http://localhost:3000/api/checkin/";

		static public void CheckInDevice (string serverUrl, string deviceId, string macAddress)
		{
			string requestString = serverUrl + ":3000/api/checkin/" + macAddress + "-" + deviceId;
			WebRequest webRequest = WebRequest.Create (requestString);

			webRequest.GetResponse ();
			webRequest.Abort ();
		}
	}
}