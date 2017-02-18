using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

	private bool isTouchPlatform;
	private const double MOVE_BORDER = 1.0;
	private Vector2 input_down_pos;

	// Use this for initialization
	void Start () {
		//プラットフォームのチェック
		//IOS or Android
		if ( Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer ) {
			isTouchPlatform = true;
		} else {
			isTouchPlatform = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if ( isTouchPlatform ) {
			Touch touch = Input.GetTouch( 0 );
			if ( touch.phase == TouchPhase.Began ) {
				input_down_pos = touch.position;
			}
			if ( touch.phase == TouchPhase.Ended ) {
				double move_length = ( input_down_pos - touch.position ).magnitude;
				if ( move_length < MOVE_BORDER ) {
					//シーン切り替え
					SceneManager.LoadScene( "StageSelect" );
				}
			}
		} else {
			if ( Input.GetMouseButtonDown( 0 ) ) {
				input_down_pos = Input.mousePosition;
			}
			if ( Input.GetMouseButtonUp( 0 ) ) {
				double move_length = ( input_down_pos - ( Vector2 )Input.mousePosition ).magnitude;
				if ( move_length < MOVE_BORDER ) {
					//シーン切り替え
					SceneManager.LoadScene( "StageSelect" );
				}
			}
		}

	}
}
