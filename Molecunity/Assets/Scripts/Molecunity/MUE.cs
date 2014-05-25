using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Molecunity.Reaction;

namespace Molecunity
{
	/// <summary>
	/// Molecunity Environment
	/// </summary>
	[System.Serializable]
	public class MUE : ScriptableObject {

		private MUE() {
			instance = this;
			Debug.Log ("MUE constructor");
		}

		void OnEnable ()
		{
			//hideFlags = HideFlags.HideInHierarchy;
			Debug.Log ("MUE enabled "+ToString());

			if (species == null) {
				species = new List<MoleculeSpecies>();
			}

			if (reactionTypes == null) {
				reactionTypes = new List<ReactionType>();
			}
		}

		[SerializeField]
		private List<MoleculeSpecies> species;

		[SerializeField]
		public int ID = System.DateTime.Now.Millisecond;

		public MoleculeSpecies[] Species {
			get {
				return species.ToArray();
			}
		}

		public void AddSpecies(MoleculeSpecies s) {
			species.Add (s);

			UnityEditor.AssetDatabase.AddObjectToAsset (s, this);
		}

		public MoleculeSpecies CreateMoleculeSpecies() {
			return ScriptableObject.CreateInstance<MoleculeSpecies> ();
		}

		public void RemoveSpecies(MoleculeSpecies s) {
			s.Delete ();
			species.Remove (s);
			ScriptableObject.DestroyImmediate (s, true);
		}

		[SerializeField]
		private List<ReactionType> reactionTypes;

		public ReactionType[] ReactionTypes {
			get {
				return reactionTypes.ToArray();
			}
		}

		public ReactionType CreateReactionType() {
			return ScriptableObject.CreateInstance<ReactionType> ();
		}

		public void AddReaction(ReactionType r) {
			reactionTypes.Add (r);

			UnityEditor.AssetDatabase.AddObjectToAsset (r, this);
		}
		
		public void RemoveReaction(ReactionType r) {
			reactionTypes.Remove (r);

			ScriptableObject.DestroyImmediate (r, true);
		}

		private static MUE instance;
		public static MUE GetInstance() {
			if (instance == null) {
				Debug.Log ("try load MUE...");

				Object asset = Resources.Load("MUE");
				Debug.Log("loaded: "+(asset == null ? "null" : asset.ToString()));
				MUE loadedMue = asset as MUE;
				if (loadedMue == null)
				{
					Debug.Log ("creating new MUE...");
					MUE mue = ScriptableObject.CreateInstance<MUE>();
					UnityEditor.AssetDatabase.CreateAsset(mue, "Assets/Resources/MUE.asset");
					//UnityEditor.AssetDatabase.AddObjectToAsset(mue, "Assets/Resources/MUE.asset");
					UnityEditor.AssetDatabase.SaveAssets();
				}
			}

			return instance;
		}

		public override string ToString ()
		{
			return GetInstanceID().ToString()+" / ms:"+ID.ToString();
		}

		public void ResetData() {
			reactionTypes.Clear ();
			species.Clear ();
		}
	}
}