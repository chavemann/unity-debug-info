using C.Debugging.Cells;
using UnityEngine;

namespace C.Debugging.Rows
{

internal class HeadingRow : Row
{
	
	private readonly HeadingCell labelCell;
	
	public HeadingRow()
	{
		AssetReferences.Create(DebugInfo.Assets.headingPrefab, out labelCell, "Heading", DebugInfo.PoolContainer);
		labelCell.row = this;
	}
	
	public void Set(string label, Color? color, Color? backgroundColor)
	{
		labelCell.Set(label, color, backgroundColor);
		Size = labelCell.Size;
	}
	
	public override void OnAdded(DebugInfoTable table)
	{
		base.OnAdded(table);
		
		labelCell.transform.SetParent(table.Root);
	}
	
	public override void OnRemoved()
	{
		labelCell.transform.SetParent(DebugInfo.PoolContainer);
		RowPool<HeadingRow>.Release(this);
	}
	
	public override void SetVisible(bool visible)
	{
		Visible = visible;
		labelCell.gameObject.SetActive(visible);
	}
	
	public override void UpdateLayout(float y, float totalWidth, float[] columnWidths)
	{
		labelCell.UpdateLayout(
			new Vector2(0, y),
			new Vector2(totalWidth, Size.y));
	}
	
}

}
