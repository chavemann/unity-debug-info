using UnityEngine;
using UnityEngine.UI;

namespace C.Debugging.Cells
{

public class HeadingCell : Cell
{
	
	public RawImage border;
	
	protected override void InitialiseProperties()
	{
		base.InitialiseProperties();
		
		padding = DebugInfo.Config.headingTextPadding;
	}
	
	public void Set(string text, Color? color, Color? bgColor, Color? borderColor, TextAnchor? alignment)
	{
		border.color = borderColor ?? DebugInfo.Config.headingBorderColor;
		
		Set(text, color, bgColor);
		
		if (alignment.HasValue)
		{
			textField.alignment = alignment.Value;
		}
	}
	
}

}
