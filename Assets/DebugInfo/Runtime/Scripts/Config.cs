using System;
using UnityEngine;

namespace C.Debugging
{

/// <summary>
/// Note that these settings should be changed before logging anything to properly take effect.
/// </summary>
[Serializable]
public class Config
{
	
	/// <summary>
	/// The spacing between the table and the screen.
	/// </summary>
	[Tooltip("The spacing between the table and the screen.")]
	public Vector2 margin;
	
	/// <summary>
	/// The default text colour for all labels.
	/// </summary>
	[Tooltip("The default text colour for all labels.")]
	public Color textColor = Color.white;
	
	/// <summary>
	/// The default cell background colour for all labels.
	/// </summary>
	[Tooltip("The default cell background colour for all labels.")]
	public Color backgroundColor = new Color(0, 0, 0, 0.25f);
	
	/// <summary>
	/// The padding inside a cell around the text.
	/// </summary>
	[Tooltip("The padding inside a cell around the text.")]
	public Vector2 textPadding = new(4, 2);
	
	/// <summary>
	/// The spacing between columns and rows.
	/// </summary>
	[Tooltip("The spacing between columns and rows.")]
	public Vector2 cellSpacing = new(1, 1);
	
}

}
