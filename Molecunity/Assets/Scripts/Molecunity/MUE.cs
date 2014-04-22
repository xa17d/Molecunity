using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Molecunity Environment
/// </summary>
public class MUE : ScriptableObject {

	void OnEnable ()
	{
		hideFlags = HideFlags.HideAndDontSave;
		Debug.Log ("MUE enabled");
		if (instance == null) {
			instance = this;
		}
	}

	[SerializeField]
	private List<MoleculeSpecies> species = new List<MoleculeSpecies>();
	public MoleculeSpecies[] Species {
		get {
			return species.ToArray();
		}
	}

	public string TEST;

	public void AddSpecies(MoleculeSpecies s) {
		species.Add (s);
	}

	public void RemoveSpecies(MoleculeSpecies s) {
		species.Remove (s);
	}

	private static MUE instance;
	public static MUE GetInstance() {
		if (instance == null) {
			Debug.Log ("create MUE on my own");
			instance = CreateInstance<MUE>();
		}

		return instance;
	}
}
