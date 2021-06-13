using System;
using System.Reflection;

namespace Wolfram.NETLink.Internal
{
	internal class EventHelper
	{
		internal static string getDelegateTypeName(object eventsObject, string aqTypeName, string evtName)
		{
			return getEventInfo(eventsObject, aqTypeName, evtName).EventHandlerType.AssemblyQualifiedName;
		}

		internal static Delegate addHandler(object eventsObject, string aqTypeName, string evtName, Delegate dlg)
		{
			getEventInfo(eventsObject, aqTypeName, evtName).AddEventHandler(eventsObject, dlg);
			return dlg;
		}

		internal static void removeHandler(object eventsObject, string aqTypeName, string evtName, Delegate dlg)
		{
			getEventInfo(eventsObject, aqTypeName, evtName).RemoveEventHandler(eventsObject, dlg);
		}

		private static EventInfo getEventInfo(object eventsObject, string aqTypeName, string evtName)
		{
			EventInfo eventInfo = null;
			if (eventsObject != null)
			{
				EventInfo[] events = eventsObject.GetType().GetEvents(BindingFlags.Instance | BindingFlags.Public);
				foreach (EventInfo eventInfo2 in events)
				{
					if (Utils.memberNamesMatch(eventInfo2.Name, evtName))
					{
						eventInfo = eventInfo2;
						break;
					}
				}
				if (eventInfo == null)
					throw new ArgumentException("No public instance event named " + evtName + " exists for the given object.");
			}
			else
			{
				EventInfo[] events2 = TypeLoader.GetType(aqTypeName, throwOnError: true).GetEvents(BindingFlags.Static | BindingFlags.Public);
				foreach (EventInfo eventInfo3 in events2)
				{
					if (Utils.memberNamesMatch(eventInfo3.Name, evtName))
					{
						eventInfo = eventInfo3;
						break;
					}
				}
				if (eventInfo == null)
					throw new ArgumentException("No public static event named " + evtName + " exists for the type " + aqTypeName.Split(',')[0] + ".");
			}
			return eventInfo;
		}
	}
}
