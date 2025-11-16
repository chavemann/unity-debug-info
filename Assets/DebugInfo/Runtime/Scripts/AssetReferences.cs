using C.Debugging.Cells;
using UnityEngine;
using UnityEngine.EventSystems;

namespace C.Debugging
{

[CreateAssetMenu(fileName = "AssetReferences", menuName = "DebugInfo.AssetReferences")]
internal class AssetReferences : ScriptableObject
{
	
	public DebugInfo rootPrefab;
	public EventSystem eventSystemPrefab;
	public DebugInfoTable tablePrefab;
	public Cell cellPrefab;
	public IndentMargin indentMarginPrefab;
	public Cell headingPrefab;
	public Cell groupHeadingPrefab;
	public GameObject notificationPrefab;
	
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
