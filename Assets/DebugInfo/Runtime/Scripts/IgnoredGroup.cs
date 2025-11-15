using System;

namespace C.Debugging
{

public readonly struct IgnoredGroup : IDisposable
{
	
	public void Dispose()
	{
		// Noop
	}
	
}

}
