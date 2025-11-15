using System;

namespace C.Debugging
{

[Serializable]
public enum UpdateMode
{
	
	/// <summary>
	/// Updates automatically happen every frame during Unity's Update.
	/// </summary>
	Update,
	
	/// <summary>
	/// Updates automatically happen every frame during Unity's FixedUpdate.
	/// </summary>
	FixedUpdate,
	
	/// <summary>
	/// Updates do not happen automatically and <see cref="DebugInfo.UpdateAll"/> most be called every frame.<br/>
	/// It's important that <see cref="DebugInfo.UpdateAll"/> is called every frame to reset the previous frame's data otherwise
	/// all log calls will continue to accumulate causing a memory leak and performance issues.<br/>
	/// <see cref="DebugInfo.UpdateAll"/> should be called as late as possible after all logging calls for the current frame for info to correctly update.
	/// </summary>
	Manual,
	
}

}
