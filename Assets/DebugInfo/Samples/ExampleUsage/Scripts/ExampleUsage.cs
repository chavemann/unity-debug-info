using System;
using C.Debugging.Formatting;
using C.Debugging.Rows;
using UnityEngine;
#if (ENABLE_INPUT_SYSTEM)
using UnityEngine.InputSystem;
#endif

namespace C.Debugging.Samples.ExampleUsage
{

public class ExampleUsage : MonoBehaviour
{
	
	[SerializeField]
	private Transform sphere;
	[SerializeField]
	private Rigidbody sphereRigidbody;
	
	[SerializeField]
	private float sphereMoveForce = 10;
	
	[SerializeField]
	private bool showMisc = true;
	[SerializeField]
	private bool groupMisc = true;
	
	private int frame;
	private int collisionCount;
	private float direction = 1;
	private Vector3 previousPosition;
	
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
	
	private void Start()
	{
		ShowWelcomeNotification(6);
		
		previousPosition = sphere.position;
	}
	
	private void FixedUpdate()
	{
		MoveSphere();
		
		TestLogs();
		
		// For demonstration purposes manually update DebugInfo, but `UpdateMode.FixedUpdate` could have
		// been used instead to allow DebugInfo to handle it automatically.
		if (DebugInfo.Config.updateMode == UpdateMode.Manual)
		{
			DebugInfo.UpdateAll();
		}
		
		frame++;
	}
	
	private void MoveSphere()
	{
		sphereRigidbody.AddForce(sphereMoveForce * direction, 0, 0, ForceMode.Force);
		
		float delta = sphere.position.x - previousPosition.x;
		if (delta != 0 && Mathf.Sign(delta) != Mathf.Sign(direction))
		{
			direction = -direction;
		}
		
		previousPosition = sphere.position;
	}
	
	private void Update()
	{
		TestNotifications();
	}
	
	private void TestNotifications()
	{
		// ReSharper disable JoinDeclarationAndInitializer
		bool helloMessage;
		bool toggleMisc;
		bool toggleGroupMisc;
		// ReSharper restore JoinDeclarationAndInitializer
		
		#if (ENABLE_LEGACY_INPUT_MANAGER)
		helloMessage = Input.GetKeyDown(KeyCode.Alpha1);
		toggleMisc = Input.GetKeyDown(KeyCode.Alpha2);
		toggleGroupMisc = Input.GetKeyDown(KeyCode.Alpha3);
		#elif (ENABLE_INPUT_SYSTEM)
		helloMessage = Keyboard.current?.digit1Key.wasPressedThisFrame ?? false;
		toggleMisc = Keyboard.current?.digit2Key.wasPressedThisFrame ?? false;
		toggleGroupMisc = Keyboard.current?.digit3Key.wasPressedThisFrame ?? false;
		#endif
		
		if (helloMessage)
		{
			ShowWelcomeNotification();
		}
		if (toggleMisc)
		{
			showMisc = !showMisc;
			DebugInfo.NotifyOn("Show Misc", showMisc, Color.brown, new Color(0.41f, 0f, 0.09f, 0.5f));
		}
		if (toggleGroupMisc)
		{
			groupMisc = !groupMisc;
			DebugInfo.NotifyEnabled("Group Misc", groupMisc, Color.darkGreen, new Color(0f, 0.39f, 0f, 0.5f));
		}
	}
	
	private static void ShowWelcomeNotification(float duration = 0)
	{
		DebugInfo.Notify(
			$"This is a {Str.I(Str.Azure("Notification"))}.\n" +
			 "Press 1 to display this message again, and the\n" +
			 "other number keys to test other notifications.",
			 "HelloMsg", color: Color.aquamarine, duration: duration);
	}
	
	private void TestLogs()
	{
		// `Heading`s display a single column of text and are slightly larger and
		// visually distinct from regular rows.
		DebugInfo.Heading("- This is a Heading -");
		
		// The `Str` class has various methods and fields for easily and conveniently
		// formatting text.
		// `Str.F` has several overloads to handle a few of the common Unity type,
		// And the default float precision can be changed in the config.
		
		// `Log` two values to show two columns with a key and a value.
		DebugInfo.Log("Time", $"{frame} ({Str.F(Time.fixedTime)})");
		DebugInfo.Spacer();
		
		// Use `Log` with a single parameter to display a single column.
		DebugInfo.Log(
			"Click on the \"Sphere\" group\n" +
			"below to fold/unfold");
		
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
			DebugInfo.Log("Velocity", Str.F(velocity), Str.TransformRgb);
			DebugInfo.Log(Str.I(Str.Gold("Speed >>")), Str.Clr($"[{Str.F(velocity.magnitude)}]", "#ddbaef"));
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
