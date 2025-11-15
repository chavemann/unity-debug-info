using C.Debugging.Cells;
using UnityEngine;

namespace C.Debugging.Rows
{

public abstract class BasicRow : Row
{
	
	protected readonly Cell labelCell;
	
	protected BasicRow(Cell cellPrefab, string name)
	{
		CreateCell(cellPrefab, name, out labelCell);
		labelCell.row = this;
	}
	
	public void Set(string label, Color? color, Color? backgroundColor)
	{
		labelCell.Set(label, color, backgroundColor);
		Size = labelCell.Size;
	}
	
	internal override void OnAdded(DebugInfoTable table)
	{
		base.OnAdded(table);
		
		labelCell.transform.SetParent(table.Root);
	}
	
	internal override void OnRemoved()
	{
		base.OnRemoved();
		
		labelCell.transform.SetParent(DebugInfo.PoolContainer);
		ReturnToPool();
	}
	
	protected abstract void ReturnToPool();
	
	protected override void UpdateVisible() => labelCell.gameObject.SetActive(Visible);
	
	internal override void UpdateLayout(float y, float totalWidth, float[] columnWidths)
	{
		labelCell.UpdateLayout(
			new Vector2(0, y),
			new Vector2(totalWidth, Size.y));
	}
	
	protected override bool ShowIndentMargin => true;
	
	protected override Cell IndentMarginContainer => labelCell;
	
}

}
