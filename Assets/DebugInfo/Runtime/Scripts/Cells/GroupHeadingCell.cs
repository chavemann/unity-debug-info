using C.Debugging.Rows;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace C.Debugging.Cells
{

public class GroupHeadingCell : HeadingCell, IPointerClickHandler
{
	
	private const float IconSpacing = 4;
	
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
		
		textFieldTransform.localPosition = new Vector3(padding.x + foldIconTransform.sizeDelta.x + IconSpacing, -padding.y, 0);
		Vector3 foldIconPosition = foldIconTransform.localPosition;
		foldIconPosition.x = padding.x;
		foldIconTransform.localPosition = foldIconPosition;
	}
	
	public override void Set(string text, Color? color, Color? backgroundColor)
	{
		base.Set(text, color, backgroundColor);
		
		foldIcon.color = color ?? DebugInfo.Config.textColor;
	}
	
	protected override void CalculateSize()
	{
		base.CalculateSize();
		
		Size = new Vector2(
			foldIconTransform.sizeDelta.x + IconSpacing + TextSize.x + padding.x * 2,
			Mathf.Max(foldIconTransform.sizeDelta.y, TextSize.y) + padding.y * 2
		);
	}
	
	public void OnPointerClick(PointerEventData eventData)
	{
		groupHeadingRow.Collapsed = !groupHeadingRow.Collapsed;
	}
	
}

}
