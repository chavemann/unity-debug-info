using System;
using System.Runtime.CompilerServices;
using C.Debugging.Rows;
using UnityEngine;

namespace C.Debugging
{

public class DebugInfoTable : MonoBehaviour
{
	
	[SerializeField]
	private new RectTransform transform;
	[SerializeField]
	private Canvas canvas;
	[SerializeField]
	private RectTransform root;
	
	public RectTransform Root => root;
	
	private Config config;
	
	private Row[] rows = Array.Empty<Row>();
	private int rowCount;
	private int previousRowCount;
	
	private void Awake()
	{
		config = DebugInfo.Config;
		
		root.anchoredPosition = new Vector2(config.margin.x, -config.margin.y);
	}
	
	#region -- Logging Methods --------------------------------
	
	public DebugInfoTable Log(string label, string value, Color? color = null, Color? backgroundColor = null)
	{
		NextRow().Set(label, value, color, backgroundColor);
		return this;
	}
	
	#endregion -----------------------------------------
	
	internal void UpdateImpl()
	{
		float labelColumnWidth = 0;
		float valueColumnWidth = 0;
		
		for (int i = 0; i < rowCount; i++)
		{
			Row row = rows[i];
			labelColumnWidth = Mathf.Max(labelColumnWidth, row.labelCell.Size.x);
			valueColumnWidth = Mathf.Max(valueColumnWidth, row.valueCell.Size.x);
		}
		
		float y = 0;
		
		for (int i = 0; i < rowCount; i++)
		{
			Row row = rows[i];
			row.UpdateLayout(y, labelColumnWidth, valueColumnWidth);
			y -= row.Height + config.cellSpacing.y;
		}
		
		// Disable unused rows at the end of the array.
		if (rowCount < previousRowCount)
		{
			for (int i = rowCount; i < previousRowCount; i++)
			{
				rows[i].Deactivate();
			}
		}
		// Enable new rows at the end of the array.
		else if (rowCount > previousRowCount)
		{
			for (int i = previousRowCount; i < rowCount; i++)
			{
				rows[i].Activate();
			}
		}
		
		previousRowCount = rowCount;
		rowCount = 0;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private Row NextRow()
	{
		if (rowCount + 1 > rows.Length)
		{
			ExpandCapacity();
		}
		
		return rows[rowCount++];
	}
	
	private void ExpandCapacity()
	{
		int previousCapacity = rows.Length;
		Array.Resize(ref rows, rows.Length > 0 ? rows.Length * 2 : 8);
		
		for (int i = previousCapacity; i < rows.Length; i++)
		{
			rows[i] = new Row(this, i);
		}
	}
	
}

}
