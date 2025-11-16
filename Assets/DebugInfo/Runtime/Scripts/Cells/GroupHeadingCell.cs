using C.Debugging.Rows;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace C.Debugging.Cells
{

public class GroupHeadingCell : HeadingCell, IPointerClickHandler
{
	
	private const float IconSpacing = 8;
	
	[SerializeField]
	private RectTransform foldIconTransform;
	[SerializeField]
	private Image foldIcon;
	[SerializeField]
	private Sprite foldRightIcon;
	[SerializeField]
	private Sprite foldDownIcon;
	
	internal GroupHeadingRow groupHeadingRow;
	
	public bool Collapsed
	{
		set => foldIcon.sprite = value ? foldRightIcon : foldDownIcon;
	}
	
	protected override void Awake()
	{
		base.Awake();
		
		PositionText();
		PositionFoldIcon();
	}
	
	public override void Set(string text, Color? color, Color? bgColor)
	{
		base.Set(text, color, bgColor);
		
		foldIcon.color = color ?? DebugInfo.Config.textColor;
	}
	
	protected override void CalculateSize()
	{
		base.CalculateSize();
		
		Size = new Vector2(
			indentWidth + foldIconTransform.sizeDelta.x + IconSpacing + TextSize.x + padding.x * 2,
			Mathf.Max(foldIconTransform.sizeDelta.y, TextSize.y) + padding.y * 2
		);
	}
	
	public override void UpdateIndent(float indent, IndentMargin indentMargin)
	{
		base.UpdateIndent(indent, indentMargin);
		
		PositionFoldIcon();
	}
	
	private void PositionFoldIcon()
	{
		Vector3 foldIconPosition = foldIconTransform.localPosition;
		foldIconPosition.x = padding.x + indentWidth;
		foldIconTransform.localPosition = foldIconPosition;
	}
	
	protected override void PositionText()
	{
		textFieldTransform.localPosition = new Vector3(padding.x + indentWidth + foldIconTransform.sizeDelta.x + IconSpacing, -padding.y, 0);
	}
	
	protected override Vector2 GetTextSize(Vector2 size) => new(
		size.x - (foldIconTransform.sizeDelta.x + IconSpacing),
		size.y);
	
	public void OnPointerClick(PointerEventData eventData)
	{
		groupHeadingRow.Collapsed = !groupHeadingRow.Collapsed;
	}
	
}

}
