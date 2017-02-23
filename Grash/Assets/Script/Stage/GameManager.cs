using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public enum STATE {
        STATE_READY,
        STATE_PLAY,
        STATE_CLEAR
    }

    private const int SPRITE_MAX = 4;
    public Sprite[] CountSprite = new Sprite[ SPRITE_MAX ];

    private float _count_time = SPRITE_MAX;
    private STATE _state;

    private GameObject _goal;
    private GameObject _player;
    private GameObject _time;

    private SpriteRenderer _ready_sprite;

    private void Awake( ) {
        _goal = GameObject.Find( "Goal" );
        _player = GameObject.Find( "Player" );
        _time = GameObject.Find( "Time" );

        _ready_sprite = GameObject.Find( "ReadyCount" ).GetComponent<SpriteRenderer>( );
    }
    // Use this for initialization
    void Start ( ) {
        _state = STATE.STATE_READY;
	}
	
	// Update is called once per frame
	void Update ( ) {
        switch ( _state ) {
            case STATE.STATE_READY:
                ReadyCount( );
                break;
        }
	}

    private void ReadyCount( ) {
        _count_time -= Time.deltaTime;
        _ready_sprite.sprite = CountSprite[ ( int )_count_time ];
        if ( _count_time <= 0.0f ) {
            _state = STATE.STATE_PLAY;
            Timer timer = _time.GetComponent<Timer>( );
            timer.setGameStart( );
            _ready_sprite.gameObject.active = false;
        }
    }

    public STATE getState( ) {
        return _state;
    }
}
