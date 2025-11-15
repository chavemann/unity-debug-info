using C.Debugging.Rows;
using UnityEngine;
using UnityEngine.UI;

namespace C.Debugging.Cells
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
	protected RectTransform textFieldTransform;
	
	internal Row row;
	
	protected Vector2 TextSize { get; private set; }
	public Vector2 Size { get; protected set; }
	
	private string currentText;
	protected Vector2 padding;
	
	protected virtual void Awake()
	{
		InitialiseProperties();
		textFieldTransform.localPosition = new Vector3(padding.x, -padding.y, 0);
	}
	
	protected virtual void InitialiseProperties()
	{
		padding = DebugInfo.Config.textPadding;
	}
	
	public virtual void Set(string text, Color? color, Color? backgroundColor)
	{
		textField.color = color ?? DebugInfo.Config.textColor;
		background.color = backgroundColor ?? DebugInfo.Config.backgroundColor;
		
		if (text == currentText)
			return;
		
		currentText = text;
		textField.text = text;
		
		CalculateSize();
	}
	
	protected virtual void CalculateSize()
	{
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
