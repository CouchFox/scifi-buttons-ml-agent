using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBoardAgent : Agent {
	public Text posTxt;
	public Text negTxt;
	public Text stepTxt;
	public GameObject board1;
	public GameObject board2;
	public Camera cam;
	public GameObject turn1;
	public GameObject turn2;
	public GameObject helper;
	public GameObject active;
	public GameObject target;

	ScifiButtonsAcademy academy;
	GameManager gameManager;
	bool busy = false;
	Game game;
	GameObject[] buttons;
	Vector3 board1CamPos = new Vector3(-8.58f,22.1f,-8.2f);
	Vector3 board2CamPos = new Vector3(-6.29f,22.1f,-6.27f);

	//float tmpTick = 0;
	int solved = 0;
	int lost = 0;
	float unitSize;

	public void Start () {
		posTxt.text = solved.ToString ();
		negTxt.text = lost.ToString ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		academy = GameObject.Find ("Academy").GetComponent<ScifiButtonsAcademy> ();
		unitSize = (float)gameManager.unitSize;
		game = GameObject.Find ("GameManager").GetComponent<Game> ();
		buttons = new GameObject[]{ turn1, helper, active, turn2, target };

	}

	public override List<float> CollectState () {
		List<float> list = new List<float> ();
		foreach (GameObject button in buttons) {
			list.Add (button.transform.position.x /unitSize );
			list.Add (button.transform.position.z / unitSize);
			list.Add (button.transform.localScale.y < 1.0f ? 0.0f :1.0f);
			list.Add (button.GetComponent<Button>().isTarget ? 1.0f : 0.0f);
			list.Add (button.GetComponent<Button>().isStatic ? 1.0f : 0.0f);
			list.Add (button.gameObject.activeSelf ? 1.0f : 0.0f);
		} 
		return list;
	}

	public override void AgentStep (float[] action) {

		int actionNumber = (int)action [0];
		if (actionNumber != 0) {
			if (!busy) {
				busy = true;

				var button = gameManager.GetButtonById (actionNumber);
				if (button != null) {
					if (button.isPressed ()) { //negative reward if already pressed buttons are pressed
						reward -= 0.02f;
					}
					button.Action ();
				}
			}
		} else {
			//negative reward to avoid long waiting times
			reward -= 0.01f;
		}
		//negative reward in relation to distance between active and target
		reward -= (target.transform.position - active.transform.position).sqrMagnitude/100f;

		//Reward if helper button and active button are not too far apart 
		if ((helper.transform.position - active.transform.position).sqrMagnitude <= unitSize*unitSize) {
			reward += 0.03f;
		}

		if (!Input.anyKey) {
			busy = false;
		}

		if (gameManager.CheckForWin ()) {
			reward = 1f;
			solved += 1;
			done = true;
			return;
		}

		if (buttons [1].GetComponent<Button> ().isPressed () && active.GetComponent<Button> ().isPressed ()) {
			reward = -1f;
			lost += 1;
			done = true;
			return;
		}

		if (stepCounter >= maxStep-1) {
			reward = -1f;
			lost += 1;
			done = true;
			return;
		}

		posTxt.text = solved.ToString ();
		negTxt.text = lost.ToString ();
		if (stepTxt.gameObject.activeSelf) {
			stepTxt.text = "Steps: "+stepCounter.ToString ();
		}

	}

	public override void AgentReset () {
		//Debug.Log ("AgentReset");
		Level activeLevel = game.levels [academy.lessonNr - 1];
		board1.SetActive (activeLevel.boardNumber==0);
		board2.SetActive (activeLevel.boardNumber!=0);
		turn2.SetActive (activeLevel.boardNumber != 0);
		if (activeLevel.boardNumber == 0) {
			cam.transform.position = board1CamPos;
			cam.orthographicSize = 6;
		} else {
			cam.transform.position = board2CamPos;
			cam.orthographicSize = 8;
		}

		int randBoardPosNum = Random.Range (0, activeLevel.boardPositions.Count);
		foreach (GameObject button in buttons) {
			button.transform.localScale = Vector3.one;
		}
		BoardPosition boardPos = activeLevel.boardPositions[randBoardPosNum];
		active.transform.position = boardPos.activePos;
		helper.transform.position = boardPos.helperPos;
		target.transform.position = boardPos.targetPos;

	}




}
