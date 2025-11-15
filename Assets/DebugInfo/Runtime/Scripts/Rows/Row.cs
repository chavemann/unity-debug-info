using C.Debugging.Cells;
using UnityEngine;

namespace C.Debugging.Rows
{

public abstract class Row
{
	
	public DebugInfoTable Table { get; private set; }
	public GroupHeadingRow Group { get; internal set; }
	
	public Vector2 Size { get; protected set; }
	
	protected bool visible = true;
	
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
	
	public abstract void OnRemoved();
	
	public virtual void SetVisible(bool visible)
	{
		this.visible = visible;
	}
	
	public virtual void UpdateLayout(float y, float totalWidth, float[] columnWidths) { }
	
}

}
