using UnityEngine;
using System.Collections;
using UnityEditor;
using Molecunity;

[CustomEditor(typeof(Molecule))]
public class MoleculeEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		Molecule t = (Molecule)target;
		MUE mue = MUE.GetInstance ();

		MoleculeSpecies[] speciesList = mue.Species;
		string[] speciesNames = new string[speciesList.Length];

		int speciesIndex = 0;
		for (int i = 0; i < speciesList.Length; i++) {
			MoleculeSpecies s = speciesList[i];
			speciesNames[i] = s.ToString();

			if (MoleculeSpecies.Equals(t.Species, s)) {
				speciesIndex = i;
			}
		}

		speciesIndex = GUILayout.SelectionGrid (speciesIndex, speciesNames, 1);
		t.Species = speciesList [speciesIndex];
		EditorUtility.SetDirty (t);
	}
}