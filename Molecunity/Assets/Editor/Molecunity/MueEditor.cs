using UnityEngine;
using System.Collections;
using UnityEditor;
using Molecunity;
using Molecunity.Model.Pdb;

[CustomEditor(typeof(MUE))]
public class MueEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		MueGui((MUE)target);
	}

	private static MoleculeSpeciesListView listViewMolecules = new MoleculeSpeciesListView ();
	private static ReactionTypeListView listViewReactions = new ReactionTypeListView();

	private static void Space() {
		GUILayout.Space (10);
	}

	public static void MueGui(MUE mue) {

		EditorGUILayout.LabelField ("ID", mue.ID.ToString());
		
		GUILayout.Label ("Molecule Species", EditorStyles.boldLabel);
		
		listViewMolecules.Gui (mue.Species);

		Space ();
		
		GUILayout.Label ("Add Molecule", EditorStyles.miniLabel);
		
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("From Selected")) {
			
			var item = mue.CreateMoleculeSpecies();
			mue.AddSpecies(item);
			
			EditorUtility.SetDirty (mue);
			EditorUtility.SetDirty (item);
			
			listViewMolecules.FoldOpen(item);
		}
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();

		if (GUILayout.Button ("From PDB File")) {
			PdbImport pdbImport = new PdbImport();
			pdbImport.UserSelectFile();
		}

		if (GUILayout.Button ("Download From PDB")) {
			PdbImportWindow.UserDownload();
		}
		GUILayout.EndHorizontal ();
		
		Space ();
		GUILayout.Label ("Reactions", EditorStyles.boldLabel);
		
		listViewReactions.Gui (mue.ReactionTypes);
		
		Space ();
		GUILayout.Label ("Add Reaction", EditorStyles.miniLabel);
		if (GUILayout.Button ("Add Reaction")) {
			var item = mue.CreateReactionType();
			mue.AddReaction (item);
			EditorUtility.SetDirty (mue);
			
			listViewReactions.FoldOpen(item);
		}
		
		Space ();
		GUILayout.Label ("Debug", EditorStyles.boldLabel);
		if (GUILayout.Button ("Save Assets")) {
			AssetDatabase.SaveAssets();
		}
		
		Space ();

		if (GUILayout.Button ("Reset")) {
			mue.ResetData();
		}
	}
}