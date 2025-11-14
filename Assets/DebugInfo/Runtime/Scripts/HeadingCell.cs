namespace C.Debugging
{

public class HeadingCell : Cell
{
	
	protected override void InitialiseProperties()
	{
		base.InitialiseProperties();
		
		padding = DebugInfo.Config.headingTextPadding;
	}
	
}

}
