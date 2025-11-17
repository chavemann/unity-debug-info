using UnityEngine;

namespace AssetStore.Scripts
{

public static class Extensions
{
	
	public static Color WithA(in this Color @this, float a)
	{
		return new Color(@this.r, @this.g, @this.b, a);
	}
	
}

}
