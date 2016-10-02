using UnityEngine;
using System.Collections;

public class FloatyText : MonoBehaviour {

	public string text = "YOU NEED TO SET THIS!";
	public BattleRules daRules;
	public bool attack;

	private int counter=0;
	private Rect drect;

	//void
	public void ready() {
		Rect bounds = new Rect(daRules.screenBuffer,daRules.screenBuffer,Screen.width-daRules.screenBuffer*2,Screen.height-daRules.screenBuffer*2);
		drect = new Rect(bounds.xMin + (bounds.xMax - bounds.xMin) * (attack ? daRules.attackhealthHorPos : daRules.defendhealthHorPos)- daRules.healthTxtWidth / 2, bounds.yMin + (bounds.yMax - bounds.yMin) * daRules.textVertPos - daRules.healthTxtHeight / 2, daRules.healthTxtWidth, daRules.healthTxtHeight);
	}

	// Update is called once per frame
	void OnGUI() {
		GUI.Box (drect, text);
		drect.y -= 0.5f;
		++counter;
		if(counter > 100) {
			Destroy (this);
		}
	}
}
