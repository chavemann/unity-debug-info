using System;
using System.Runtime.CompilerServices;
using C.Debugging.Rows;
using UnityEngine;

// ReSharper disable UnusedMember.Global

namespace C.Debugging
{

public class DebugInfoTable : MonoBehaviour
{
	
	private const int ColumnCount = 2;
	
	[SerializeField]
	private new RectTransform transform;
	[SerializeField]
	private Canvas canvas;
	[SerializeField]
	private RectTransform root;
	
	public RectTransform Root => root;
	
	public GroupHeadingRow CurrentGroup { get; internal set; }
	
	private Config config;
	
	private Row[] rows = Array.Empty<Row>();
	private int rowCount;
	private int previousRowCount;
	private readonly float[] columnWidths = new float[ColumnCount + 1];
	
	private void Awake()
	{
		config = DebugInfo.Config;
		
		root.anchoredPosition = new Vector2(config.margin.x, -config.margin.y);
	}
	
	#region -- Logging Methods --------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public DebugInfoTable Spacer(float? space = null)
	{
		NextRow<SpacerRow>().Set(space);
		return this;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public DebugInfoTable Heading(string label, Color? color = null, Color? bgColor = null, Color? borderColor = null)
	{
		NextRow<HeadingRow>().Set(label, color, bgColor, borderColor);
		return this;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public DebugInfoTable Log(string label, Color? color = null, Color? bgColor = null)
	{
		NextRow<TextRow>().Set(label, color, bgColor);
		return this;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public GroupScope Group(string label, Color? color = null, Color? bgColor = null, bool? collapsed = null)
	{
		GroupHeadingRow row = NextRow<GroupHeadingRow>();
		row.Set(label, color, bgColor, collapsed);
		return new GroupScope(this, row);
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public DebugInfoTable Log(string label, string value, Color? color = null, Color? bgColor = null)
	{
		NextRow<NameValueRow>().Set(label, value, color, color, bgColor);
		return this;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public DebugInfoTable Log(string label, Color? labelColor, string value, Color? valueColor, Color? bgColor = null)
	{
		NextRow<NameValueRow>().Set(label, value, labelColor, valueColor, bgColor);
		return this;
	}
	
	#endregion -----------------------------------------
	
	internal void UpdateImpl()
	{
		// Calculate the column widths.
		
		columnWidths[0] = 0;
		columnWidths[1] = 0;
		float totaWidth = 0;
		
		for (int i = 0; i < rowCount; i++)
		{
			Row row = rows[i];
			
			columnWidths[0] = Mathf.Max(columnWidths[0], row.ColumnWidth(0));
			columnWidths[1] = Mathf.Max(columnWidths[1], row.ColumnWidth(1));
			totaWidth = Mathf.Max(totaWidth, Mathf.Ceil(row.Size.x));
		}
		
		float totalColumnsWidth = 0;
		for (int i = 0; i < ColumnCount; i++)
		{
			totalColumnsWidth += columnWidths[i];
		}
		totaWidth = Mathf.Max(totaWidth, totalColumnsWidth);
		
		// Add any extra space to the last column.
		if (totaWidth > totalColumnsWidth)
		{
			columnWidths[ColumnCount - 1] += totaWidth - totalColumnsWidth;
		}
		
		totaWidth += config.cellSpacing.x * (ColumnCount - 1);
		
		// Position the rows.
		
		float y = 0;
		
		for (int i = 0; i < rowCount; i++)
		{
			Row row = rows[i];
			if (!row.Visible)
				continue;
			
			row.UpdateLayout(y, totaWidth, columnWidths);
			y -= row.Size.y + config.cellSpacing.y;
		}
		
		// Disable unused rows at the end of the array.
		if (rowCount < previousRowCount)
		{
			for (int i = rowCount; i < previousRowCount; i++)
			{
				if (rows[i] == null)
					continue;
				
				rows[i].OnRemoved();
				rows[i] = null;
			}
		}
		
		previousRowCount = rowCount;
		rowCount = 0;
		CurrentGroup = null;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private T NextRow<T>() where T : Row, new()
	{
		if (rowCount + 1 > rows.Length)
		{
			ExpandCapacity();
		}
		
		Row row = rows[rowCount];
		
		if (row is not T)
		{
			row?.OnRemoved();
			row = RowPool<T>.Get();
			row.OnAdded(this);
			rows[rowCount] = row;
		}
		
		row.Index = rowCount++;
		row.Group = CurrentGroup;
		
		if (row.Group != null)
		{
			row.Group.AddChild(row);
		}
		else
		{
			row.ClearGroup();
		}
		
		row.PrepareLayout();
		return (T) row;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void ExpandCapacity() => Array.Resize(ref rows, rows.Length > 0 ? rows.Length * 2 : 8);
	
}

}
