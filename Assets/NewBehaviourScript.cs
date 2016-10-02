﻿using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	public GameObject Attacker;
	public GameObject Defender;
	public GameObject Rules;

	private BattleFunctions bfunct;

	// Use this for initialization
	void Start () {
		bfunct = GetComponentInParent<BattleFunctions> ();
		bfunct.Battle (Attacker.GetComponent<BattleStats>(), Defender.GetComponent<BattleStats>(), Rules.GetComponent<BattleRules>());
	}

}
