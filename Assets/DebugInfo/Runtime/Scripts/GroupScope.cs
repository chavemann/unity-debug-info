using System;
using C.Debugging.Rows;

namespace C.Debugging
{

public readonly struct GroupScope : IDisposable
{
	
	private readonly DebugInfoTable table;
	private readonly GroupHeadingRow previousGroup;
	
	public GroupScope(DebugInfoTable table, GroupHeadingRow group)
	{
		this.table = table;
		previousGroup = table.currentGroup;
		table.currentGroup = group;
	}
	
	public void Dispose()
	{
		table.currentGroup = previousGroup;
	}
	
}

}
