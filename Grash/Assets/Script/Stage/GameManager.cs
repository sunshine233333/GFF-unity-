using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public enum PHASE {
        PHASE_READY,
        PHASE_PLAY,
        PHASE_CLEAR
    }

    private const int SPRITE_MAX = 4;
    public Sprite[] CountSprite = new Sprite[ SPRITE_MAX ];

    private float _count_time = SPRITE_MAX;
    private PHASE _phase;

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
        _phase = PHASE.PHASE_READY;
	}
	
	// Update is called once per frame
	void Update ( ) {
        switch ( _phase ) {
            case PHASE.PHASE_READY:
                ReadyCount( );
                break;
            case PHASE.PHASE_PLAY:
                updatePlay( );
                break;
            case PHASE.PHASE_CLEAR:
                break;
        }
	}

    private void ReadyCount( ) {
        _count_time -= Time.deltaTime;
        _ready_sprite.sprite = CountSprite[ ( int )_count_time ];
        if ( _count_time <= 0.0f ) {
            _phase = PHASE.PHASE_PLAY;
            Timer timer = _time.GetComponent<Timer>( );
            timer.setGameStart( );
            _ready_sprite.gameObject.active = false;
        }
    }

    private void updatePlay( ) {
        float player_x = _player.transform.position.x;
        float goal_x = _goal.transform.position.x;

        if ( player_x >= goal_x ) {
            _phase = PHASE.PHASE_CLEAR;
            Timer timer = _time.GetComponent<Timer>( );
            timer.setGameEnd( );
        }

    }

    private void updateClear( ) {
        Vector3 player_velocioty = _player.GetComponent<Rigidbody>( ).velocity;
        if ( player_velocioty != Vector3.zero ) {
            return;
        }
    }

    public PHASE getPhase( ) {
        return _phase;
    }
}
