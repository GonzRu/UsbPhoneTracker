using System;
using System.Net;

namespace UsbPhoneTracker.Mac
{
	public class RequestHelper
	{
		private const string SERVER_API_URL = "http://localhost:3000/api/checkin/";

		static public void CheckInDevice (string deviceId, string macAddress)
		{
			string requestString = SERVER_API_URL + macAddress + "-" + deviceId;
			Console.WriteLine (requestString);
			WebRequest webRequest = WebRequest.Create (requestString);

			webRequest.GetResponse ();
			webRequest.Abort ();
		}
	}
}