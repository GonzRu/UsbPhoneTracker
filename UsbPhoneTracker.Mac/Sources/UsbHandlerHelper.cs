using System;
using System.Collections.Generic;
using UsbPhoneTracker.Common;
using System.Linq;

namespace UsbPhoneTracker.Mac
{
	static public class UsbHandleHelper
	{
		static Dictionary<DeviceIds, HashSet<string>> ConnectedDevices = new Dictionary<DeviceIds, HashSet<String>>();

		static ISet<String> EmptySerials = new HashSet<String>();

		static ISet<String> ConnectedDevicesWithIds(DeviceIds ids)
		{
			try
			{
				return ConnectedDevices[ids];
			}
			catch
			{
				return EmptySerials;
			}
		}

		static String GetSerialNumber(Dictionary<string, string> deviceInfo)
		{
			String r;
			deviceInfo.TryGetValue("Serial Number", out r);
			r = r ?? "";
			return r;
		}

		static void AddDevice(DeviceIds device, String serial)
		{
			if (ConnectedDevices.ContainsKey(device))
			{
				ConnectedDevices[device].Add(serial);
			}
			else
			{
				var s = new HashSet<String> { serial };
				ConnectedDevices[device] = s;
			}
		}

		static public void HandleUsbChanged(DeviceIds device, Boolean connected)
		{
			try
			{
				if (connected)
				{
					var connectedWithSameIds = ConnectedDevicesWithIds(device);

					var deviceInfo = DeviceInfoFinder
						.GetAllDevicesInfo()
							.Where(DeviceInfoFinder.IDMatch(device))
							.Where(x => !connectedWithSameIds.Contains(GetSerialNumber(x)))
							.FirstOrDefault();


					if (deviceInfo == null)
					{
						Console.WriteLine("Can't find connected device!");
						return;
					}
					var serialNumber = GetSerialNumber(deviceInfo);
					AddDevice(device, serialNumber);

					Console.WriteLine(
						"Vendor ID: {0}\nDevice ID: {1}\nSerial Number: {2}\n",
						device.VendorId,
						device.ProductId,
						serialNumber);

					ChechInDevice (serialNumber);

				}
				else
				{
					var allInfo = DeviceInfoFinder.GetAllDevicesInfo().Select(DeviceInfoFinder.Extract).ToSet();
					var emptyKeys = new List<DeviceIds>();
					foreach (var ids in ConnectedDevices.Keys)
					{
						var serials = ConnectedDevices[ids];
						serials.RemoveWhere(x => !allInfo.Contains(Tuple.Create(ids, x)));
						if (serials.Count == 0)
						{
							emptyKeys.Add(ids);
						}
					}
					foreach (var ids in emptyKeys)
						ConnectedDevices.Remove(ids);

					Console.WriteLine("A device has been disconnected\n");
				}
			}	
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		static HashSet<T> ToSet<T>(this IEnumerable<T> src)
		{
			var r = new HashSet<T>();
			foreach (var i in src)
				r.Add(i);
			return r;
		}

		static private void ChechInDevice(string deviceSerialNumber)
		{
			try
			{
				RequestHelper.CheckInDevice(deviceSerialNumber, MacAddressHelper.GetMacAddress());
			}
			catch (ActiveNetworkInterfaceNotFoundException)
			{
				Console.WriteLine ("Не найден активный сетевой интерфейс.");
			}
			catch (Exception e)
			{
				Console.WriteLine ("Произошла непредвиденная ошибка: " + e.Message);
			}
		}
	}
}

