using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public enum PHASE {
        PHASE_READY,
        PHASE_PLAY,
        PHASE_CLEAR
    }
    public int stage_num;

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
                _player.GetComponent<PlayerController>( ).resetPlayer( );
                break;
            case PHASE.PHASE_PLAY:
                updatePlay( );
                break;
            case PHASE.PHASE_CLEAR:
                updateClear( );
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
            RankingManage rank = GetComponent< RankingManage >( );
            rank.resetRanking( stage_num );
            rank.saveRanking( stage_num, timer.getTime( ) / 60 );
            //Debug.Log( rank.getRank( 0 ) );
        }

    }

    private void updateClear( ) {
        if ( _player.GetComponent<Rigidbody>( ).velocity == Vector3.zero ) {
            string result_name = "Result";
            int result_num = stage_num + 1;
            result_name += result_num;
            SceneManager.LoadScene( result_name );
        }
    }

    public PHASE getPhase( ) {
        return _phase;
    }
}
