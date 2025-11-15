namespace C.Debugging.Rows
{

internal class TextRow : BasicRow
{
	
	public TextRow() : base(DebugInfo.Assets.cellPrefab, "TextRow") { }
	
	protected override void ReturnToPool() => RowPool<TextRow>.Release(this);
	
}

}