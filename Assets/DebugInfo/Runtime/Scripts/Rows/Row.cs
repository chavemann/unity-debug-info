using UnityEngine;

namespace C.Debugging.Rows
{

internal abstract class Row
{
	
	public static int nextId;
	
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void RuntimeInit()
	{
		nextId = 0;
	}
	
	public int id;
	
	public Vector2 Size { get; protected set; }
	
	public virtual float ColumnWidth(int index) => 0;
	
	// public void SetSpacer(float? space = null)
	// {
	// 	ChangeTypeSpacer();
	// 	
	// 	Size = new Vector2(0, space ?? DebugInfo.Config.defaultSpacerSize);
	// }
	
	public virtual void Activate(DebugInfoTable table) { }
	
	public abstract void Deactivate();
	
	public virtual void UpdateLayout(float y, float[] columnWidths) { }
	
	public override string ToString() => $"{GetType().Name}[{id}]";
	
}

}
