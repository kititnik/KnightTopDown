// From: https://answers.unity.com/questions/801928/46-ui-making-a-button-transparent.html?childToView=851816
//
// @kurtdekker
//
// Touchable invisible non-drawing Graphic (usable with Buttons too):
//
// To make an invisible Button:
//
//	1. make a Button in the normal way
//	2. delete the "Text" GameObject which comes below a Button as standard
//	3. delete the "Image" which comes on a Button as standard
//	4. drop this script on the Button, which lets the Button get touched

using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class InvisibleGraphic : Graphic
{
	public override bool Raycast(Vector2 sp, Camera eventCamera)
	{
		//return base.Raycast(sp, eventCamera);
		return false;
	}

	protected override void OnPopulateMesh(VertexHelper vh)
	{
		// We don't want to draw anything
		vh.Clear();
	}

#if UNITY_EDITOR
	[CustomEditor(typeof(InvisibleGraphic))]
	public class InvisibleGraphicEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			// nothing
		}
	}
#endif
}
