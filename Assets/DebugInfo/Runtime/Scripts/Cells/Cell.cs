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
	protected float indentWidth;
	
	protected Transform Transform => transform;
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
	
	public virtual void Set(string text, Color? color, Color? bgColor)
	{
		textField.color = color ?? DebugInfo.Config.textColor;
		background.color = bgColor ?? DebugInfo.Config.backgroundColor;
		
		if (text == currentText)
			return;
		
		currentText = text;
		textField.text = text;
		
		CalculateSize();
	}
	
	protected virtual void CalculateSize()
	{
		TextSize = new Vector2(textField.preferredWidth, textField.preferredHeight);
		Size = new Vector2(
			indentWidth + TextSize.x + padding.x * 2,
			TextSize.y + padding.y * 2
		);
	}
	
	public void UpdateLayout(Vector2 position, Vector2 size)
	{
		transform.localPosition = position;
		
		Vector2 textSize = GetTextSize(size);
		textFieldTransform.sizeDelta = textSize - padding * 2;
		transform.sizeDelta = size;
	}
	
	public virtual void UpdateIndent(float indent, IndentMargin indentMargin)
	{
		indentWidth = indent;
		
		if (DebugInfo.Config.showGroupIndentMargin && indentMargin)
		{
			indentMargin.transform.anchoredPosition = new Vector3(
				Mathf.Ceil(indentWidth - DebugInfo.Config.groupIndent * 0.5f), 0, 0);
		}
		
		PositionText();
	}
	
	protected virtual void PositionText()
	{
		textFieldTransform.localPosition = new Vector3(padding.x + indentWidth, -padding.y, 0);
	}
	
	protected virtual Vector2 GetTextSize(Vector2 size) => size;
	
}

}
