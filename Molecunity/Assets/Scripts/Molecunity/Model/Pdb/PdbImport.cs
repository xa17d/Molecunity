using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Molecunity.Model.Pdb
{
	public class PdbImport {

		private static int molCounter = 1;
		public void UserSelectFile() {
			string molName = "mol" + (molCounter++).ToString ();
			string filename = EditorUtility.OpenFilePanel ("PDB File", "", "pdb");
			
			PdbParser p = PdbParser.FromFile (filename);
			Molecule m = p.Parse();
			Create (m);
		}

		public bool DownloadFile(string url, string name) {
			WWW www = new WWW(url);
			
			while( !www.isDone ) {
				EditorUtility.DisplayProgressBar ("Download", "Downloading...", www.progress);
			}
			
			EditorUtility.ClearProgressBar ();
			
			if (!string.IsNullOrEmpty(www.error)) {
				
				EditorUtility.DisplayDialog("Error", www.error, "Close");
				return false;
				
			} else {

				PdbParser p = PdbParser.FromString(url, www.text, name);
				Molecule m = p.Parse();
				Create(m);

				return true;
			}
		}

		private void Create(Molecule m) {

			string molName = m.Name;
			Debug.Log ("Imported: " + m.Atoms.Length + "atoms / " + m.Bonds.Length + "bonds");
			
			GameObject mol = new GameObject(molName);
			Debug.Log ("About to add atoms...");
			
			int i = 0;
			foreach (Atom atom in m.Atoms) {
				Debug.Log("Atom "+(i++)+"/"+m.Atoms.Length);
				AddAtom(
					atom,
					mol.transform
					);
			}

			MUE mue = MUE.GetInstance ();
			MoleculeSpecies species = mue.CreateMoleculeSpecies ();
			species.Name = molName;
			mue.AddSpecies (species);

			mol.AddComponent<Molecunity.Molecule>();
			Molecunity.Molecule script = mol.GetComponent<Molecunity.Molecule> ();
			script.Species = species;
			
			Debug.Log ("About to create Prefab...");
			
			Object prefab = PrefabUtility .CreateEmptyPrefab("Assets/Molecules/"+molName+".prefab");
			PrefabUtility.ReplacePrefab(mol, prefab);
			AssetDatabase.Refresh();
			
			Debug.Log ("done");
		}
		
		static void AddAtom(Atom atom, Transform transform)
		{
			var sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			sphere.transform.parent = transform;
			
			float scale = atom.Element.Radius * 4;
			sphere.transform.localScale = new Vector3 (scale, scale, scale);
			sphere.transform.position = new Vector3 (atom.X, atom.Y, atom.Z);
			Material m = Resources.Load<Material> ("Atoms/Material" + atom.Element.Symbol);
			if (m != null)
			{ sphere.renderer.material = m; }
		}
	}
}