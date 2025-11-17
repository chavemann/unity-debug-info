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
	
	public void Set(string label, Color? color, Color? bgColor, Color? borderColor, TextAnchor? alignment)
	{
		labelCell.Set(label, color, bgColor, borderColor, alignment);
		Size = labelCell.Size;
	}
	
	internal override void OnRemoved()
	{
		labelCell.textField.alignment = TextAnchor.UpperLeft;
		
		base.OnRemoved();
	}
	
}

}
