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
		// The `Str` class has various methods and fields for easily and conveniently
		// formatting text.
		// `Str.F` has several overloads to handle a few of the common Unity type,
		// And the default float precision can be changed in the config.
		DebugInfo.Log("Time", $"{frame} ({Str.F(Time.fixedTime)})");
		DebugInfo.Spacer();
		
		GroupHeadingRow sphereGroup;
		
		using (DebugInfo.Group("Sphere", Color.aquamarine))
		{
			sphereGroup = DebugInfo.DefaultTable.CurrentGroup;
			
			Vector3 velocity = sphereRigidbody.linearVelocity;
			
			// The label, value, or color of both can be changed as a whole by passing
			// using the `Color` parameters.
			DebugInfo.Log("Position", Str.F(sphere.localPosition), Str.TransformRgb);
			// There are also various functions for wrapping individual sections of text
			// in specific colors.
			DebugInfo.Log(
				$"{Str.TransformClr("Velocity")} {Str.I(Str.AliceBlue("(M)"))}", Str.TransformRgb,
				$"{Str.TransformClr(Str.F(velocity))} {Str.Clr($"({Str.F(velocity.magnitude)})", "#ddbaef")}", null);
			DebugInfo.Heading("Nested Heading");
			
			if (showMisc)
			{
				// Use `TryGroup` to conditional wrap logs in a group.
				// If condition is false the inner logs are still shown but no group is created.
				// If condition is true `DebugInfo.Group` is executed and the inner logs are
				// added to the group like normal.
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
		
		DebugInfo.Spacer();
		DebugInfo.Log("Multiline", null, "Line 1\nLine 2", Color.gray7);
		
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
