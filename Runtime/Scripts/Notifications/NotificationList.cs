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
	/// <param name="message">The text to display.</param>
	/// <param name="id">An optional unique identifier for this notification. If non-null subsequent calls with the same id will
	///		update the existing notification instead of creating new ones.</param>
	/// <param name="borderColor">The right-hand side border color.
	///		If null, the default color set in <see cref="DebugInfo.Config"/> will be used.
	///		If the alpha is zero, no border will be shown. </param>
	/// <param name="bgColor">The background color.</param>
	/// <param name="color">The text color.</param>
	/// <param name="duration">If > 0, sets how long the notification will remain visible for,
	///		otherwise uses the default set in <see cref="DebugInfo.Config"/></param>
	public void Notify(string message, string id = null, Color? borderColor = null, Color? bgColor = null, Color? color = null, float duration = 0)
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
				notificationIds.Add(id, notification);
			}
		}
		
		notification.Set(message, id, borderColor, bgColor, color, duration);
		
		pendingLayout = true;
		gameObject.SetActive(true);
	}
	
	/// <summary>
	/// Displays a toggle message in the format <c>LABEL: ON/OFF</c> based on a condition.
	/// If the condition is true the <c>ON</c> text will be displayed, otherwise the <c>OFF</c> text is shown.<br/>
	/// This notification is automatically made unique based on the <c>text</c> parameter.
	/// </summary>
	/// <param name="text">The label text to display.</param>
	/// <param name="on">Whether to show the on or off text.</param>
	/// <param name="onText">The value text to show the condition is true.</param>
	/// <param name="offText">The value text to show the condition is false.</param>
	/// <param name="borderColor">The right-hand side border color.
	///		If null, the default color set in <see cref="DebugInfo.Config"/> will be used.
	/// 	If the alpha is zero, no border will be shown. </param>
	/// <param name="bgColor">The background color.</param>
	/// <param name="color">The text color.</param>
	/// <param name="duration">If > 0, sets how long the notification will remain visible for,
	/// 	otherwise uses the default set in <see cref="DebugInfo.Config"/></param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void NotifyToggle(
		string text, bool on, string onText, string offText, Color? borderColor = null, Color? bgColor = null, Color? color = null,
		float duration = 0f)
		=> Notify(Str.ToggleMsg(text, on, onText, offText), text, borderColor, bgColor, color, duration);
	
	/// <summary>
	/// Displays an on/off message in the format <c>LABEL: ON/OFF</c> based on a condition.<br/>
	/// If the condition is true the <c>ON</c> text will be displayed, otherwise the <c>OFF</c> text is shown.<br/>
	/// The on/off text can be globally controlled with the <see cref="Str.onText"/> and <see cref="Str.offText"/> fields.<br/>
	/// This notification is automatically made unique based on the <c>text</c> parameter.
	/// </summary>
	/// <param name="text">The label text to display.</param>
	/// <param name="on">Whether to show the on or off text.</param>
	/// <param name="borderColor">The right-hand side border color.
	///		If null, the default color set in <see cref="DebugInfo.Config"/> will be used.
	/// 	If the alpha is zero, no border will be shown. </param>
	/// <param name="bgColor">The background color.</param>
	/// <param name="color">The text color.</param>
	/// <param name="duration">If > 0, sets how long the notification will remain visible for,
	/// 	otherwise uses the default set in <see cref="DebugInfo.Config"/></param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void NotifyOn(
		string text, bool on, Color? borderColor = null, Color? bgColor = null, Color? color = null,
		float duration = 0f)
		=> Notify(Str.OnMsg(text, on), text, borderColor, bgColor, color, duration);
	
	/// <summary>
	/// Displays an enabled/disabled message in the format <c>LABEL: ENABLED/DISABLED</c> based on a condition.<br/>
	/// If the condition is true the <c>ON</c> text will be displayed, otherwise the <c>OFF</c> text is shown.<br/>
	/// The enabled/disabled text can be globally controlled with the <see cref="Str.enabledText"/> and <see cref="Str.disabledText"/> fields.<br/>
	/// This notification is automatically made unique based on the <c>text</c> parameter.
	/// </summary>
	/// <param name="text">The label text to display.</param>
	/// <param name="enabled">Whether to show the on or off text.</param>
	/// <param name="borderColor">The right-hand side border color.
	///		If null, the default color set in <see cref="DebugInfo.Config"/> will be used.
	/// 	If the alpha is zero, no border will be shown. </param>
	/// <param name="bgColor">The background color.</param>
	/// <param name="color">The text color.</param>
	/// <param name="duration">If > 0, sets how long the notification will remain visible for,
	/// 	otherwise uses the default set in <see cref="DebugInfo.Config"/></param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void NotifyEnabled(string text, bool enabled, Color? borderColor = null, Color? bgColor = null, Color? color = null,
		float duration = 0f)
		=> Notify(Str.EnabledMsg(text, enabled), text, borderColor, bgColor, color, duration);
	
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
