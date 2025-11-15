using C.Debugging.Cells;
using UnityEngine;

namespace C.Debugging.Rows
{

public class GroupHeadingRow : Row
{
	
	private readonly GroupHeadingCell labelCell;
	
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
	
	public GroupHeadingRow()
	{
		CreateCell(DebugInfo.Assets.groupHeadingPrefab, "GroupHeading", out labelCell);
		labelCell.groupHeadingRow = this;
	}
	
	public void Set(string label, Color? color, Color? backgroundColor, bool? collapsed = null)
	{
		if (collapsed.HasValue && !this.collapsed.HasValue)
		{
			Collapsed = collapsed.Value;
		}
		
		labelCell.Set(label, color, backgroundColor);
		Size = labelCell.Size;
	}
	
	public void AddChild(Row row)
	{
		row.Group = this;
		row.indentLevel = indentLevel + 1;
		row.Visible = Visible && !Collapsed;
		
		childCount++;
	}
	
	internal override void OnAdded(DebugInfoTable table)
	{
		base.OnAdded(table);
		
		labelCell.transform.SetParent(table.Root);
	}
	
	internal override void OnRemoved()
	{
		base.OnRemoved();
		
		collapsed = false;
		labelCell.Collapsed = Collapsed;
		childCount = 0;
		
		labelCell.transform.SetParent(DebugInfo.PoolContainer);
		RowPool<GroupHeadingRow>.Release(this);
	}
	
	protected override void UpdateVisible()
	{
		labelCell.gameObject.SetActive(Visible);
	}
	
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
