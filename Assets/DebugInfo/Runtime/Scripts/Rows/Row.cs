using UnityEngine;

namespace C.Debugging.Rows
{

internal abstract class Row
{
	
	protected DebugInfoTable table;
	
	public Vector2 Size { get; protected set; }
	
	public virtual float ColumnWidth(int index) => 0;
	
	public virtual void Activate(DebugInfoTable table)
	{
		this.table = table;
	}
	
	public abstract void Deactivate();
	
	public virtual void UpdateLayout(float y, float totalWidth, float[] columnWidths) { }
	
}

}
