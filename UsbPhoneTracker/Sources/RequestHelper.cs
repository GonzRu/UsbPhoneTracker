using System;
using System.Net;

namespace UsbPhoneTracker.Mac
{
	public class RequestHelper
	{
		static private string ServerUrl = null;

		static public void SetServerUrl (string serverUrl)
		{
			if (String.IsNullOrEmpty (serverUrl))
				throw new ArgumentException ();

			ServerUrl = serverUrl;
		}

		static public void CheckInDevice (string deviceId, string macAddress)
		{
			if (String.IsNullOrEmpty (ServerUrl))
				throw new ServerUrlNotSetException ();

			string requestString = "http://" + ServerUrl + ":3000/api/checkindevice/" + macAddress + "-" + deviceId;

			SendRequest (requestString);
		}

		static public void CheckInUser (string macAddress)
		{
			if (String.IsNullOrEmpty (ServerUrl))
				throw new ServerUrlNotSetException ();

			string requestString = "http://" + ServerUrl + ":3000/api/checkinuser/" + macAddress;

			SendRequest (requestString);
		}

		static private void SendRequest (string requestString)
		{
			try
			{
				WebRequest webRequest = WebRequest.Create (requestString);

				webRequest.GetResponse ();
				webRequest.Abort ();
			}
			catch (ServerUrlNotSetException)
			{
				Console.WriteLine ("Не задан адрес сервера.");
			}
			catch (NotSupportedException)
			{
				Console.WriteLine ("Неправильно сформирована строка запроса");
			}
			catch (WebException)
			{
				Console.WriteLine ("Сервер не ожидает запроса. Соединение сброшено.");
			}
		}
	}

	public class ServerUrlNotSetException : Exception
	{
		public ServerUrlNotSetException() : base() {}

		public ServerUrlNotSetException(string message) : base(message) {}
	}
}