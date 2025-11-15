using System.Globalization;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

// ReSharper disable UnusedMember.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ConvertToConstant.Global

namespace C.Debugging.Formatting
{

/// <summary>
/// Contains helper methods for consistently formatting debug text.
/// </summary>
[UsedImplicitly]
public static class Str
{
	
	/// <summary>
	/// The default precision when formatting floats.
	/// </summary>
	public static int defaultPrecision = 3;
	
	/// <summary>
	/// The "on" text for <see cref="OnMsg"/>.
	/// </summary>
	public static string onText = "On";
	
	/// <summary>
	/// The "off" text for <see cref="OnMsg"/>.
	/// </summary>
	public static string offText = "Off";
	
	/// <summary>
	/// The "enabled" text for <see cref="OnMsg"/>.
	/// </summary>
	public static string enabledText = "Enabled";
	
	/// <summary>
	/// The "disabled" text for <see cref="OnMsg"/>.
	/// </summary>
	public static string disabledText = "Disabled";
	
	/// <summary>
	/// The prefix when formatting a Unity Color with <see cref="F(Color)"/>.
	/// </summary>
	public static string colorPrefixText = "Rgb";
	
	// public 
	
	#region -- Formatting methods --------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string B<T>(T v) => $"<b>{v}</b>";
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string I<T>(T v) => $"<i>{v}</i>";
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Size<T>(T v, int size) => $"<size={size}>{v}</size>";
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string F<T>(T v) => v.ToString();
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string F(int v) => v.ToString();
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string F(float v) => v.ToString("f" + defaultPrecision, CultureInfo.InvariantCulture);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string F(float v, int precision) => v.ToString("f" + precision, CultureInfo.InvariantCulture);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string F(Vector2 v) => $"<{F(v.x)},{F(v.y)}>";
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string F(Vector3 v) => $"<{F(v.x)},{F(v.y)},{F(v.z)}>";
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string F(Color v) => $"{colorPrefixText}({F(v.r)},{F(v.g)},{F(v.b)},{F(v.a)})";
	
	#endregion -----------------------------------------
	
	#region -- Messages --------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ToggleMsg(string text, bool on, string onText, string offText) => $"{text}: {(on ? OnClr(onText) : OffClr(offText))}";
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string OnMsg(string text, bool on) => ToggleMsg(text, on, onText, offText);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string EnabledMsg(string text, bool on) => ToggleMsg(text, on, enabledText, disabledText);
	
	#endregion -----------------------------------------
	
	#region -- Utility Colors --------------------------------
	
	public static readonly Color TransformRgb = new(1f, 0.68f, 0.72f);
	// public static readonly string TransformHex = ColorUtility.ToHtmlStringRGBA(TransformRgb);
	public static readonly string TransformHex = "#ffadb8ff";
	
	public static readonly Color CollisionRgb = new(1f, 0.83f, 0.7f);
	public static readonly string CollisionHex = ColorUtility.ToHtmlStringRGBA(CollisionRgb);
	
	public static readonly Color StateRgb = new(0.73f, 0.86f, 0.74f);
	public static readonly string StateHex = ColorUtility.ToHtmlStringRGBA(StateRgb);
	
	public static readonly Color OnRgb = new(0.584f, 1, 0.584f, 1);
	public static readonly string OnHex = ColorUtility.ToHtmlStringRGBA(OnRgb);
	
	public static readonly Color OffRgb = new(1, 0.584f, 0.584f, 1);
	public static readonly string OffHex = ColorUtility.ToHtmlStringRGBA(OffRgb);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Clr(string v, string clr) => $"<color={clr}>{v}</color>";
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string OnClr(string v) => Clr(v, OnHex);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string OffClr(string v) => Clr(v, OffHex);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string TransformClr(string v) => Clr(v, TransformHex);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string CollisionClr(string v) => Clr(v, CollisionHex);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string StateClr(string v) => Clr(v, StateHex);
	
	#endregion -----------------------------------------
	
