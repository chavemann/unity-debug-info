using C.Debugging.Cells;

namespace C.Debugging.Rows
{

internal class HeadingRow : BasicRow
{
	
	private readonly HeadingCell labelCell;
	
	public HeadingRow() : base(DebugInfo.Assets.headingPrefab, "Heading") { }
	
	protected override void ReturnToPool() => RowPool<HeadingRow>.Release(this);
	
}

}
