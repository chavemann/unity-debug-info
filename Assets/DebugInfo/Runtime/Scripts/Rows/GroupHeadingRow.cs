using System.Collections.Generic;
using C.Debugging.Cells;
using UnityEngine;

namespace C.Debugging.Rows
{

public class GroupHeadingRow : Row
{
	
	private readonly GroupHeadingCell labelCell;
	
	private readonly HashSet<Row> rows = new();
	
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
			SetChildrenVisible(true);
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
	
	public void Add(Row row)
	{
		if (!rows.Add(row))
			return;
		
		if (Collapsed || !Visible)
		{
			row.SetVisible(false);
		}
		
		row.Indent(indentLevel + 1);
	}
	
	public void Remove(Row row)
	{
		rows.Remove(row);
	}
	
	public override void OnAdded(DebugInfoTable table)
	{
		base.OnAdded(table);
		
		labelCell.transform.SetParent(table.Root);
	}
	
	public override void OnRemoved()
	{
		base.OnRemoved();
		
		if (Collapsed)
		{
			foreach (Row row in rows)
			{
				row.SetVisible(Visible);
			}
		}
		
		collapsed = false;
		labelCell.Collapsed = Collapsed;
		rows.Clear();
		
		labelCell.transform.SetParent(DebugInfo.PoolContainer);
		RowPool<GroupHeadingRow>.Release(this);
	}
	
	public override void SetVisible(bool visible)
	{
		Visible = visible;
		
		labelCell.gameObject.SetActive(visible);
		
		SetChildrenVisible(visible);
	}
	
	private void SetChildrenVisible(bool visible)
	{
		foreach (Row row in rows)
		{
			row.SetVisible(visible && Visible && !Collapsed);
		}
	}
	
	public override void UpdateLayout(float y, float totalWidth, float[] columnWidths)
	{
		labelCell.UpdateLayout(
			new Vector2(0, y),
			new Vector2(totalWidth, Size.y));
	}
	
	protected override bool ShowIndentMargin => true;
	
	protected override Cell IndentMarginContainer => labelCell;
	
}

}
