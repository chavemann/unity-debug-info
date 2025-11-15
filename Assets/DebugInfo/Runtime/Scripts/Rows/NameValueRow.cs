using C.Debugging.Cells;
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
		CreateCell(DebugInfo.Assets.cellPrefab, "Label", out labelCell);
		CreateCell(DebugInfo.Assets.cellPrefab, "Value", out valueCell);
		
		labelCell.textField.alignment = TextAnchor.UpperRight;
	}
	
	public void Set(string label, string value, Color? labelColor, Color? valueColor, Color? backgroundColor)
	{
		labelCell.Set(label, labelColor, backgroundColor);
		valueCell.Set(value, valueColor, backgroundColor);
		
		Size = new Vector2(
			labelCell.Size.x + valueCell.Size.x,
			Mathf.Max(labelCell.Size.y, valueCell.Size.y)
		);
	}
	
	public override void OnAdded(DebugInfoTable table)
	{
		base.OnAdded(table);
		
		labelCell.transform.SetParent(table.Root);
		valueCell.transform.SetParent(table.Root);
	}
	
	public override void OnRemoved()
	{
		labelCell.transform.SetParent(DebugInfo.PoolContainer);
		valueCell.transform.SetParent(DebugInfo.PoolContainer);
		RowPool<NameValueRow>.Release(this);
	}
	
	public override void SetVisible(bool visible)
	{
		Visible = visible;
		
		labelCell.gameObject.SetActive(visible);
		valueCell.gameObject.SetActive(visible);
	}
	
	public override void UpdateLayout(float y, float totalWidth, float[] columnWidths)
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
