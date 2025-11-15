using UnityEngine;
using UnityEngine.UI;

namespace C.Debugging.Cells
{

public class HeadingCell : Cell
{
	
	[SerializeField]
	private RawImage border;
	
	protected override void InitialiseProperties()
	{
		base.InitialiseProperties();
		
		padding = DebugInfo.Config.headingTextPadding;
	}
	
	public void Set(string text, Color? color, Color? backgroundColor, Color? borderColor)
	{
		border.color = borderColor ?? DebugInfo.Config.headingBorderColor;
		
		Set(text, color, backgroundColor);
	}
	
}

}
