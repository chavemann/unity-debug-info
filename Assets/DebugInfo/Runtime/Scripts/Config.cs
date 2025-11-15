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
	
	public UpdateMode updateMode = UpdateMode.Update;
	
	/// <summary>
	/// The spacing between the table and the screen.
	/// </summary>
	[Tooltip("The spacing between the table and the screen.")]
	public Vector2 margin = new(4, 4);
	
	/// <summary>
	/// The default text alignment for all labels.
	/// </summary>
	[Tooltip("The default text alignment for all labels.")]
	public TextAnchor labelAlign = TextAnchor.UpperLeft;
	
	/// <summary>
	/// The default text colour for all labels.
	/// </summary>
	[Tooltip("The default text colour for all labels.")]
	public Color textColor = Color.white;
	
	/// <summary>
	/// The default cell background colour for all labels.
	/// </summary>
	[Tooltip("The default cell background colour for all labels.")]
	public Color backgroundColor = new(0, 0, 0, 0.25f);
	
	/// <summary>
	/// The padding inside a cell around the text.
	/// </summary>
	[Tooltip("The padding inside a cell around the text.")]
	public Vector2 textPadding = new(4, 2);
	
	/// <summary>
	/// The padding inside a heading cell around the text.
	/// </summary>
	[Tooltip("The padding inside a heading cell around the text.")]
	public Vector2 headingTextPadding = new(4, 2);
	
	/// <summary>
	/// The spacing between columns and rows.
	/// </summary>
	[Tooltip("The spacing between columns and rows.")]
	public Vector2 cellSpacing = new(1, 1);
	
	/// <summary>
	/// The default height for spacers when a size isn't explicitly set.
	/// </summary>
	[Tooltip("The default height for spacers when a size isn't explicitly set.")]
	public float defaultSpacerSize = 4;
	
	/// <summary>
	/// The size of the indent inside of groups.
	/// </summary>
	[Tooltip("The size of the indent inside of groups.")]
	public float groupIndent = 12;
	
	/// <summary>
	/// If true shows a border/margin on the left side of rows inside of groups.
	/// </summary>
	[Tooltip("If true shows a border/margin on the left side of rows inside of groups.")]
	public bool showGroupIndentMargin = true;
	
}

}
