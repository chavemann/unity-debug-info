using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using C.Debugging.Notifications;
using C.Debugging.Rows;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace C.Debugging
{

// TODO: Frame history?
// TODO: Editor window?

/// <summary>
/// The core DebugInfo class with various static methods for logging information.
/// </summary>
[DefaultExecutionOrder(10000)]
public class DebugInfo : MonoBehaviour
{
	
	// ReSharper disable MemberCanBePrivate.Global
	// ReSharper disable FieldCanBeMadeReadOnly.Global
	// ReSharper disable ConvertToConstant.Global
	
	/// <summary>
	/// The DebugInfo GameObject instance.
	/// </summary>
	/// <exclude/>
	public static DebugInfo Instance { get; private set; }
	
	/// <summary>
	/// The default table instance where logs displayed.
	/// DebugInfo provides static convenience methods for most methods so normally accessing this directly isn't needed.<br/>
	/// Note: At the moment only a single table is supported.
	/// </summary>
	/// <exclude/>
	public static DebugInfoTable DefaultTable { get; private set; }
	
	/// <summary>
	/// Global configuration for DebugInfo.
	/// Changes to this should be done before using DebugInfo to ensure that the values
	/// are correctly applied to logs/notifications.
	/// </summary>
	public static Config Config => Instance.config;
	
	// ReSharper restore MemberCanBePrivate.Global
	// ReSharper restore FieldCanBeMadeReadOnly.Global
	// ReSharper restore ConvertToConstant.Global
	
	internal static Transform PoolContainer => Instance.poolContainer;
	internal static AssetReferences Assets { get; private set; }
	
	[SerializeField]
	private new Transform transform;
	[SerializeField]
	private Transform poolContainer;
	[SerializeField]
	private NotificationList notificationList;
	
	private EventSystem eventSystem;
	
	[SerializeField]
	private Config config = new();
	
	private readonly List<DebugInfoTable> tables = new();
	
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void RuntimeInit()
	{
		Assets = Resources.Load<AssetReferences>("AssetReferences");
		
		Instance = AssetReferences.Create<DebugInfo>(Assets.rootPrefab);
	}
	
	private void Awake()
	{
		Instance = this;
		DontDestroyOnLoad(gameObject);
		
		poolContainer.gameObject.SetActive(false);
		
		eventSystem = FindFirstObjectByType<EventSystem>(FindObjectsInactive.Include);
		if (!eventSystem)
		{
			AssetReferences.Create(Assets.eventSystemPrefab, out eventSystem, parent: transform);
		}
		
		DefaultTable = AssetReferences.Create<DebugInfoTable>(Assets.tablePrefab, "Default", transform);
		tables.Add(DefaultTable);
	}
	
	/// <summary>
	/// Make sure to call this if <see cref="Debugging.Config.updateMode"/> is set to <see cref="UpdateMode.Manual"/>.<br/>
	/// If the update mode is not set to <see cref="UpdateMode.Manual"/>, this will do nothing and issue a warning.<br/>
	/// Failing to call this when the update mode is manual will prevent previous frame logs from being reset,
	/// causing memory leaks and performance issues.
	/// </summary>
	[UsedImplicitly]
	public static void UpdateAll()
	{
		#if UNITY_EDITOR
		if (Config.updateMode != UpdateMode.Manual)
		{
			Debug.LogWarning("Only call UpdateAll when updateMode is set to Manual.");
			return;
		}
		#endif
		
		Instance.UpdateImpl();
	}
	
	private void Update()
	{
		if (config.updateMode != UpdateMode.Update)
			return;
		
		UpdateImpl();
	}
	
	private void FixedUpdate()
	{
		if (config.updateMode != UpdateMode.FixedUpdate)
			return;
		
		UpdateImpl();
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void UpdateImpl()
	{
		foreach (DebugInfoTable table in tables)
		{
			table.UpdateImpl();
		}
	}
	
	#region -- Notifications --------------------------------
	// ReSharper disable UnusedMethodReturnValue.Global
	// ReSharper disable UnusedMember.Global
	
	/// <inheritdoc cref="NotificationList.Notify"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Notify(
		string message, string id = null, Color? borderColor = null, Color? bgColor = null, Color? color = null, float duration = 0)
		=> Instance.notificationList.Notify(message, id, borderColor, bgColor, color, duration);
	
	/// <inheritdoc cref="NotificationList.NotifyToggle"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void NotifyToggle(
		string text, bool on, string onText, string offText, Color? borderColor = null, Color? bgColor = null, Color? color = null, float duration = 0)
		=> Instance.notificationList.NotifyToggle(text, on, onText, offText, borderColor, bgColor, color, duration);
	
	/// <inheritdoc cref="NotificationList.NotifyOn"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void NotifyOn(string text, bool on, Color? borderColor = null, Color? bgColor = null, Color? color = null, float duration = 0)
		=> Instance.notificationList.NotifyOn(text, on, borderColor, bgColor, color, duration);
	
	/// <inheritdoc cref="NotificationList.NotifyEnabled"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void NotifyEnabled(string text, bool on, Color? borderColor = null, Color? bgColor = null, Color? color = null, float duration = 0)
		=> Instance.notificationList.NotifyEnabled(text, on, borderColor, bgColor, color, duration);
	
	// ReSharper restore UnusedMethodReturnValue.Global
	// ReSharper restore UnusedMember.Global
	#endregion -----------------------------------------
	
	#region -- Static Logging Convenience Methods --------------------------------
	// ReSharper disable UnusedMethodReturnValue.Global
	// ReSharper disable UnusedMember.Global
	
	/// <inheritdoc cref="DebugInfoTable.Spacer"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DebugInfoTable Spacer(float? space = null) => DefaultTable.Spacer(space);
	
	/// <inheritdoc cref="DebugInfoTable.Heading"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DebugInfoTable Heading(string label, Color? color = null, Color? bgColor = null, Color? borderColor = null, TextAnchor? alignment = null)
		=> DefaultTable.Heading(label, color, bgColor, borderColor, alignment);
	
	/// <inheritdoc cref="DebugInfoTable.Group"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static GroupScope Group(
		string label, Color? color = null, Color? bgColor = null, Color? borderColor = null,
		bool? collapsed = null, Action<GroupHeadingRow, bool> onCollapsed = null
	)
		=> DefaultTable.Group(label, color, bgColor, borderColor, collapsed, onCollapsed);
	
	/// <summary>
	/// A convenience method for conditionally wrapping logs in a group.<br/>
	/// Usage:
	/// <code>using (DebugInfo.TryGroup(condition) ?? DebugInfo.Group("My Group")) { /* ... */ }</code>
	/// </summary>
	/// <param name="condition">If true, the group will work as per normal.
	/// If false, no group will be created and subsequent logs will be output normally.</param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IDisposable TryGroup(bool condition) => condition ? null : new IgnoredGroup();
	
	/// <inheritdoc cref="DebugInfoTable.Log(string,Color?,Color?)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DebugInfoTable Log(string label, Color? color = null, Color? bgColor = null)
		=> DefaultTable.Log(label, color, bgColor);
	
	/// <inheritdoc cref="DebugInfoTable.Log(string,string,Color?,Color?)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DebugInfoTable Log(string label, string value, Color? color = null, Color? bgColor = null)
		=> DefaultTable.Log(label, value, color, bgColor);
	
	/// <inheritdoc cref="DebugInfoTable.Log(string,Color?,string,Color?,Color?)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DebugInfoTable Log(string label, Color? labelColor, string value, Color? valueColor, Color? bgColor = null)
	{
		return DefaultTable.Log(label, labelColor, value, valueColor, bgColor);
	}
	
	// ReSharper restore UnusedMethodReturnValue.Global
	// ReSharper restore UnusedMember.Global
	#endregion -----------------------------------------
	
}

}
