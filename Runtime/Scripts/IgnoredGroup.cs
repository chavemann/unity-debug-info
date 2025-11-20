using System;

namespace C.Debugging
{

/// <exclude/>
public readonly struct IgnoredGroup : IDisposable
{
	
	public void Dispose()
	{
		// Noop
	}
	
}

}
