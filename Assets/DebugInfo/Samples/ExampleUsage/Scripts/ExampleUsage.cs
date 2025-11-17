using C.Debugging.Formatting;
using C.Debugging.Rows;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
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
	
	private void Update()
	{
		TestNotifications();
	}
	
	private void TestNotifications()
	{
		// ReSharper disable JoinDeclarationAndInitializer
		bool helloMessage;
		bool testMessage1;
		bool uniqueMessage;
		bool toggleMisc;
		bool toggleGroupMisc;
		// ReSharper restore JoinDeclarationAndInitializer
		
		#if ENABLE_LEGACY_INPUT_MANAGER
		helloMessage = Input.GetKeyDown(KeyCode.Alpha1);
		testMessage1 = Input.GetKeyDown(KeyCode.Alpha2);
		uniqueMessage = Input.GetKeyDown(KeyCode.Alpha3);
		toggleMisc = Input.GetKeyDown(KeyCode.Alpha4);
		toggleGroupMisc = Input.GetKeyDown(KeyCode.Alpha5);
		#elif ENABLE_INPUT_SYSTEM
		helloMessage = Keyboard.current?.digit1Key.wasPressedThisFrame ?? false;
		testMessage1 = Keyboard.current?.digit2Key.wasPressedThisFrame ?? false;
		uniqueMessage = Keyboard.current?.digit3Key.wasPressedThisFrame ?? false;
		toggleMisc = Keyboard.current?.digit4Key.wasPressedThisFrame ?? false;
		toggleGroupMisc = Keyboard.current?.digit5Key.wasPressedThisFrame ?? false;
		#endif
		
		if (helloMessage)
		{
			ShowWelcomeNotification();
		}
		
		if (testMessage1)
		{
			// Each call will create a new notification.
			DebugInfo.Notify($"Test message 1: {Str.F(Time.time)}");
		}
		
		if (uniqueMessage)
		{
			Color randomClr = NiceColour(saturationMin: 0, saturationMax: 0.5f);
			// Pass an id to create a unique notification.
			// Subsequent will instead update the existing notification and reset its timer.
			DebugInfo.Notify($"This message is unique: {Str.Clr(Str.F(Time.time), Str.ToHex(randomClr))}", "UniqueMsg");
		}
		
		if (toggleMisc)
		{
			showMisc = !showMisc;
			// Shows an "On"/"Off" message based on the passed in bool value. These are unique by default.
			DebugInfo.NotifyOn("Show Misc", showMisc, Clr.brown, new Color(0.41f, 0f, 0.09f, 0.5f));
		}
		
		if (toggleGroupMisc)
		{
			groupMisc = !groupMisc;
			// Shows an "Enabled"/"Disabled" message based on the passed in bool value. These are unique by default.
			DebugInfo.NotifyEnabled("Group Misc", groupMisc, Clr.forestGreen, new Color(0f, 0.39f, 0f, 0.5f));
		}
	}
	
	private void TestLogs()
	{
		// `Heading`s display a single column of text and are slightly larger and
		// visually distinct from regular rows.
		DebugInfo.Heading("- This is a Heading -");
		
		// `Str.F` has several overloads for conveniently and consistently formatting a few
		// of the common built-in, and Unity types.
		
		// `Log` two values to show two columns with a key and a value.
		DebugInfo.Log("Time", $"{frame} ({Str.F(Time.fixedTime)})");
		DebugInfo.Spacer();
		
		// Use `Log` with a single parameter to display a single column.
		DebugInfo.Log(
			"Click on the \"Sphere\" group\n" +
			"below to fold/unfold");
		
		GroupHeadingRow sphereGroup;
		
		using (DebugInfo.Group("Sphere", Clr.aquamarine, borderColor: Clr.aquamarine, onCollapsed: OnSphereGroupCollapsed))
		{
			sphereGroup = DebugInfo.DefaultTable.CurrentGroup;
			
			#pragma warning disable CS0618 // Type or member is obsolete
			Vector3 velocity = sphereRigidbody.velocity;
			#pragma warning restore CS0618 // Type or member is obsolete
			
			// The label, value, or color of both can be changed as a whole by passing
			// using the `Color` parameters.
			DebugInfo.Log("Position", Str.F(sphere.localPosition), Clr.TransformRgb);
			// There are also various functions for wrapping individual sections of text
			// in specific colors.
			DebugInfo.Log("Velocity", Str.F(velocity), Clr.TransformRgb);
			DebugInfo.Log(Str.I(Str.Gold("Speed >>")), Str.Clr($"[{Str.F(velocity.magnitude)}]", "#ddbaef"));
			
			// Setting a per-heading border color is also possible.
			DebugInfo.Heading("Nested Heading", new Color(0.66f, 0.7f, 0.92f), borderColor: new Color(0.66f, 0.7f, 0.92f, 0.5f));
			
			if (showMisc)
			{
				// Use `TryGroup` to conditional wrap logs in a group.
				// If condition is false the inner logs are still shown but no group is created.
				// If condition is true `DebugInfo.Group` is executed and the inner logs are
				// added to the group like normal.
				using (DebugInfo.TryGroup(groupMisc) ?? DebugInfo.Group("Misc (Nested Group)", Clr.magenta, borderColor: Clr.magenta))
				{
					DebugInfo.Log("Direction", velocity.x < 0 ? "Left" : "Right", Clr.StateRgb);
					DebugInfo.Log("Collisions", Str.F(collisionCount), Clr.CollisionRgb);
				}
			}
			
			DebugInfo.Log("Show Misc", null, Str.F(showMisc), showMisc ? Clr.OnRgb : Clr.OffRgb);
			DebugInfo.Log("Group Misc", null, Str.F(groupMisc), groupMisc ? Clr.OnRgb : Clr.OffRgb);
		}
		
		DebugInfo.Log("Collapsed", null, Str.F(sphereGroup.Collapsed), !sphereGroup.Collapsed ? Clr.OnRgb : Clr.OffRgb);
		
		DebugInfo.Spacer();
		DebugInfo.Log("Multiline", null, "Line 1\nLine 2", Clr.gray7);
	}
	
	private static void OnSphereGroupCollapsed(GroupHeadingRow group, bool collapsed)
	{
		DebugInfo.Notify("Sphere group " + (collapsed ? Str.OffClr("Closed") : Str.OnClr("Opened")), "SphereGroupState");
	}
	
	private static void ShowWelcomeNotification(float duration = 0)
	{
		DebugInfo.Notify(
			$"This is a {Str.I(Str.Aquamarine("Notification"))}.\n" +
			$"Press {Str.B(Str.Cyan("[1]"))} to display this message again, and the\n" +
			 "other number keys to test other notifications.",
			 "HelloMsg", borderColor: Clr.aquamarine, duration: duration);
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
	
	private void OnCollisionEnter()
	{
		collisionCount++;
	}
	
	private void OnCollisionExit()
	{
		collisionCount--;
	}
	
	private static Color NiceColour(
		float alphaMin = 1f, float alphaMax = 1f,
		float hueMin = 0f, float hueMax = 1f,
		float saturationMin = 0.65f, float saturationMax = 1f,
		float valueMin = 0.65f, float valueMax = 1f)
	{
		return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, alphaMin, alphaMax);
	}
	
}

}
