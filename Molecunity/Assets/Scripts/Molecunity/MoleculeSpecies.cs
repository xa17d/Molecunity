using UnityEngine;
using System.Collections;
using Molecunity.Utility;

namespace Molecunity
{
	[System.Serializable]
	public class MoleculeSpecies : ScriptableObject {

		private MoleculeSpecies() { }

		void OnEnable ()
		{
			hideFlags = HideFlags.HideInHierarchy;
		}

		[SerializeField]
		public string Name = "Molecule";

		public override string ToString ()
		{
			return Name;
		}

		
		public override int GetHashCode ()
		{
			return GetInstanceID();
		}
		
		public override bool Equals (object o)
		{
			MoleculeSpecies other;
			if (Utils.TypeEquals<MoleculeSpecies> (this, o, out other)) {
				
				return 
					(GetInstanceID() == other.GetInstanceID()) &&
					(Name == other.Name);
				
			} else { return false; }
		}
	}
}