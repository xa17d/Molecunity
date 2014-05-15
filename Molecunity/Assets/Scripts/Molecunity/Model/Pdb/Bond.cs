using UnityEngine;
using System.Collections;

namespace Molecunity.Model.Pdb
{
	public class Bond
	{
		public Bond(Atom atom, Atom bondedAtom)
		{
			this.Atom = atom;
			this.BondedAtom = bondedAtom;
		}
		
		public Atom Atom { get; private set; }
		public Atom BondedAtom { get; private set; }
		
		public override string ToString()
		{
			return Atom + " -- " + BondedAtom;
		}
	}
}