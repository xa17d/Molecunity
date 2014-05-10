using UnityEngine;
using System.Collections;
using UnityEditor;

//[CustomEditor(typeof(Molecule))]
public class MoleculeEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		//Molecule t = (Molecule)target;

		//EditorGUILayout.LabelField("MUE", t.mue.ToString());
		//EditorUtility.SetDirty (t);
	}
}