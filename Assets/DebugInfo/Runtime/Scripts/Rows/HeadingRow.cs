using C.Debugging.Cells;
using UnityEngine;

namespace C.Debugging.Rows
{

public class HeadingRow : BasicRow
{
	
	protected new readonly HeadingCell labelCell;
	
	public HeadingRow() : base(DebugInfo.Assets.headingPrefab, "Heading")
	{
		labelCell = (HeadingCell) base.labelCell;
	}
	
	protected HeadingRow(Cell cellPrefab, string name) : base(cellPrefab, name)
	{
		labelCell = (HeadingCell) base.labelCell;
	}
	
	protected override void ReturnToPool() => RowPool<HeadingRow>.Release(this);
	
	public void Set(string label, Color? color, Color? backgroundColor, Color? borderColor)
	{
		labelCell.Set(label, color, backgroundColor, borderColor);
		Size = labelCell.Size;
	}
	
}

}
