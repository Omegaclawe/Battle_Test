using UnityEngine;
using System.Collections;
//using BattleRules;
//using BattleStats;

public class BattleGUI : MonoBehaviour {

	public BattleRules daRules;
	public BattleStats Attacker;
	public BattleStats Defender;
	private Rect bounds;
	private Rect AttackSpriteBounds;
	private Rect DefendSpriteBounds;
	private Rect AttackHPBounds;
	private Rect DefenseHPBounds;

	// Use this for initialization
	public void ready () {
		Debug.Log ("GUI Ready");
		bounds = new Rect(daRules.screenBuffer,daRules.screenBuffer,Screen.width-daRules.screenBuffer*2,Screen.height-daRules.screenBuffer*2);
		AttackSpriteBounds = new Rect (bounds.xMin + (bounds.xMax - bounds.xMin) * daRules.attackHorPos - Attacker.Sprite.width / 2, bounds.yMin + (bounds.yMax - bounds.yMin) * daRules.spriteVertPos - Attacker.Sprite.height / 2, Attacker.Sprite.width, Attacker.Sprite.height);
		DefendSpriteBounds = new Rect (bounds.xMin + (bounds.xMax - bounds.xMin) * daRules.defendHorPos - Defender.Sprite.width / 2, bounds.yMin + (bounds.yMax - bounds.yMin) * daRules.spriteVertPos - Defender.Sprite.height / 2, Defender.Sprite.width, Defender.Sprite.height);
		AttackHPBounds = new Rect (bounds.xMin + (bounds.xMax - bounds.xMin) * daRules.attackhealthHorPos - daRules.healthTxtWidth / 2, bounds.yMin + (bounds.yMax - bounds.yMin) * daRules.healthVertPos - daRules.healthTxtHeight / 2, daRules.healthTxtWidth, daRules.healthTxtHeight);
		DefenseHPBounds = new Rect (bounds.xMin + (bounds.xMax - bounds.xMin) * daRules.defendhealthHorPos - daRules.healthTxtWidth / 2, bounds.yMin + (bounds.yMax - bounds.yMin) * daRules.healthVertPos - daRules.healthTxtHeight / 2, daRules.healthTxtWidth, daRules.healthTxtHeight);
	}
	
	// Update is called once per frame
	void OnGUI() {
		//Draw Battle Background
		GUI.Box(bounds,daRules.background);
		//Draw Character Sprites
		GUI.Box(AttackSpriteBounds, Attacker.Sprite);
		GUI.Box(DefendSpriteBounds, Defender.Sprite);
		//Draw Hitpoints
		GUI.Box(AttackHPBounds,Attacker.health.ToString());
		GUI.Box(DefenseHPBounds,Defender.health.ToString());
	}
		
}
