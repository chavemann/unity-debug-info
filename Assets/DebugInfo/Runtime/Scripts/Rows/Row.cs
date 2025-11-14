using System.Runtime.CompilerServices;
using UnityEngine;

namespace C.Debugging.Rows
{

internal class Row
{
	
	private readonly DebugInfoTable table;
	
	public readonly Cell labelCell;
	public readonly Cell valueCell;
	
	public float Height { get; private set; }
	
	public Row(DebugInfoTable table, int index)
	{
		this.table = table;
		
		AssetReferences.Create(DebugInfo.Assets.cellPrefab, out labelCell, $"Label.{index}", table.Root);
		AssetReferences.Create(DebugInfo.Assets.cellPrefab, out valueCell, $"Value.{index}", table.Root);
		
		labelCell.AlignRight();
		
		Deactivate();
	}
	
	internal void Set(string label, string value, Color? color, Color? backgroundColor)
	{
		labelCell.Set(label, color, backgroundColor);
		valueCell.Set(value, color, backgroundColor);
		
		Height = Mathf.Max(labelCell.Size.y, valueCell.Size.y);
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Activate()
	{
		labelCell.gameObject.SetActive(true);
		valueCell.gameObject.SetActive(true);
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Deactivate()
	{
		labelCell.gameObject.SetActive(false);
		valueCell.gameObject.SetActive(false);
	}
	
	public void UpdateLayout(float y, float labelColumnWidth, float valueColumnWidth)
	{
		labelCell.UpdateLayout(
			new Vector2(0, y),
			new Vector2(labelColumnWidth, Height));
		valueCell.UpdateLayout(
			new Vector2(labelColumnWidth + DebugInfo.Config.cellSpacing.x, y),
			new Vector2(valueColumnWidth, Height));
	}
	
}

}
