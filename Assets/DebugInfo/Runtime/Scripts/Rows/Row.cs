using C.Debugging.Cells;
using UnityEngine;

namespace C.Debugging.Rows
{

public abstract class Row
{
	
	public DebugInfoTable Table { get; private set; }
	public GroupHeadingRow Group { get; internal set; }
	
	public Vector2 Size { get; protected set; }
	
	public bool Visible { get; protected set; } = true;
	
	private IndentMargin indentMargin;
	private bool hasIndentMargin;
	protected int indentLevel;
	
	protected void CreateCell<T>(Cell prefab, string name, out T cell) where T : Cell
	{
		AssetReferences.Create(prefab, out cell, name, DebugInfo.PoolContainer);
		cell.row = this;
	}
	
	public virtual float ColumnWidth(int index) => 0;
	
	public virtual void OnAdded(DebugInfoTable table)
	{
		Table = table;
		Group = null;
	}
	
	public virtual void OnRemoved()
	{
		if (hasIndentMargin && indentLevel > 0)
		{
			indentMargin.gameObject.SetActive(false);
			indentLevel = 0;
		}
	}
	
	public virtual void SetVisible(bool visible) => Visible = visible;
	
	public virtual void UpdateLayout(float y, float totalWidth, float[] columnWidths) { }
	
	public void Indent(int level)
	{
		if (!DebugInfo.Config.showGroupIndentMargin || !ShowIndentMargin)
			return;
		
		if (!hasIndentMargin)
		{
			AssetReferences.Create(DebugInfo.Assets.indentMarginPrefab, out indentMargin, "IndentMargin", IndentMarginContainer.transform);
			hasIndentMargin = true;
		}
		
		if (indentLevel == 0)
		{
			indentMargin.gameObject.SetActive(true);
		}
		
		indentLevel = level;
		IndentMarginContainer.UpdateIndent(indentLevel * DebugInfo.Config.groupIndent, indentMargin);
	}
	
	public void ClearIndent()
	{
		if (indentLevel == 0)
			return;
		if (!DebugInfo.Config.showGroupIndentMargin || !ShowIndentMargin)
			return;
		
		if (hasIndentMargin)
		{
			indentMargin.gameObject.SetActive(false);
		}
		
		indentLevel = 0;
		IndentMarginContainer.UpdateIndent(0, indentMargin);
	}
	
	protected virtual bool ShowIndentMargin => false;
	
	protected virtual Cell IndentMarginContainer => null;
	
}

}
