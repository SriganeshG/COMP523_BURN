using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateScoreCount : MonoBehaviour {
	private string currentScore;
	public Text scoreText;

	void Start () {
		currentScore = scoreText.text;
		scoreText.text = "Score: " + score.ToString ();


	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Vector3 position = this.transform.position;
			position.x--;
			this.transform.position = position;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			Vector3 position = this.transform.position;
			position.x++;
			this.transform.position = position;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			Vector3 position = this.transform.position;
			position.y++;
			this.transform.position = position;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			Vector3 position = this.transform.position;
			position.y--;
			this.transform.position = position;
		}






	}
	void OnCollisionEnter(Collision col) {
		if(col.gameObject.name == "JupiterEnding") {
			System.Console.WriteLine ("plus10");

		}




	}
}
