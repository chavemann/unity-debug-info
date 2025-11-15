using UnityEngine;

namespace C.Debugging.Rows
{

internal class SpacerRow : Row
{
	
	public void Set(float? space = null)
	{
		Size = new Vector2(0, space ?? DebugInfo.Config.defaultSpacerSize);
	}
	
	public override void OnRemoved() => RowPool<SpacerRow>.Release(this);
	
}

}
