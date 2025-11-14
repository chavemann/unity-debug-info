using C.Debugging.Cells;
using UnityEngine;

namespace C.Debugging.Rows
{

internal class HeadingRow : Row
{
	
	private readonly Cell labelCell;
	
	public HeadingRow()
	{
		AssetReferences.Create(DebugInfo.Assets.headingPrefab, out labelCell, "Heading", DebugInfo.PoolContainer);
	}
	
	public void Set(string label, Color? color, Color? backgroundColor)
	{
		labelCell.Set(label, color, backgroundColor);
		Size = labelCell.Size;
	}
	
	public override void Activate(DebugInfoTable table)
	{
		base.Activate(table);
		
		labelCell.transform.SetParent(table.Root);
	}
	
	public override void Deactivate()
	{
		labelCell.transform.SetParent(DebugInfo.PoolContainer);
		RowPool<HeadingRow>.Release(this);
	}
	
	public override void UpdateLayout(float y, float totalWidth, float[] columnWidths)
	{
		labelCell.UpdateLayout(
			new Vector2(0, y),
			new Vector2(totalWidth, Size.y));
	}
	
}

}
