using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MoleculeSpeciesListView : ListView<MoleculeSpecies>
{
	protected override void OnItemGui (MoleculeSpecies item)
	{
		if (item == null) {
			base.OnItemGui(item);
			return;
		}

		item.Name = EditorGUILayout.TextField ("Name", item.Name);
		EditorGUILayout.LabelField("ID", item.GetInstanceID().ToString());

		EditorUtility.SetDirty (item);

		if (GUILayout.Button ("remove")) {
			MUE mue = MUE.GetInstance();
			mue.RemoveSpecies(item);

			EditorUtility.SetDirty(mue);
		}
	}
}

public class ReactionTypeListView : ListView<ReactionType>
{
	protected override void OnItemGui (ReactionType item)
	{
		if (item == null) {
			base.OnItemGui(item);
			return;
		}
		
		item.Name = EditorGUILayout.TextField ("Name", item.Name);
		//EditorGUILayout.LabelField("ID", item.GetInstanceID().ToString());

		EditorUtility.SetDirty (item);

		if (GUILayout.Button ("remove")) {
			MUE mue = MUE.GetInstance();
			mue.RemoveReaction(item);

			EditorUtility.SetDirty(mue);
		}
	}
}