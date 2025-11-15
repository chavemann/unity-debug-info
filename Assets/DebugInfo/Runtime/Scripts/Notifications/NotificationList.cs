using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using C.Debugging.Formatting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

// ReSharper disable UnusedMember.Global

namespace C.Debugging.Notifications
{

public class NotificationList : MonoBehaviour
{
	
	[SerializeField]
	public new RectTransform transform;
	
	private IObjectPool<Notification> notificationPool;
	private readonly List<Notification> notifications = new();
	private readonly Dictionary<string, Notification> notificationIds = new();
	
	private bool pendingLayout;
	
	private void Awake()
	{
		notificationPool = new ObjectPool<Notification>(
			PoolCreateFunc, PoolGetFunc, PoolReleaseFunc, PoolDestroyFunc,
			Debug.isDebugBuild);
		
		gameObject.SetActive(false);
	}
	
	private Notification PoolCreateFunc()
		=> AssetReferences.Create<Notification>(DebugInfo.Assets.notificationPrefab, "Notification", transform);
	
	private static void PoolGetFunc(Notification notification)
	{
		notification.StartFadeIn();
		notification.gameObject.SetActive(true);
	}
	
	private static void PoolReleaseFunc(Notification notification) => notification.gameObject.SetActive(false);
	
	private static void PoolDestroyFunc(Notification notification)
	{
		if (Application.isPlaying)
		{
			Destroy(notification);
		}
	}
	
	/// <summary>
	/// Shows a temporary notification message.
	/// </summary>
	/// <param name="message"></param>
	/// <param name="id"></param>
	/// <param name="borderColor"></param>
	/// <param name="bgColor"></param>
	/// <param name="color"></param>
	public void Notify(string message, string id = null, Color? borderColor = null, Color? bgColor = null, Color? color = null)
	{
		Notification notification = null;
		bool hasNotification = false;
		
		if (id != null)
		{
			hasNotification = notificationIds.TryGetValue(id, out notification);
		}
		
		if (!hasNotification)
		{
			notification = notificationPool.Get();
			notifications.Add(notification);
			
			if (id != null)
			{
				notification.id = id;
				notificationIds.Add(id, notification);
			}
		}
		
		notification.Set(message, borderColor, bgColor, color);
		
		pendingLayout = true;
		gameObject.SetActive(true);
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void NotifyToggle(string text, bool on, string onText, string offText, Color? borderColor = null, Color? bgColor = null, Color? color = null)
		=> Notify(Str.ToggleMsg(text, on, onText, offText), text, borderColor, bgColor, color);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void NotifyOn(string text, bool on, Color? borderColor = null, Color? bgColor = null, Color? color = null)
		=> Notify(Str.OnMsg(text, on), text, borderColor, bgColor, color);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void NotifyEnabled(string text, bool on, Color? borderColor = null, Color? bgColor = null, Color? color = null)
		=> Notify(Str.EnabledMsg(text, on), text, borderColor, bgColor, color);
	
	private void LateUpdate()
	{
		for (int i = notifications.Count - 1; i >= 0; i--)
		{
			Notification notification = notifications[i];
			if (notification.Tick())
				continue;
			
			notifications.RemoveAt(i);
			if (notification.id != null)
			{
				notificationIds.Remove(notification.id);
			}
			notificationPool.Release(notification);
		}
		
		if (pendingLayout)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(transform);
			LayoutRebuilder.ForceRebuildLayoutImmediate(transform);
			pendingLayout = false;
		}
		
		if (notifications.Count == 0)
		{
			gameObject.SetActive(false);
		}
	}
	
}

}
