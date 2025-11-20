using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace C.Debugging.Notifications
{

public class Notification : MonoBehaviour, IPointerClickHandler
{
	
	[SerializeField]
	private new RectTransform transform;
	[SerializeField]
	private CanvasGroup canvasGroup;
	[SerializeField]
	private HorizontalLayoutGroup layout;
	[SerializeField]
	public HorizontalLayoutGroup textWrapper;
	[SerializeField]
	public Text textfield;
	[SerializeField]
	public RawImage background;
	[SerializeField]
	public RawImage border;
	
	internal string id;
	internal float time;
	
	private State state = State.FadingIn;
	private float fadeTime;
	
	internal void Set(string message, string id, Color? borderColor, Color? bgColor, Color? textColor, float duration)
	{
		this.id = id;
		
		textfield.text = message;
		time = duration > 0 ? duration : DebugInfo.Config.notificationTime;
		
		Color borderClr = borderColor ?? DebugInfo.Config.defaultNotificationBorderColor;
		
		border.color = borderClr;
		background.color = bgColor ?? DebugInfo.Config.defaultNotificationColor;
		textfield.color = textColor ?? DebugInfo.Config.textColor;
		
		border.gameObject.SetActive(borderClr.a > 0);
		
		transform.SetAsLastSibling();
	}
	
	private void StartFade(State fadeState)
	{
		fadeTime = DebugInfo.Config.notificationFadeTime;
		
		if (fadeTime > 0)
		{
			state = fadeState;
		}
		else if (fadeState == State.FadingIn)
		{
			state = State.Visible;
			SetAlpha(1);
		}
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void StartFadeIn() => StartFade(State.FadingIn);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void StartFadeOut() => StartFade(State.FadingOut);
	
	internal bool Tick()
	{
		switch (state)
		{
			case State.FadingIn:
				fadeTime -= Time.unscaledDeltaTime;
				
				if (fadeTime < 0)
				{
					SetAlpha(1);
					state = State.Visible;
				}
				else
				{
					SetAlpha(1 - fadeTime / DebugInfo.Config.notificationFadeTime);
				}
				break;
			case State.Visible:
				time -= Time.unscaledDeltaTime;
				
				if (time < 0)
				{
					StartFadeOut();
				}
				break;
			case State.FadingOut:
				fadeTime -= Time.unscaledDeltaTime;
				SetAlpha(fadeTime / DebugInfo.Config.notificationFadeTime);
				
				return fadeTime > 0;
			default:
				throw new ArgumentOutOfRangeException();
		}
		
		return true;
	}
	
	private void SetAlpha(float alpha)
	{
		layout.padding = new RectOffset(0, (int) (-DebugInfo.Config.notificationSlideDistance * (1 - alpha)), 0, 0);
		canvasGroup.alpha = alpha;
	}
	
	private enum State
	{
		
		FadingIn,
		Visible,
		FadingOut,
		
	}
	
	public void OnPointerClick(PointerEventData eventData)
	{
		if (!DebugInfo.Config.closeNotificationOnClick)
			return;
		
		StartFadeOut();
	}
	
}

}
