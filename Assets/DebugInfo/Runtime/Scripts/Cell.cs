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
	private Text textField;
	[SerializeField]
	private RectTransform textFieldTransform;
	
	public Vector2 TextSize { get; private set; }
	public Vector2 Size { get; private set; }
	
	private string currentText;
	
	private void Awake()
	{
		textFieldTransform.localPosition = new Vector3(
			DebugInfo.Config.textPadding.x, -DebugInfo.Config.textPadding.y, 0);
	}
	
	public void AlignRight()
	{
		textField.alignment = TextAnchor.UpperRight;
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
		Size = new Vector2(
			TextSize.x + DebugInfo.Config.textPadding.x * 2,
			TextSize.y + DebugInfo.Config.textPadding.y * 2
		);
	}
	
	public void UpdateLayout(Vector2 position, Vector2 size)
	{
		transform.localPosition = position;
		
		textFieldTransform.sizeDelta = size - DebugInfo.Config.textPadding * 2;
		transform.sizeDelta = size;
	}
	
}

}
