using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using C.Debugging.Notifications;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace C.Debugging
{

// TODO: Make bgColor and backgroundColor  names consistent
// TODO: Frame history?
// TODO: Smooth out width changes?
// TODO: Editor window?
// TODO: Stack trace, click to go to?
// TODO: Doc comments for all public methods and fields
[DefaultExecutionOrder(10000)]
public class DebugInfo : MonoBehaviour
{
	
	// ReSharper disable MemberCanBePrivate.Global
	// ReSharper disable FieldCanBeMadeReadOnly.Global
	// ReSharper disable ConvertToConstant.Global
	public static DebugInfo Instance { get; private set; }
	public static DebugInfoTable DefaultTable { get; private set; }
	public static AssetReferences Assets { get; private set; }
	public static Config Config => Instance.config;
	public static Transform PoolContainer => Instance.poolContainer;
	// ReSharper restore MemberCanBePrivate.Global
	// ReSharper restore FieldCanBeMadeReadOnly.Global
	// ReSharper restore ConvertToConstant.Global
	
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
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DebugInfoTable Spacer(float? space = null) => DefaultTable.Spacer(space);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DebugInfoTable Heading(string label, Color? color = null, Color? bgColor = null, Color? borderColor = null)
		=> DefaultTable.Heading(label, color, bgColor, borderColor);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static GroupScope Group(string label, Color? color = null, Color? bgColor = null, Color? borderColor = null, bool? collapsed = null)
		=> DefaultTable.Group(label, color, bgColor, borderColor, collapsed);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static IDisposable TryGroup(bool condition) => condition ? null : new IgnoredGroup();
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DebugInfoTable Log(string label, Color? color = null, Color? bgColor = null)
		=> DefaultTable.Log(label, color, bgColor);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DebugInfoTable Log(string label, string value, Color? color = null, Color? bgColor = null)
		=> DefaultTable.Log(label, value, color, bgColor);
	
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
