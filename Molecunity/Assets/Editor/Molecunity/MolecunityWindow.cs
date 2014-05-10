using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MolecunityWindow : EditorWindow
{
	[MenuItem ("Window/Molecunity")]
	static void Init () {
		EditorWindow.GetWindow <MolecunityWindow>();
	}
	
	void OnEnable ()
	{
		hideFlags = HideFlags.HideAndDontSave;
	}
	
	void OnGUI () {
		MueEditor.MueGui (MUE.GetInstance ());
	}

}