using UnityEngine;

namespace C.Debugging.Rows
{

internal class NameValueRow : Row
{
	
	private readonly Cell labelCell;
	private readonly Cell valueCell;
	
	public override float ColumnWidth(int index) => (index == 1 ? valueCell.Size : labelCell.Size).x;
	
	public NameValueRow()
	{
		AssetReferences.Create(DebugInfo.Assets.cellPrefab, out labelCell, "Label", DebugInfo.PoolContainer);
		AssetReferences.Create(DebugInfo.Assets.cellPrefab, out valueCell, "Value", DebugInfo.PoolContainer);
		
		labelCell.AlignRight();
	}
	
	public void Set(string label, string value, Color? color, Color? backgroundColor)
	{
		labelCell.Set(label, color, backgroundColor);
		valueCell.Set(value, color, backgroundColor);
		
		Size = new Vector2(
			labelCell.Size.x + valueCell.Size.x,
			Mathf.Max(labelCell.Size.y, valueCell.Size.y)
		);
	}
	
	public override void Activate(DebugInfoTable table)
	{
		labelCell.transform.SetParent(table.Root);
		valueCell.transform.SetParent(table.Root);
	}
	
	public override void Deactivate()
	{
		labelCell.transform.SetParent(DebugInfo.PoolContainer);
		valueCell.transform.SetParent(DebugInfo.PoolContainer);
		RowPool<NameValueRow>.Release(this);
	}
	
	public override void UpdateLayout(float y, float[] columnWidths)
	{
		labelCell.UpdateLayout(
			new Vector2(0, y),
			new Vector2(columnWidths[0], Size.y));
		valueCell.UpdateLayout(
			new Vector2(columnWidths[0] + DebugInfo.Config.cellSpacing.x, y),
			new Vector2(columnWidths[1], Size.y));
	}
	
}

}
