using System;
using C.Debugging.Rows;

namespace C.Debugging
{

/// <summary>
/// A disposable scope used to group logs. Do not use this directly, instead create groups with <see cref="DebugInfo.Group"/>.
/// </summary>
/// <exclude/>
public readonly struct GroupScope : IDisposable
{
	
	private readonly DebugInfoTable table;
	private readonly GroupHeadingRow previousGroup;
	
	/// <exclude/>
	internal GroupScope(DebugInfoTable table, GroupHeadingRow group)
	{
		this.table = table;
		previousGroup = table.CurrentGroup;
		table.CurrentGroup = group;
	}
	
	/// <exclude/>
	public void Dispose()
	{
		table.CurrentGroup = previousGroup;
	}
	
}

}
