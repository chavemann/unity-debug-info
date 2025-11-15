using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace C.Debugging.Notifications
{

public class Notification : MonoBehaviour
{
	
	[SerializeField]
	private new RectTransform transform;
	[SerializeField]
	private CanvasGroup canvasGroup;
	[SerializeField]
	private Text textfield;
	[SerializeField]
	public RawImage background;
	[SerializeField]
	public RawImage border;
	
	internal string id;
	internal float time;
	
	private State state = State.FadingIn;
	private float fadeTime;
	
	internal void Set(string message, Color? borderColor, Color? bgColor, Color? textColor)
	{
		textfield.text = message;
		time = DebugInfo.Config.notificationTime;
		
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
		canvasGroup.alpha = alpha;
	}
	
	private enum State
	{
		
		FadingIn,
		Visible,
		FadingOut,
		
	}
	
}

}
