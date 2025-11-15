using System;
using C.Debugging.Formatting;
using C.Debugging.Rows;
using UnityEngine;

namespace C.Debugging.Samples.ExampleUsage
{

public class ExampleUsage : MonoBehaviour
{
	
	[SerializeField]
	private Transform sphere;
	[SerializeField]
	private Rigidbody sphereRigidbody;
	
	[SerializeField]
	private bool showMisc = true;
	[SerializeField]
	private bool groupMisc = true;
	
	private int frame;
	private int collisionCount;
	
	/// <summary>
	/// The behaviour of DebugInfo can be changed using the `Config` property.
	/// Note that most of this options should be changed beforehand in order for the
	/// changes to properly be applied. 
	/// </summary>
	private void Awake()
	{
		// By default, the debug info will automatically be cleared and update every Update.
		// This can be changed to instead be automatically done during FixedUpdate, or can be
		// set to Manual in which case `DebugInfo.UpdateAll` should be clear at the end of each frame.
		DebugInfo.Config.updateMode = UpdateMode.Manual;
	}
	
	private void FixedUpdate()
	{
		DebugInfo.Heading("- This is a Heading -");
		DebugInfo.Log("Time", $"{frame} ({Str.F(Time.fixedTime)})");
		DebugInfo.Spacer();
		
		GroupHeadingRow sphereGroup;
		
		using (DebugInfo.Group("Sphere", Color.aquamarine))
		{
			sphereGroup = DebugInfo.DefaultTable.CurrentGroup;
			
			Vector3 velocity = sphereRigidbody.linearVelocity;
			
			DebugInfo.Log("Position", Str.F(sphere.localPosition), Str.TransformRgb);
			DebugInfo.Log("Velocity", Str.TransformRgb, $"{Str.TransformClr(Str.F(velocity))} {Str.Clr($"({Str.F(velocity.magnitude)})", "#ddbaef")}", null);
			
			if (showMisc)
			{
				using (DebugInfo.TryGroup(groupMisc) ?? DebugInfo.Group("Misc", Color.aquamarine))
				{
					DebugInfo.Log("Direction", velocity.x < 0 ? "Left" : "Right", Str.StateRgb);
					DebugInfo.Log("Collisions", Str.F(collisionCount), Str.CollisionRgb);
				}
			}
			
			DebugInfo.Log("Show Misc", null, Str.F(showMisc), showMisc ? Str.OnRgb : Str.OffRgb);
			DebugInfo.Log("Group Misc", null, Str.F(groupMisc), groupMisc ? Str.OnRgb : Str.OffRgb);
		}
		
		DebugInfo.Log("Collapsed", null, Str.F(sphereGroup.Collapsed), !sphereGroup.Collapsed ? Str.OnRgb : Str.OffRgb);
		
		// For demonstration purposes manually update DebugInfo, but `UpdateMode.FixedUpdate` could have
		// been used instead to allow DebugInfo to handle it automatically.
		if (DebugInfo.Config.updateMode == UpdateMode.Manual)
		{
			DebugInfo.UpdateAll();
		}
		
		frame++;
	}
	
	private void OnCollisionEnter()
	{
		collisionCount++;
	}
	
	private void OnCollisionExit()
	{
		collisionCount--;
	}
	
}

}
