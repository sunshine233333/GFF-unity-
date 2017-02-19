using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {

    //切り替え判定
	private bool _is_touch_platform;
	private const double MOVE_BORDER = 1.0;
	private Vector2 _input_down_pos;
    private bool _change_scene;

    //文字の明滅
    private SpriteRenderer _sprite_renderer;

    //scene切り替え
    private SpriteRenderer _fade_mask;
    private int _fade_counter;
    private int FADE_MAX_COUNT = 30;

    void Awake() {
        //プラットフォームのチェック
		//IOS or Android
		if ( Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer ) {
			_is_touch_platform = true;
		} else {
			_is_touch_platform = false;
		}
        GameObject sprite = GameObject.Find( "TouchScreen" );
        _sprite_renderer = sprite.GetComponent< SpriteRenderer >( );
        

        GameObject mask = GameObject.Find( "FadePanel" );
        _fade_mask = mask.GetComponent< SpriteRenderer >( );
        _fade_counter = 0;

        _change_scene = false;
    }
	
	// Update is called once per frame
	void Update () {
        //sceneの切り替え判定
        if ( !_change_scene ) {
		    if ( _is_touch_platform ) {
                checkTouchScene( );
		    } else {
                checkClickScene( );
		    }
            //文字の明滅
            float count = Time.timeSinceLevelLoad;
            _sprite_renderer.color -= new Color( 0.0f, 0.0f, 0.0f, Mathf.Sin( count ) / 100 );
        } else {
            _fade_counter++;
            _fade_mask.color += new Color( 0, 0, 0, 1.0f / FADE_MAX_COUNT );
            if ( _fade_counter > FADE_MAX_COUNT ) {
                SceneManager.LoadScene( "StageSelect" );
            }
        }
	}

    private void checkTouchScene( ) {
        Touch touch = Input.GetTouch( 0 );
		if ( touch.phase == TouchPhase.Began ) {
			_input_down_pos = touch.position;
		}
		if ( touch.phase == TouchPhase.Ended ) {
			double move_length = ( _input_down_pos - touch.position ).magnitude;
			if ( move_length < MOVE_BORDER ) {
				//シーン切り替え
				_change_scene = true;
			}
		}
    }

    private void checkClickScene( ) {
        if ( Input.GetMouseButtonDown( 0 ) ) {
			_input_down_pos = Input.mousePosition;
		}
		if ( Input.GetMouseButtonUp( 0 ) ) {
			double move_length = ( _input_down_pos - ( Vector2 )Input.mousePosition ).magnitude;
			if ( move_length < MOVE_BORDER ) {
				//シーン切り替え
				_change_scene = true;
			}
		}
    }
}
