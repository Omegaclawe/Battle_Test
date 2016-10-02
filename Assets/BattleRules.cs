using UnityEngine;
using System.Collections;

public class BattleRules : MonoBehaviour {

	public bool DeterministicRNG = true;
	public int RNG_Seed = 525600;
	[Range(1,255)]
	public int Speed_Difference_For_Double_Attack = 5;
	public float Crit_Multiplier = 3.0f;
	public int Triangle_Hit_Bonus = 30;
	public float Triangle_Damage_Multiplier = 2.0f;
	public int Triangle_Hit_Deduction = 30;
	public float Triangle_Damage_Reverse_Multiplier = .75f; //Attacking a stronger weapon

	//Positioning for GUI Elements. Sort of a Placeholder
	public float screenBuffer = 50.0f;
	public float spriteVertPos = 0.5f;
	public float attackHorPos = 0.3f;
	public float defendHorPos = 0.7f;
	public float textVertPos = 0.2f;
	public float attacktextHorPos = 0.3f;
	public float defendtextHorPos = 0.7f;
	public float healthVertPos = 0.8f;
	public float attackhealthHorPos = 0.3f;
	public float defendhealthHorPos = 0.7f;
	public float healthTxtWidth = 100.0f;
	public float healthTxtHeight = 10.0f;

	public Texture background;
}
