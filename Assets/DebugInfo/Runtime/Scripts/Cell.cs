using UnityEngine;
using UnityEngine.UI;

namespace C.Debugging
{

public class Cell : MonoBehaviour
{
	
	[SerializeField]
	private new RectTransform transform;
	[SerializeField]
	private RawImage background;
	[SerializeField]
	public Text textField;
	[SerializeField]
	private RectTransform textFieldTransform;
	
	public Vector2 TextSize { get; private set; }
	public Vector2 Size { get; private set; }
	
	private string currentText;
	protected Vector2 padding;
	
	private void Awake()
	{
		InitialiseProperties();
		textFieldTransform.localPosition = new Vector3(padding.x, -padding.y, 0);
	}
	
	protected virtual void InitialiseProperties()
	{
		padding = DebugInfo.Config.textPadding;
	}
	
	public void Set(string text, Color? color, Color? backgroundColor)
	{
		textField.color = color ?? DebugInfo.Config.textColor;
		background.color = backgroundColor ?? DebugInfo.Config.backgroundColor;
		
		if (text == currentText)
			return;
		
		currentText = text;
		textField.text = text;
		
		TextSize = new Vector2(textField.preferredWidth, textField.preferredHeight);
		Size = TextSize + padding * 2;
	}
	
	public void UpdateLayout(Vector2 position, Vector2 size)
	{
		transform.localPosition = position;
		
		textFieldTransform.sizeDelta = size - padding * 2;
		transform.sizeDelta = size;
	}
	
}

}
