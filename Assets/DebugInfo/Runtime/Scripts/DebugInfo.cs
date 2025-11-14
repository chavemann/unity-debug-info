using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace C.Debugging
{

[DefaultExecutionOrder(10000)]
public class DebugInfo : MonoBehaviour
{
	
	// ReSharper disable MemberCanBePrivate.Global
	public static DebugInfo Instance { get; private set; }
	public static DebugInfoTable DefaultTable { get; private set; }
	public static AssetReferences Assets { get; private set; }
	public static Config Config => Instance.config;
	public static Transform PoolContainer => Instance.poolContainer;
	// ReSharper restore MemberCanBePrivate.Global
	
	public static UpdateMode updateMode = UpdateMode.Update;
	
	[SerializeField]
	private new Transform transform;
	[SerializeField]
	private Transform poolContainer;
	
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
		if (updateMode != UpdateMode.Manual)
		{
			Debug.LogWarning("Only call UpdateAll when updateMode is set to Manual.");
			return;
		}
		
		Instance.UpdateImpl();
	}
	
	private void Update()
	{
		if (updateMode != UpdateMode.Update)
			return;
		
		UpdateImpl();
	}
	
	private void FixedUpdate()
	{
		if (updateMode != UpdateMode.FixedUpdate)
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
	
	#region -- Static Logging Convenience Methods --------------------------------
	// ReSharper disable UnusedMethodReturnValue.Global
	// ReSharper disable UnusedMember.Global
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DebugInfoTable Spacer(float? space = null) => DefaultTable.Spacer(space);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DebugInfoTable Log(string label, string value, Color? color = null, Color? bgColor = null)
		=> DefaultTable.Log(label, value, color, bgColor);
	
	// ReSharper restore UnusedMethodReturnValue.Global
	// ReSharper restore UnusedMember.Global
	#endregion -----------------------------------------
	
}

}
