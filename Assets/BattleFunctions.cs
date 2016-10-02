using UnityEngine;
using System.Collections;
//using BattleStats;
//using BattleRules;
//using BattleGUI;

public class BattleFunctions : MonoBehaviour {

	public enum attackResults {
		MISS,
		WEAK,
		HIT,
		CRIT
	}
	static public attackResults Attack(BattleStats attacker, BattleStats defender, BattleRules daRules) {
		//Consolidate Values
		int attackerSpeed = (int) attacker.speed + attacker.weaponSpeed;
		int defenderSpeed = (int) defender.speed + defender.weaponSpeed;
		int attack = (int) attacker.attack + attacker.weaponAttack;

		bool advantage = false;
		bool disadvantage = false;

		//Determine Hit Chance:
		int hitChance = (int)( attacker.weaponAccuracy + attacker.skill * 2 - defender.evade * 2 + (attackerSpeed - defenderSpeed) / 2);
		if (TriangleAdvantage (attacker, defender)) {
			hitChance += daRules.Triangle_Hit_Bonus;
			advantage = true;
		} else if (TriangleAdvantage (attacker, defender)) {
			hitChance -= daRules.Triangle_Hit_Deduction;
			disadvantage = true;
		}
		hitChance = (hitChance < 0) ? 0 : ((hitChance > 100) ? 100 : hitChance); //clamp value between 1 and 100
		int hitRoll = Random.Range(0,100);
		if (hitRoll > hitChance) {
			return attackResults.MISS; // The attack has missed
		}
		//Determine Crit Chance:
		int critChance = (int)( attacker.weaponCrit + attacker.skill);
		bool crit = false;
		hitRoll = Random.Range(0,100);
		if (hitRoll <= critChance) {
			crit = true;
		}
		//Determine damage
		int damage = (int) ( (attack - defender.defense) * (crit ? daRules.Crit_Multiplier : 1.0f) * (advantage ? daRules.Triangle_Damage_Multiplier : 1.0f) * (disadvantage ? daRules.Triangle_Damage_Reverse_Multiplier : 1.0f));
		Debug.Log (damage);
		if (damage<= 0) {
			return attackResults.WEAK; //Attack to weak to cause damage
		}
		defender.health -= damage;
		return crit ? attackResults.CRIT : attackResults.HIT;
	}

	static public bool TriangleAdvantage(BattleStats attacker, BattleStats defender) {
		if(attacker.weaponType == BattleStats.WeaponTypes.PAPER && defender.weaponType == BattleStats.WeaponTypes.ROCK) {
			return true;
		}
		if(attacker.weaponType == BattleStats.WeaponTypes.ROCK && defender.weaponType == BattleStats.WeaponTypes.SCISORS) {
			return true;
		}
		if(attacker.weaponType == BattleStats.WeaponTypes.SCISORS && defender.weaponType == BattleStats.WeaponTypes.PAPER) {
			return true;
		}
		return false; //for all others, no advantage
	}

	private IEnumerator doBattle(BattleStats attacker, BattleStats defender, BattleRules daRules, bool oneway = false) {
		Debug.Log ("StartBattle");
		//Start GUI
		BattleGUI gui = gameObject.AddComponent<BattleGUI>();
		FloatyText ftxt;
		gui.daRules = daRules;
		gui.Attacker = attacker;
		gui.Defender = defender;
		gui.ready();

		//Deterministic RNG
		if(daRules.DeterministicRNG) {
			Random.InitState (daRules.RNG_Seed);
		}
		int attackerSpeed = (int)  attacker.speed + attacker.weaponSpeed;
		int defenderSpeed = (int)  defender.speed + defender.weaponSpeed;

		attackResults result;
		//Attacker Attacks
		result = Attack(attacker, defender, daRules);
		Debug.Log (result);
		//Do something with result
		ftxt = gameObject.AddComponent<FloatyText>();
		ftxt.text = result.ToString ();
		ftxt.daRules = daRules;
		ftxt.attack = true;
		ftxt.ready ();
		yield return new WaitForSeconds(1.5f);

		if (defender.health <= 0) {
			//Aw he dead. Handle death here.
			Debug.Log ("EndBattle1");
			yield break;
		}
		if (!oneway) { //Oneway attacks for out of range or something
			result = Attack (defender, attacker, daRules);
			Debug.Log (result);
			//Do something with result
			ftxt = gameObject.AddComponent<FloatyText>();
			ftxt.text = result.ToString ();
			ftxt.daRules = daRules;
			ftxt.attack = false;
			ftxt.ready ();
			yield return new WaitForSeconds(1.5f);

			if (attacker.health <= 0) {
				//Aw he dead. Handle death here.
				Debug.Log ("EndBattle2");
				yield break;
			}
		}
		//Check for Double Attack
		if(daRules.Speed_Difference_For_Double_Attack > Mathf.Abs(attackerSpeed - defenderSpeed)) {
			//No doubles
			Debug.Log ("EndBattle6");
			yield break;
		}
		if(attackerSpeed > defenderSpeed) {
			//Attacker gets a double
			result = Attack(attacker, defender, daRules);
			//Do something with result
			ftxt = gameObject.AddComponent<FloatyText>();
			ftxt.text = result.ToString ();
			ftxt.daRules = daRules;
			ftxt.attack = true;
			ftxt.ready ();
			Debug.Log (result);
			yield return new WaitForSeconds(1.5f);

			if (defender.health <= 0) {
				//Aw he dead. Handle death here.
				Debug.Log ("EndBattle3");
				yield break;
			}
		} else {
			//Defender gets Double
			if (!oneway) {
				result = Attack (defender, attacker, daRules);
				//Do something with result
				Debug.Log (result);
				ftxt = gameObject.AddComponent<FloatyText>();
				ftxt.text = result.ToString ();
				ftxt.daRules = daRules;
				ftxt.attack = false;
				ftxt.ready ();
				yield return new WaitForSeconds(1.5f);

				if (attacker.health <= 0) {
					//Aw he dead. Handle death here.
					Debug.Log ("EndBattle4");
					yield break;
				}
			}
		}
		Debug.Log ("EndBattle5");
	}

	public void Battle(BattleStats attacker, BattleStats defender, BattleRules daRules, bool oneway = false) {
		StartCoroutine(doBattle(attacker,defender,daRules,oneway));
	}
}