using C.Debugging;
using C.Debugging.Formatting;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0162 // Unreachable code detected
// ReSharper disable HeuristicUnreachableCode
// ReSharper disable InvertIf

namespace AssetStore.Scripts
{

public class DocumentationExamples : MonoBehaviour
{
	
	[SerializeField]
	private Rigidbody sphereRigidbody;
	
	private void Start()
	{
		SetupComfortableLayout();
		
		DocumentationNotifications();
	}
	
	private void Update()
	{
		DocumentationLogs();
	}
	
	private void DocumentationLogs()
	{
		/* Basic */
		if (false)
		{
			DebugInfo.Log("A single line of text.");
			DebugInfo.Log("Key", "Value");
		}
		
		/* Color */
		if (false)
		{
			DebugInfo.Log("RedKey", "RedValue", Color.lightCoral);
			DebugInfo.Log("GreenKey", Color.lawnGreen, "BlueValue", Color.lightSkyBlue);
			DebugInfo.Log("RedBackground", bgColor: new Color(0.31f, 0.11f, 0.15f, 0.5f));
		}
		
		/* Formatting */
		if (false)
		{
			DebugInfo.Log("Time", $"{Str.F(Time.fixedTime)} ({Str.Cyan(Str.F(Time.frameCount))})");
			DebugInfo.Log("Velocity", Str.F(sphereRigidbody.linearVelocity), Str.TransformRgb);
		}
		
		/* Headings */
		if (false)
		{
			DebugInfo.Heading("Heading");
			DebugInfo.Log("Key1", "Value1");
			
			// It's possible to set a height per spacer. If omitted the default as set in the configuration is used.
			DebugInfo.Spacer(8);
			
			// Changing the background and border color is also possible.
			DebugInfo.Heading("Heading2", color: Color.lightCoral,
				bgColor: new Color(0.31f, 0.11f, 0.15f, 0.5f), borderColor: Color.lightCoral);
			DebugInfo.Log("Key2", "Value2");
		}
		
		/* Groups */
		if (false)
		{
			using (DebugInfo.Group("My Group", Color.lightSkyBlue)) {
				DebugInfo.Log("A single line of text.");
				using (DebugInfo.Group("Nested Group", Color.lightCoral)) {
					DebugInfo.Log("Nested content");
					using (DebugInfo.Group("Collapsed Group", Color.khaki, collapsed: true)) {
						DebugInfo.Log("Collapsed content");
					}
				}
				DebugInfo.Log("Key", "Value");
			}
		}
	}
	
	private static void DocumentationNotifications()
	{
		/* Basic */
		if (false)
		{
			DebugInfo.Notify($"Lorem {Str.Cyan("ipsum dolor")} sit amet," +
				"\nconsectetur adipiscing elit.");
			DebugInfo.Notify("Lorem ipsum dolor sit amet",
				bgColor: new Color(0.7f, 0.7f, 0.24f, 0.25f),
				borderColor: new Color(0.86f, 0.78f, 0.43f));
			
			// Set the duration to 10 seconds before the notification fades out.
			DebugInfo.Notify($"[!Important] Lorem ipsum dolor sit amet",
				color: Color.lightCoral, duration: 10);
			
			// Giving the notification a unique id will cause subsequent calls to
			// update the existing notification instead of creating new ones.
			DebugInfo.Notify($"CurrentTime: {Str.F(Time.deltaTime)}", "UniqueId",
				color: Color.khaki);
		}
		
		/* Toggle Messages */
		if (false)
		{
			DebugInfo.NotifyOn("ToggledOn", true);
			DebugInfo.NotifyOn("ToggledOff", false);
			DebugInfo.NotifyEnabled("Enabled", true);
			DebugInfo.NotifyEnabled("Disabled", false);
		}
		
	}
	
	private static void SetupComfortableLayout()
	{
		Config cfg = DebugInfo.Config;
		AssetReferences assets = DebugInfo.Assets;
		
		const int FontSize = 18;
		
		cfg.cellSpacing = new Vector2(2, 2);
		cfg.textPadding = new Vector2(8, 4);
		cfg.headingTextPadding = new Vector2(8, 8);
		
		assets.cellPrefab.GetComponentInChildren<Text>().fontSize = FontSize;
		assets.headingPrefab.GetComponentInChildren<Text>().fontSize = FontSize;
		assets.groupHeadingPrefab.GetComponentInChildren<Text>().fontSize = FontSize;
		
		assets.headingPrefab.border.rectTransform.sizeDelta = new Vector2(0, 4);
		
		// Notifications
		assets.notificationPrefab.textfield.fontSize = FontSize;
		assets.notificationPrefab.textWrapper.padding = new RectOffset(8, 8, 4, 4);
		DebugInfo.NotificationList.GetComponent<VerticalLayoutGroup>().spacing = 4;
	}
	
}

}
