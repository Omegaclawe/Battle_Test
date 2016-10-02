using UnityEngine;
using System.Collections;


public class BattleStats : MonoBehaviour {
	public enum WeaponTypes {
		NEUTRAL = 0,
		ROCK = 1,
		PAPER = 2,
		SCISORS = 3
	};


	public string gemName = "Shadow The Hedgegem";
	[Range(1,255)]
	public int health = 10;
	[Range(1,255)]
	public uint speed = 10;
	[Range(1,255)]
	public uint skill = 10;
	[Range(1,255)]
	public uint attack = 10;
	[Range(1,255)]
	public uint defense = 10;
	[Range(1,255)]
	public uint evade = 10;

	[Range(-255,255)]
	public int weaponAttack = 3;
	[Range(-255,255)]
	public int weaponAccuracy = 70;
	[Range(-255,255)]
	public int weaponSpeed = 3;
	[Range(-255,255)]
	public int weaponCrit = 3;
	public WeaponTypes weaponType = WeaponTypes.NEUTRAL;
	public Texture Sprite;
}
