using System;
using System.Runtime.CompilerServices;
using C.Debugging.Rows;
using UnityEngine;

// ReSharper disable UnusedMember.Global

namespace C.Debugging
{

public class DebugInfoTable : MonoBehaviour
{
	
	private const int MaxWidthShrinkTime = 1;
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
	private readonly float[] columnWidths = new float[ColumnCount];
	
	private readonly float[] maxColumnWidths = new float[ColumnCount];
	private readonly float[] maxColumnWidthTimers = new float[ColumnCount];
	
	private void Awake()
	{
		config = DebugInfo.Config;
		
		root.anchoredPosition = new Vector2(config.margin.x, -config.margin.y);
	}
	
	#region -- Logging Methods --------------------------------
	
	/// <summary>
	/// Adds empty space between the previous and next row.
	/// </summary>
	/// <param name="space">If not provided the default spacing set in <see cref="DebugInfo.Config"/> will be used.</param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public DebugInfoTable Spacer(float? space = null)
	{
		NextRow<SpacerRow>().Set(space);
		return this;
	}
	
	/// <summary>
	/// A header only has a single column, is slightly larger than normal rows, and has a subtle bottom border.
	/// </summary>
	/// <param name="label">The heading text.</param>
	/// <param name="color">The text color.</param>
	/// <param name="bgColor">The background color.</param>
	/// <param name="borderColor">The bottom border color.</param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public DebugInfoTable Heading(string label, Color? color = null, Color? bgColor = null, Color? borderColor = null)
	{
		NextRow<HeadingRow>().Set(label, color, bgColor, borderColor);
		return this;
	}
	
	/// <summary>
	/// Displays a row with a single column of text.
	/// </summary>
	/// <param name="label">The text to display1111.</param>
	/// <param name="color">The text color.</param>
	/// <param name="bgColor">The background color.</param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public DebugInfoTable Log(string label, Color? color = null, Color? bgColor = null)
	{
		NextRow<TextRow>().Set(label, color, bgColor);
		return this;
	}
	
	/// <summary>
	/// Wraps subsequent logs in a collapsable group.<br/>
	/// Returns an <see cref="IDisposable"/> that must be used with the <c>using</c> statement.<br/>
	/// Usage:
	/// <code>using (DebugInfo.Group("My Group")) { /* ... */ }</code>
	/// </summary>
	/// <param name="label">The group heading text.</param>
	/// <param name="color">The text color.</param>
	/// <param name="bgColor">The background color.</param>
	/// <param name="borderColor">The bottom border color.</param>
	/// <param name="collapsed">If non-null, sets the default state when the group is first created.</param>
	/// <param name="onCollapsed">A callback triggered when the group is toggled by the user.</param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public GroupScope Group(
		string label, Color? color = null, Color? bgColor = null, Color? borderColor = null,
		bool? collapsed = null, Action<GroupHeadingRow, bool> onCollapsed = null)
	{
		GroupHeadingRow row = NextRow<GroupHeadingRow>();
		row.Set(label, color, bgColor, borderColor, collapsed, onCollapsed);
		return new GroupScope(this, row);
	}
	
	/// <summary>
	/// Displays a row with a key and value column.
	/// </summary>
	/// <param name="label">The key/label column text.</param>
	/// <param name="value">The value column text.</param>
	/// <param name="color">The text color for the whole row.</param>
	/// <param name="bgColor">The background color.</param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public DebugInfoTable Log(string label, string value, Color? color = null, Color? bgColor = null)
	{
		NextRow<NameValueRow>().Set(label, value, color, color, bgColor);
		return this;
	}
	
	/// <summary>
	/// Displays a row with a key and value column each with a different color.
	/// </summary>
	/// <param name="label">The key/label column text.</param>
	/// <param name="labelColor">The key/label column text color.</param>
	/// <param name="value">The value column text.</param>
	/// <param name="valueColor">The value column text color.</param>
	/// <param name="bgColor">The background color.</param>
	/// <returns></returns>
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
		
		for (int i = 0; i < rowCount; i++)
		{
			Row row = rows[i];
			
			columnWidths[0] = Mathf.Max(columnWidths[0], row.ColumnWidth(0));
			columnWidths[1] = Mathf.Max(columnWidths[1], row.ColumnWidth(1));
		}
		
		float totaWidth = 0;
		for (int i = 0; i < columnWidths.Length; i++)
		{
			float columnWidth = columnWidths[i];
			
			if (columnWidth >= maxColumnWidths[i])
			{
				maxColumnWidths[i] = columnWidth;
				maxColumnWidthTimers[i] = MaxWidthShrinkTime;
			}
			else if (maxColumnWidthTimers[i] > 0)
			{
				maxColumnWidthTimers[i] -= Time.fixedUnscaledDeltaTime;
				
				if (maxColumnWidthTimers[i] <= 0)
				{
					maxColumnWidths[i] = columnWidth;
				}
				else
				{
					columnWidth = maxColumnWidths[i];
					columnWidths[i] = columnWidth;
				}
			}
			
			totaWidth += columnWidth;
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
