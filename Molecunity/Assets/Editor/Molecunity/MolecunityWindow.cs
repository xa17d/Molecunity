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
		
		if (mue == null) {
			mue = MUE.GetInstance ();
		}
	}
	
	[SerializeField]
	private MUE mue;
	
	public Vector2 scrollPosition = Vector2.zero;
	
	private void Space() {
		GUILayout.Space (10);
	}

	private MoleculeSpeciesListView listViewMolecules = new MoleculeSpeciesListView ();
	private ReactionTypeListView listViewReactions = new ReactionTypeListView();

	void OnGUI () {
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
		GUILayout.Button ("From PDB File");
		GUILayout.Button ("Download From PDB");
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

		if (GUILayout.Button ("Load")) {
			MUE loadedMue = AssetDatabase.LoadAssetAtPath("Assets/MUE.asset", typeof(MUE)) as MUE;
			if (loadedMue == null)
				Debug.LogError("MUE not found on disk");
			else {
				mue = loadedMue;
			}
		}
		if (GUILayout.Button ("Reset")) {
			mue.ResetData();
		}
	}

}