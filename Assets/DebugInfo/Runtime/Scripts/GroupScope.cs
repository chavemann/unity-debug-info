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
		previousGroup = table.CurrentGroup;
		table.CurrentGroup = group;
	}
	
	public void Dispose()
	{
		table.CurrentGroup = previousGroup;
	}
	
}

}
