using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Pool;
#if UNITY_6000_6_OR_NEWER
using Unity.Scripting.LifecycleManagement;
#endif

namespace C.DebuggingInfo.Rows
{

#if UNITY_6000_6_OR_NEWER
[NoAutoStaticsCleanup]
#endif
internal static class RowPool<T> where T : Row, new()
{
	
	private static readonly ObjectPool<T> Pool = new(CreateFunc, collectionCheck: Debug.isDebugBuild);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T Get() => Pool.Get();
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Release(T row) => Pool.Release(row);
	
	private static T CreateFunc() => new();
	
}

}