	#region -- Unity Colors --------------------------------
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string AliceBlue(string v) => Clr(v, "#f0f8ff");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string AntiqueWhite(string v) => Clr(v, "#faebd7");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Aquamarine(string v) => Clr(v, "#7fffd4");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Azure(string v) => Clr(v, "#f0ffff");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Beige(string v) => Clr(v, "#f5f5dc");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Bisque(string v) => Clr(v, "#ffe4c4");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Black(string v) => Clr(v, "#000000");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string BlanchedAlmond(string v) => Clr(v, "#ffebcd");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Blue(string v) => Clr(v, "#0000ff");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string BlueViolet(string v) => Clr(v, "#8a2be2");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Brown(string v) => Clr(v, "#a52a2a");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Burlywood(string v) => Clr(v, "#deb887");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string CadetBlue(string v) => Clr(v, "#5f9ea0");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Chartreuse(string v) => Clr(v, "#7fff00");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Chocolate(string v) => Clr(v, "#d2691e");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Clear(string v) => Clr(v, "#000000");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Coral(string v) => Clr(v, "#ff7f50");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string CornflowerBlue(string v) => Clr(v, "#6495ed");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Cornsilk(string v) => Clr(v, "#fff8dc");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Crimson(string v) => Clr(v, "#dc143c");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Cyan(string v) => Clr(v, "#00ffff");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DrkBlue(string v) => Clr(v, "#00008b");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DrkCyan(string v) => Clr(v, "#008b8b");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DrkGoldenRod(string v) => Clr(v, "#b8860b");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DrkGray(string v) => Clr(v, "#a9a9a9");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DrkGreen(string v) => Clr(v, "#006400");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DrkKhaki(string v) => Clr(v, "#bdb76b");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DrkMagenta(string v) => Clr(v, "#8b008b");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DrkOliveGreen(string v) => Clr(v, "#556b2f");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DrkOrange(string v) => Clr(v, "#ff8c00");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DrkOrchid(string v) => Clr(v, "#9932cc");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DrkRed(string v) => Clr(v, "#8b0000");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DrkSalmon(string v) => Clr(v, "#e9967a");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DrkSeaGreen(string v) => Clr(v, "#8fbc8f");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DrkSlateBlue(string v) => Clr(v, "#483d8b");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DrkSlateGray(string v) => Clr(v, "#2f4f4f");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DrkTurquoise(string v) => Clr(v, "#00ced1");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DrkViolet(string v) => Clr(v, "#9400d3");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DeepPink(string v) => Clr(v, "#ff1493");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DeepSkyBlue(string v) => Clr(v, "#00bfff");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DimGray(string v) => Clr(v, "#696969");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string DodgerBlue(string v) => Clr(v, "#1e90ff");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Firebrick(string v) => Clr(v, "#b22222");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string FloralWhite(string v) => Clr(v, "#fffaf0");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string ForestGreen(string v) => Clr(v, "#228b22");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Gainsboro(string v) => Clr(v, "#dcdcdc");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string GhostWhite(string v) => Clr(v, "#f8f8ff");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Gold(string v) => Clr(v, "#ffd700");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string GoldenRod(string v) => Clr(v, "#daa520");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Gray(string v) => Gray5(v);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Grey(string v) => Gray5(v);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Gray1(string v) => Clr(v, "#1a1a1a");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Gray2(string v) => Clr(v, "#333333");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Gray3(string v) => Clr(v, "#4c4c4c");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Gray4(string v) => Clr(v, "#666666");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Gray5(string v) => Clr(v, "#808080");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Gray6(string v) => Clr(v, "#999999");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Gray7(string v) => Clr(v, "#b2b2b2");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Gray8(string v) => Clr(v, "#cccccc");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Gray9(string v) => Clr(v, "#e6e6e6");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Green(string v) => Clr(v, "#00ff00");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string GreenYellow(string v) => Clr(v, "#adff2f");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Honeydew(string v) => Clr(v, "#f0fff0");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string HotPink(string v) => Clr(v, "#ff69b4");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string IndianRed(string v) => Clr(v, "#cd5c5c");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Indigo(string v) => Clr(v, "#4b0082");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Ivory(string v) => Clr(v, "#fffff0");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Khaki(string v) => Clr(v, "#f0e68c");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Lavender(string v) => Clr(v, "#e6e6fa");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LavenderBlush(string v) => Clr(v, "#fff0f5");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LawnGreen(string v) => Clr(v, "#7cfc00");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LemonChiffon(string v) => Clr(v, "#fffacd");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LiteBlue(string v) => Clr(v, "#add8e6");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LiteCoral(string v) => Clr(v, "#f08080");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LiteCyan(string v) => Clr(v, "#e0ffff");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LiteGoldenRod(string v) => Clr(v, "#eedd82");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LiteGoldenRodYellow(string v) => Clr(v, "#fafad2");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LiteGray(string v) => Clr(v, "#d3d3d3");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LiteGreen(string v) => Clr(v, "#90ee90");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LitePink(string v) => Clr(v, "#ffb6c1");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LiteSalmon(string v) => Clr(v, "#ffa07a");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LiteSeaGreen(string v) => Clr(v, "#20b2aa");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LiteSkyBlue(string v) => Clr(v, "#87cefa");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LiteSlateBlue(string v) => Clr(v, "#8470ff");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LiteSlateGray(string v) => Clr(v, "#778899");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LiteSteelBlue(string v) => Clr(v, "#b0c4de");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LiteYellow(string v) => Clr(v, "#ffffe0");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string LimeGreen(string v) => Clr(v, "#32cd32");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Linen(string v) => Clr(v, "#faf0e6");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Magenta(string v) => Clr(v, "#ff00ff");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Maroon(string v) => Clr(v, "#b03060");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string MediumAquamarine(string v) => Clr(v, "#66cdaa");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string MediumBlue(string v) => Clr(v, "#0000cd");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string MediumOrchid(string v) => Clr(v, "#ba55d3");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string MediumPurple(string v) => Clr(v, "#9370db");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string MediumSeaGreen(string v) => Clr(v, "#3cb371");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string MediumSlateBlue(string v) => Clr(v, "#7b68ee");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string MediumSpringGreen(string v) => Clr(v, "#00fa9a");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string MediumTurquoise(string v) => Clr(v, "#48d1cc");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string MediumVioletRed(string v) => Clr(v, "#c71585");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string MidnightBlue(string v) => Clr(v, "#191970");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string MintCream(string v) => Clr(v, "#f5fffa");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string MistyRose(string v) => Clr(v, "#ffe4e1");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Moccasin(string v) => Clr(v, "#ffe4b5");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string NavajoWhite(string v) => Clr(v, "#ffdead");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string NavyBlue(string v) => Clr(v, "#000080");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string OldLace(string v) => Clr(v, "#fdf5e6");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Olive(string v) => Clr(v, "#808000");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string OliveDrab(string v) => Clr(v, "#6b8e23");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Orange(string v) => Clr(v, "#ffa500");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string OrangeRed(string v) => Clr(v, "#ff4500");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Orchid(string v) => Clr(v, "#da70d6");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string PaleGoldenRod(string v) => Clr(v, "#eee8aa");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string PaleGreen(string v) => Clr(v, "#98fb98");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string PaleTurquoise(string v) => Clr(v, "#afeeee");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string PaleVioletRed(string v) => Clr(v, "#db7093");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string PapayaWhip(string v) => Clr(v, "#ffefd5");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string PeachPuff(string v) => Clr(v, "#ffdab9");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Peru(string v) => Clr(v, "#cd853f");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Pink(string v) => Clr(v, "#ffc0cb");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Plum(string v) => Clr(v, "#dda0dd");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string PowderBlue(string v) => Clr(v, "#b0e0e6");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Purple(string v) => Clr(v, "#a020f0");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string RebeccaPurple(string v) => Clr(v, "#663399");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Red(string v) => Clr(v, "#ff0000");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string RosyBrown(string v) => Clr(v, "#bc8f8f");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string RoyalBlue(string v) => Clr(v, "#4169e1");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string SaddleBrown(string v) => Clr(v, "#8b4513");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Salmon(string v) => Clr(v, "#fa8072");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string SandyBrown(string v) => Clr(v, "#f4a460");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string SeaGreen(string v) => Clr(v, "#2e8b57");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Seashell(string v) => Clr(v, "#fff5ee");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Sienna(string v) => Clr(v, "#a0522d");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Silver(string v) => Clr(v, "#c0c0c0");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string SkyBlue(string v) => Clr(v, "#87ceeb");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string SlateBlue(string v) => Clr(v, "#6a5acd");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string SlateGray(string v) => Clr(v, "#708090");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Snow(string v) => Clr(v, "#fffafa");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string SoftRed(string v) => Clr(v, "#dc3132");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string SoftBlue(string v) => Clr(v, "#30aebf");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string SoftGreen(string v) => Clr(v, "#8cc924");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string SoftYellow(string v) => Clr(v, "#ffee8c");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string SpringGreen(string v) => Clr(v, "#00ff7f");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string SteelBlue(string v) => Clr(v, "#4682b4");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Tan(string v) => Clr(v, "#d2b48c");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Teal(string v) => Clr(v, "#008080");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Thistle(string v) => Clr(v, "#d8bfd8");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Tomato(string v) => Clr(v, "#ff6347");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Turquoise(string v) => Clr(v, "#40e0d0");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Violet(string v) => Clr(v, "#ee82ee");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string VioletRed(string v) => Clr(v, "#d02090");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Wheat(string v) => Clr(v, "#f5deb3");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string White(string v) => Clr(v, "#ffffff");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string WhiteSmoke(string v) => Clr(v, "#f5f5f5");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string Yellow(string v) => Clr(v, "#ffeb04");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string YellowGreen(string v) => Clr(v, "#9acd32");
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string YellowNice(string v) => Clr(v, "#ffeb04");
	
	#endregion -----------------------------------------
	
}

}
