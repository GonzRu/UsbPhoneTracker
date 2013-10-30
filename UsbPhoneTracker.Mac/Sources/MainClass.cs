using System;
using UsbPhoneTracker.Common;

namespace UsbPhoneTracker.Mac
{
	public static class MainClass
	{
		static void Main(String[] args)
		{
			SetServerUrl (args);

			UsbNotifier.UsbChanged += UsbHandleHelper.HandleUsbChanged;
			UsbNotifier.Start();
		}

		static void SetServerUrl (String[] args)
		{
			string serverUrl;

			if (args.Length == 0) {
				Console.WriteLine ("В аргументах программы не найден адрес сервера.");
				serverUrl = "127.0.0.1";
			} else {
				serverUrl = args [0];
			}

			Console.WriteLine ("Адресс сервера: " + serverUrl);
			RequestHelper.SetServerUrl (serverUrl);
		}
	}
}

