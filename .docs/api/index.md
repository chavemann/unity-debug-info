# DebugInfo Overview

---

## Setup and Configuration

After installation, no setup is required to start using `DebugInfo` - it will initialise
itself on game start.

However various global defaults can be accessed via the static
[DebugInfo.Config](xref:C.Debugging.DebugInfo.Config) property.

> [!NOTE]
> It's important any changes to the config happen before using `DebugInfo`
> to ensure that the changes are correctly applied.

---

## Logging

The main logging function is `DebugInfo.Log`.  
There are two basic versions, [one][single-log-ref] that logs a single text message,
and [another][double-log-ref] that logs a key, value pair in two columns.

<div class="example-box">
<img src="/images/logging-basic.png" alt="Basic logging example."/>

```csharp
DebugInfo.Log("A single line of text.");
DebugInfo.Log("Key", "Value");
```
</div>

### Color

It's possible to specify the color of the background; key text, value text, or both.

<div class="example-box">
<img src="/images/logging-colors.png" alt="Color logging example."/>

```csharp
DebugInfo.Log("RedKey", "RedValue", Color.lightCoral);
DebugInfo.Log("GreenKey", Color.lawnGreen, "BlueValue", Color.lightSkyBlue);
```
</div>

### Formatting

Formatting and coloring individual parts of text can be done with the static methods provided
by the [Str][str-ref] class.  
There are some global options for changing the formatting behaviour, such as the floating
point [precision][str-precision-ref] field.

<div class="example-box">
<img src="/images/logging-formatting.png" alt="Str Formatting example."/>

```csharp
DebugInfo.Log("Time", $"{Str.F(Time.fixedTime)} ({Str.Cyan(Str.F(Time.frameCount))})");
DebugInfo.Log("Velocity", Str.F(sphereRigidbody.linearVelocity), Str.TransformRgb);
```
</div>

### Headings

The [Header][heading-ref] and [Spacer][spacer-ref] methods can be used to create headings
and gaps between rows.

<div class="example-box">
<img src="/images/logging-headings.png" alt="Headings and Spacers example."/>

```csharp
DebugInfo.Heading("Heading");
DebugInfo.Log("Key1", "Value1");

// It's possible to set a height per spacer. If omitted the default as set in the configuration is used.
DebugInfo.Spacer(8);

// Changing the background and border color is also possible.
DebugInfo.Heading("Heading2", color: Color.lightCoral,
	bgColor: new Color(0.31f, 0.11f, 0.15f, 0.5f), borderColor: Color.lightCoral);
DebugInfo.Log("Key2", "Value2");
```
</div>

### Groups

Rows can be grouped together into collapsable sections using the [Group][group-ref] method.  
The [Group][group-ref] method returns an [IDisposable](xref:System.IDisposable) that must be used
with a `using` block to correctly open and close the group.  
Groups can be clicked to open and close them, and there are various options such as color, initial state,
and a callback for when the group is opened or closed.


<div class="example-box">
<img src="/images/logging-groups.png" alt="Grouping example."/>

```csharp
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
```
</div>

---

## Notifications

`DebugInfo` also provides methods for showing once-off [notification messages][notify-ref].

<div class="example-box">
<img src="/images/notifications.png" alt="Notifications example."/>

```csharp
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
```
</div>

### Toggle Messages

There are also convenience methods for creating on/off messages.

<div class="example-box">
<img src="/images/notifications-toggles.png" alt="Notification toggles example."/>

```csharp
DebugInfo.NotifyOn("ToggledOn", true);
DebugInfo.NotifyOn("ToggledOff", false);
DebugInfo.NotifyEnabled("Enabled", true);
DebugInfo.NotifyEnabled("Disabled", false);
```
</div>


[str-ref]: xref:C.Debugging.Formatting.Str
[str-precision-ref]: xref:C.Debugging.Formatting.Str.defaultPrecision
[str-f-ref]: xref:C.Debugging.Formatting.Str.F(System.Int32)
[single-log-ref]: xref:C.Debugging.DebugInfo.Log(System.String,System.Nullable{UnityEngine.Color},System.Nullable{UnityEngine.Color})
[double-log-ref]: xref:C.Debugging.DebugInfo.Log(System.String,System.String,System.Nullable{UnityEngine.Color},System.Nullable{UnityEngine.Color})
[heading-ref]: xref:C.Debugging.DebugInfo.Heading(System.String,System.Nullable{UnityEngine.Color},System.Nullable{UnityEngine.Color},System.Nullable{UnityEngine.Color})
[spacer-ref]: xref:C.Debugging.DebugInfo.Spacer(System.Nullable{System.Single})
[group-ref]: xref:C.Debugging.DebugInfo.Group(System.String,System.Nullable{UnityEngine.Color},System.Nullable{UnityEngine.Color},System.Nullable{UnityEngine.Color},System.Nullable{System.Boolean},System.Action{C.Debugging.Rows.GroupHeadingRow,System.Boolean})
[notify-ref]: xref:C.Debugging.DebugInfo.Notify(System.String,System.String,System.Nullable{UnityEngine.Color},System.Nullable{UnityEngine.Color},System.Nullable{UnityEngine.Color},System.Single)
