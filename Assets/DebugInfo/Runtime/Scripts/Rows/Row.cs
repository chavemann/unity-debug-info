using C.Debugging.Cells;
using UnityEngine;

namespace C.Debugging.Rows
{

public abstract class Row
{
	
	public DebugInfoTable Table { get; private set; }
	public int Index { get; internal set; }
	public GroupHeadingRow Group { get; internal set; }
	public bool Visible { get; internal set; }
	
	public Vector2 Size { get; protected set; }
	
	private IndentMargin indentMargin;
	private bool hasIndentMargin;
	
	internal int indentLevel;
	private int currentIndentLevel;
	
	private bool currentVisible;
	
	protected void CreateCell<T>(Cell prefab, string name, out T cell) where T : Cell
	{
		AssetReferences.Create(prefab, out cell, name, DebugInfo.PoolContainer);
		cell.row = this;
	}
	
	public virtual float ColumnCount => 0;
	
	public virtual float ColumnWidth(int index) => 0;
	
	internal virtual void OnAdded(DebugInfoTable table)
	{
		Table = table;
		Group = null;
	}
	
	internal virtual void OnRemoved() { }
	
	internal void PrepareLayout()
	{
		PrepareIndent();
		PrepareVisible();
	}
	
	protected virtual void UpdateVisible() {  }
	
	internal virtual void UpdateLayout(float y, float totalWidth, float[] columnWidths) { }
	
	private void PrepareIndent()
	{
		if (!ShowIndentMargin)
			return;
		if (indentLevel == currentIndentLevel)
			return;
		
		if (DebugInfo.Config.showGroupIndentMargin)
		{
			if (!hasIndentMargin)
			{
				AssetReferences.Create(DebugInfo.Assets.indentMarginPrefab, out indentMargin, "IndentMargin", IndentMarginContainer.transform);
				hasIndentMargin = true;
			}
			
			if (indentLevel == 0)
			{
				indentMargin.gameObject.SetActive(true);
			}
		}
		
		currentIndentLevel = indentLevel;
		IndentMarginContainer.UpdateIndent(indentLevel * DebugInfo.Config.groupIndent, indentMargin);
	}
	
	private void PrepareVisible()
	{
		if (Visible == currentVisible)
			return;
		
		currentVisible = Visible;
		UpdateVisible();
	}
	
	internal void ClearGroup()
	{
		Group = null;
		indentLevel = 0;
		Visible = true;
	}
	
	protected virtual bool ShowIndentMargin => false;
	
	protected virtual Cell IndentMarginContainer => null;
	
}

}
