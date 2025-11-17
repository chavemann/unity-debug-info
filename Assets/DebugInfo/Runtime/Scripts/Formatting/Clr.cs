using UnityEngine;

// ReSharper disable InconsistentNaming

namespace C.Debugging.Formatting
{

public static class Clr
{
	
	public static readonly Color TransformRgb = new(1f, 0.68f, 0.725f);
	public static readonly string TransformHex = Str.ToHex(TransformRgb);
	
	public static readonly Color CollisionRgb = new(1f, 0.83f, 0.7f);
	public static readonly string CollisionHex = Str.ToHex(CollisionRgb);
	
	public static readonly Color StateRgb = new(0.73f, 0.86f, 0.74f);
	public static readonly string StateHex = Str.ToHex(StateRgb);
	
	public static readonly Color OnRgb = new(0.584f, 1, 0.584f, 1);
	public static readonly string OnHex = Str.ToHex(OnRgb);
	
	public static readonly Color OffRgb = new(1, 0.584f, 0.584f, 1);
	public static readonly string OffHex = Str.ToHex(OffRgb);
	
	public static readonly Color aliceBlue = new(0.9411765f, 0.9725491f, 1f, 1f);
	public static readonly Color antiqueWhite = new(0.9803922f, 0.9215687f, 0.8431373f, 1f);
	public static readonly Color aquamarine = new(0.4980392f, 1f, 0.8313726f, 1f);
	public static readonly Color azure = new(0.9411765f, 1f, 1f, 1f);
	public static readonly Color beige = new(0.9607844f, 0.9607844f, 0.8627452f, 1f);
	public static readonly Color bisque = new(1f, 0.8941177f, 0.7686275f, 1f);
	public static readonly Color black = new(0f, 0f, 0f, 1f);
	public static readonly Color blanchedAlmond = new(1f, 0.9215687f, 0.8039216f, 1f);
	public static readonly Color blue = new(0f, 0f, 1f, 1f);
	public static readonly Color blueViolet = new(0.5411765f, 0.1686275f, 0.8862746f, 1f);
	public static readonly Color brown = new(0.6470588f, 0.1647059f, 0.1647059f, 1f);
	public static readonly Color burlywood = new(0.8705883f, 0.7215686f, 0.5294118f, 1f);
	public static readonly Color cadetBlue = new(0.372549f, 0.6196079f, 0.627451f, 1f);
	public static readonly Color chartreuse = new(0.4980392f, 1f, 0f, 1f);
	public static readonly Color chocolate = new(0.8235295f, 0.4117647f, 0.1176471f, 1f);
	public static readonly Color clear = new(0f, 0f, 0f, 0f);
	public static readonly Color coral = new(1f, 0.4980392f, 0.3137255f, 1f);
	public static readonly Color cornflowerBlue = new(0.3921569f, 0.5843138f, 0.9294118f, 1f);
	public static readonly Color cornsilk = new(1f, 0.9725491f, 0.8627452f, 1f);
	public static readonly Color crimson = new(0.8627452f, 0.07843138f, 0.2352941f, 1f);
	public static readonly Color cyan = new(0f, 1f, 1f, 1f);
	public static readonly Color darkBlue = new(0f, 0f, 0.5450981f, 1f);
	public static readonly Color darkCyan = new(0f, 0.5450981f, 0.5450981f, 1f);
	public static readonly Color darkGoldenRod = new(0.7215686f, 0.5254902f, 0.04313726f, 1f);
	public static readonly Color darkGray = new(0.6627451f, 0.6627451f, 0.6627451f, 1f);
	public static readonly Color darkGreen = new(0f, 0.3921569f, 0f, 1f);
	public static readonly Color darkKhaki = new(0.7411765f, 0.7176471f, 0.4196079f, 1f);
	public static readonly Color darkMagenta = new(0.5450981f, 0f, 0.5450981f, 1f);
	public static readonly Color darkOliveGreen = new(0.3333333f, 0.4196079f, 0.1843137f, 1f);
	public static readonly Color darkOrange = new(1f, 0.5490196f, 0f, 1f);
	public static readonly Color darkOrchid = new(0.6f, 0.1960784f, 0.8000001f, 1f);
	public static readonly Color darkRed = new(0.5450981f, 0f, 0f, 1f);
	public static readonly Color darkSalmon = new(0.9137256f, 0.5882353f, 0.4784314f, 1f);
	public static readonly Color darkSeaGreen = new(0.5607843f, 0.7372549f, 0.5607843f, 1f);
	public static readonly Color darkSlateBlue = new(0.282353f, 0.2392157f, 0.5450981f, 1f);
	public static readonly Color darkSlateGray = new(0.1843137f, 0.3098039f, 0.3098039f, 1f);
	public static readonly Color darkTurquoise = new(0f, 0.8078432f, 0.8196079f, 1f);
	public static readonly Color darkViolet = new(0.5803922f, 0f, 0.8274511f, 1f);
	public static readonly Color deepPink = new(1f, 0.07843138f, 0.5764706f, 1f);
	public static readonly Color deepSkyBlue = new(0f, 0.7490196f, 1f, 1f);
	public static readonly Color dimGray = new(0.4117647f, 0.4117647f, 0.4117647f, 1f);
	public static readonly Color dodgerBlue = new(0.1176471f, 0.5647059f, 1f, 1f);
	public static readonly Color firebrick = new(0.6980392f, 0.1333333f, 0.1333333f, 1f);
	public static readonly Color floralWhite = new(1f, 0.9803922f, 0.9411765f, 1f);
	public static readonly Color forestGreen = new(0.1333333f, 0.5450981f, 0.1333333f, 1f);
	public static readonly Color gainsboro = new(0.8627452f, 0.8627452f, 0.8627452f, 1f);
	public static readonly Color ghostWhite = new(0.9725491f, 0.9725491f, 1f, 1f);
	public static readonly Color gold = new(1f, 0.8431373f, 0f, 1f);
	public static readonly Color goldenRod = new(0.854902f, 0.6470588f, 0.1254902f, 1f);
	public static readonly Color gray = new(0.5f, 0.5f, 0.5f, 1f);
	public static readonly Color gray1 = new(0.1f, 0.1f, 0.1f, 1f);
	public static readonly Color gray2 = new(0.2f, 0.2f, 0.2f, 1f);
	public static readonly Color gray3 = new(0.3f, 0.3f, 0.3f, 1f);
	public static readonly Color gray4 = new(0.4f, 0.4f, 0.4f, 1f);
	public static readonly Color gray5 = new(0.5f, 0.5f, 0.5f, 1f);
	public static readonly Color gray6 = new(0.6f, 0.6f, 0.6f, 1f);
	public static readonly Color gray7 = new(0.7f, 0.7f, 0.7f, 1f);
	public static readonly Color gray8 = new(0.8f, 0.8f, 0.8f, 1f);
	public static readonly Color gray9 = new(0.9f, 0.9f, 0.9f, 1f);
	public static readonly Color green = new(0f, 1f, 0f, 1f);
	public static readonly Color greenYellow = new(0.6784314f, 1f, 0.1843137f, 1f);
	public static readonly Color grey = new(0.5f, 0.5f, 0.5f, 1f);
	public static readonly Color honeydew = new(0.9411765f, 1f, 0.9411765f, 1f);
	public static readonly Color hotPink = new(1f, 0.4117647f, 0.7058824f, 1f);
	public static readonly Color indianRed = new(0.8039216f, 0.3607843f, 0.3607843f, 1f);
	public static readonly Color indigo = new(0.2941177f, 0f, 0.509804f, 1f);
	public static readonly Color ivory = new(1f, 1f, 0.9411765f, 1f);
	public static readonly Color khaki = new(0.9411765f, 0.9019608f, 0.5490196f, 1f);
	public static readonly Color lavender = new(0.9019608f, 0.9019608f, 0.9803922f, 1f);
	public static readonly Color lavenderBlush = new(1f, 0.9411765f, 0.9607844f, 1f);
	public static readonly Color lawnGreen = new(0.4862745f, 0.9882354f, 0f, 1f);
	public static readonly Color lemonChiffon = new(1f, 0.9803922f, 0.8039216f, 1f);
	public static readonly Color lightBlue = new(0.6784314f, 0.8470589f, 0.9019608f, 1f);
	public static readonly Color lightCoral = new(0.9411765f, 0.5019608f, 0.5019608f, 1f);
	public static readonly Color lightCyan = new(0.8784314f, 1f, 1f, 1f);
	public static readonly Color lightGoldenRod = new(0.9333334f, 0.8666667f, 0.509804f, 1f);
	public static readonly Color lightGoldenRodYellow = new(0.9803922f, 0.9803922f, 0.8235295f, 1f);
	public static readonly Color lightGray = new(0.8274511f, 0.8274511f, 0.8274511f, 1f);
	public static readonly Color lightGreen = new(0.5647059f, 0.9333334f, 0.5647059f, 1f);
	public static readonly Color lightPink = new(1f, 0.7137255f, 0.7568628f, 1f);
	public static readonly Color lightSalmon = new(1f, 0.627451f, 0.4784314f, 1f);
	public static readonly Color lightSeaGreen = new(0.1254902f, 0.6980392f, 0.6666667f, 1f);
	public static readonly Color lightSkyBlue = new(0.5294118f, 0.8078432f, 0.9803922f, 1f);
	public static readonly Color lightSlateBlue = new(0.5176471f, 0.4392157f, 1f, 1f);
	public static readonly Color lightSlateGray = new(0.4666667f, 0.5333334f, 0.6f, 1f);
	public static readonly Color lightSteelBlue = new(0.6901961f, 0.7686275f, 0.8705883f, 1f);
	public static readonly Color lightYellow = new(1f, 1f, 0.8784314f, 1f);
	public static readonly Color limeGreen = new(0.1960784f, 0.8039216f, 0.1960784f, 1f);
	public static readonly Color linen = new(0.9803922f, 0.9411765f, 0.9019608f, 1f);
	public static readonly Color magenta = new(1f, 0f, 1f, 1f);
	public static readonly Color maroon = new(0.6901961f, 0.1882353f, 0.3764706f, 1f);
	public static readonly Color mediumAquamarine = new(0.4f, 0.8039216f, 0.6666667f, 1f);
	public static readonly Color mediumBlue = new(0f, 0f, 0.8039216f, 1f);
	public static readonly Color mediumOrchid = new(0.7294118f, 0.3333333f, 0.8274511f, 1f);
	public static readonly Color mediumPurple = new(0.5764706f, 0.4392157f, 0.8588236f, 1f);
	public static readonly Color mediumSeaGreen = new(0.2352941f, 0.7019608f, 0.4431373f, 1f);
	public static readonly Color mediumSlateBlue = new(0.482353f, 0.4078432f, 0.9333334f, 1f);
	public static readonly Color mediumSpringGreen = new(0f, 0.9803922f, 0.6039216f, 1f);
	public static readonly Color mediumTurquoise = new(0.282353f, 0.8196079f, 0.8000001f, 1f);
	public static readonly Color mediumVioletRed = new(0.7803922f, 0.08235294f, 0.5215687f, 1f);
	public static readonly Color midnightBlue = new(0.09803922f, 0.09803922f, 0.4392157f, 1f);
	public static readonly Color mintCream = new(0.9607844f, 1f, 0.9803922f, 1f);
	public static readonly Color mistyRose = new(1f, 0.8941177f, 0.882353f, 1f);
	public static readonly Color moccasin = new(1f, 0.8941177f, 0.7098039f, 1f);
	public static readonly Color navajoWhite = new(1f, 0.8705883f, 0.6784314f, 1f);
	public static readonly Color navyBlue = new(0f, 0f, 0.5019608f, 1f);
	public static readonly Color oldLace = new(0.9921569f, 0.9607844f, 0.9019608f, 1f);
	public static readonly Color olive = new(0.5019608f, 0.5019608f, 0f, 1f);
	public static readonly Color oliveDrab = new(0.4196079f, 0.5568628f, 0.1372549f, 1f);
	public static readonly Color orange = new(1f, 0.6470588f, 0f, 1f);
	public static readonly Color orangeRed = new(1f, 0.2705882f, 0f, 1f);
	public static readonly Color orchid = new(0.854902f, 0.4392157f, 0.8392158f, 1f);
	public static readonly Color paleGoldenRod = new(0.9333334f, 0.909804f, 0.6666667f, 1f);
	public static readonly Color paleGreen = new(0.5960785f, 0.9843138f, 0.5960785f, 1f);
	public static readonly Color paleTurquoise = new(0.6862745f, 0.9333334f, 0.9333334f, 1f);
	public static readonly Color paleVioletRed = new(0.8588236f, 0.4392157f, 0.5764706f, 1f);
	public static readonly Color papayaWhip = new(1f, 0.937255f, 0.8352942f, 1f);
	public static readonly Color peachPuff = new(1f, 0.854902f, 0.7254902f, 1f);
	public static readonly Color peru = new(0.8039216f, 0.5215687f, 0.2470588f, 1f);
	public static readonly Color pink = new(1f, 0.7529413f, 0.7960785f, 1f);
	public static readonly Color plum = new(0.8666667f, 0.627451f, 0.8666667f, 1f);
	public static readonly Color powderBlue = new(0.6901961f, 0.8784314f, 0.9019608f, 1f);
	public static readonly Color purple = new(0.627451f, 0.1254902f, 0.9411765f, 1f);
	public static readonly Color rebeccaPurple = new(0.4f, 0.2f, 0.6f, 1f);
	public static readonly Color red = new(1f, 0f, 0f, 1f);
	public static readonly Color rosyBrown = new(0.7372549f, 0.5607843f, 0.5607843f, 1f);
	public static readonly Color royalBlue = new(0.254902f, 0.4117647f, 0.882353f, 1f);
	public static readonly Color saddleBrown = new(0.5450981f, 0.2705882f, 0.07450981f, 1f);
	public static readonly Color salmon = new(0.9803922f, 0.5019608f, 0.4470589f, 1f);
	public static readonly Color sandyBrown = new(0.9568628f, 0.6431373f, 0.3764706f, 1f);
	public static readonly Color seaGreen = new(0.1803922f, 0.5450981f, 0.3411765f, 1f);
	public static readonly Color seashell = new(1f, 0.9607844f, 0.9333334f, 1f);
	public static readonly Color sienna = new(0.627451f, 0.3215686f, 0.1764706f, 1f);
	public static readonly Color silver = new(0.7529413f, 0.7529413f, 0.7529413f, 1f);
	public static readonly Color skyBlue = new(0.5294118f, 0.8078432f, 0.9215687f, 1f);
	public static readonly Color slateBlue = new(0.4156863f, 0.3529412f, 0.8039216f, 1f);
	public static readonly Color slateGray = new(0.4392157f, 0.5019608f, 0.5647059f, 1f);
	public static readonly Color snow = new(1f, 0.9803922f, 0.9803922f, 1f);
	public static readonly Color softBlue = new(0.1882353f, 0.682353f, 0.7490196f, 1f);
	public static readonly Color softGreen = new(0.5490196f, 0.7882354f, 0.1411765f, 1f);
	public static readonly Color softRed = new(0.8627452f, 0.1921569f, 0.1960784f, 1f);
	public static readonly Color softYellow = new(1f, 0.9333334f, 0.5490196f, 1f);
	public static readonly Color springGreen = new(0f, 1f, 0.4980392f, 1f);
	public static readonly Color steelBlue = new(0.2745098f, 0.509804f, 0.7058824f, 1f);
	public static readonly Color tan = new(0.8235295f, 0.7058824f, 0.5490196f, 1f);
	public static readonly Color teal = new(0f, 0.5019608f, 0.5019608f, 1f);
	public static readonly Color thistle = new(0.8470589f, 0.7490196f, 0.8470589f, 1f);
	public static readonly Color tomato = new(1f, 0.3882353f, 0.2784314f, 1f);
	public static readonly Color turquoise = new(0.2509804f, 0.8784314f, 0.8156863f, 1f);
	public static readonly Color violet = new(0.9333334f, 0.509804f, 0.9333334f, 1f);
	public static readonly Color violetRed = new(0.8156863f, 0.1254902f, 0.5647059f, 1f);
	public static readonly Color wheat = new(0.9607844f, 0.8705883f, 0.7019608f, 1f);
	public static readonly Color white = new(1f, 1f, 1f, 1f);
	public static readonly Color whiteSmoke = new(0.9607844f, 0.9607844f, 0.9607844f, 1f);
	public static readonly Color yellow = new(1f, 0.92f, 0.016f, 1f);
	public static readonly Color yellowGreen = new(0.6039216f, 0.8039216f, 0.1960784f, 1f);
	public static readonly Color yellowNice = new(1f, 0.92f, 0.016f, 1f);
	
}

}
