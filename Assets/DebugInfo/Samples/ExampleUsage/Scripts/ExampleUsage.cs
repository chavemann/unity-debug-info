namespace C.Debugging.Samples.ExampleUsage
{

public class ExampleUsage : UnityEngine.MonoBehaviour
{
	
	private void Awake()
	{
		DebugInfo.updateMode = UpdateMode.FixedUpdate;
	}
	
}

}
