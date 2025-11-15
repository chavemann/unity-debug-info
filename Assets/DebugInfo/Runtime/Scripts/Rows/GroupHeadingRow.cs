using C.Debugging.Cells;
using UnityEngine;

namespace C.Debugging.Rows
{

public class GroupHeadingRow : HeadingRow
{
	
	protected new readonly GroupHeadingCell labelCell;
	
	private int childCount;
	
	private bool? collapsed;
	
	public bool Collapsed
	{
		get => collapsed ?? false;
		set
		{
			if (collapsed == value)
				return;
			
			collapsed = value;
			labelCell.Collapsed = collapsed.Value;
		}
	}
	
	public GroupHeadingRow() : base(DebugInfo.Assets.groupHeadingPrefab, "GroupHeading")
	{
		labelCell = (GroupHeadingCell) base.labelCell;
		labelCell.groupHeadingRow = this;
	}
	
	public void Set(string label, Color? color, Color? backgroundColor, Color? borderColor, bool? collapsed = null)
	{
		if (collapsed.HasValue && !this.collapsed.HasValue)
		{
			Collapsed = collapsed.Value;
		}
		
		base.Set(label, color, backgroundColor, borderColor);
	}
	
	protected override void ReturnToPool() => RowPool<GroupHeadingRow>.Release(this);
	
	public void AddChild(Row row)
	{
		row.Group = this;
		row.indentLevel = indentLevel + 1;
		row.Visible = Visible && !Collapsed;
		
		childCount++;
	}
	
	internal override void OnRemoved()
	{
		collapsed = false;
		labelCell.Collapsed = Collapsed;
		childCount = 0;
		
		base.OnRemoved();
	}
	
}

}
