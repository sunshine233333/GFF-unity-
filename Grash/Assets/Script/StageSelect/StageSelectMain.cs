using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeStage( int StageNum ) {
		switch( StageNum ) {
			case 0:
				SceneManager.LoadScene( "Stage1" );
				break;
			case 1:
				SceneManager.LoadScene( "Stage2" );
				break;
			case 2:
				SceneManager.LoadScene( "Stage3" );
				break;
		}
	}
}
