using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    enum STATE {
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

    private SpriteRenderer _ready_sprite;

    private void Awake( ) {
        _goal = GameObject.Find( "Goal" );
        _player = GameObject.Find( "Player" );

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
    }

}
