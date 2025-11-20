using C.Debugging.Cells;
using C.Debugging.Notifications;
using UnityEngine;
using UnityEngine.EventSystems;

namespace C.Debugging
{

// <exclude/>
[CreateAssetMenu(fileName = "AssetReferences", menuName = "DebugInfo.AssetReferences")]
public class AssetReferences : ScriptableObject
{
	
	public DebugInfo rootPrefab;
	public EventSystem eventSystemPrefab;
	public DebugInfoTable tablePrefab;
	public Cell cellPrefab;
	public IndentMargin indentMarginPrefab;
	public HeadingCell headingPrefab;
	public GroupHeadingCell groupHeadingPrefab;
	public Notification notificationPrefab;
	
	public static T Create<T>(GameObject prefab, string name = null, Transform parent = null)
	{
		GameObject newObj = Instantiate(prefab, parent, false);
		newObj.name = name ?? prefab.name;
		newObj.TryGetComponent(out T component);
		return component;
	}
	
	public static T Create<T>(MonoBehaviour prefab, string name = null, Transform parent = null)
	{
		return Create<T>(prefab.gameObject, name, parent);
	}
	
	public static void Create<T>(GameObject prefab, out T component, string name = null, Transform parent = null)
	{
		component = Create<T>(prefab, name, parent);
	}
	
	public static void Create<T>(MonoBehaviour prefab, out T component, string name = null, Transform parent = null)
	{
		Create(prefab.gameObject, out component, name, parent);
	}
	
}

}
