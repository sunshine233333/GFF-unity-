using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private float _time;
    private bool _is_start_game = false;
    private bool _is_end_game = false;
    private Text time_text;

	// Use this for initialization
	void Start ( ) {
        _time = 0;
        time_text = GetComponent< Text >( );
	}
	
	// Update is called once per frame
	void Update ( ) {
        if ( _is_start_game ) {
            addTime( );
        }
        writeTime( );
	}
    private void addTime( ) {
        _time++;
    }

    private void writeTime( ) {
        string text = "Time:";
        int sec = ( int ) ( _time / 60 );
        if ( Mathf.Log10( sec ) < 1 ) {
            text += "0";
        }
        text += sec;
        text += ".";
        int mil_sec = (int)( _time % 60 );
        mil_sec *= ( 1000 / 60 );
        mil_sec /= 10;
        text += mil_sec;
        if ( Mathf.Log10( mil_sec ) < 1 ) {
            text += "0";
        }
        time_text.text = text;
    }

    public void setGameStart( ) {
        _is_start_game = true;
    }

    public void setGameEnd( ) {
        _is_start_game = false;
        _is_end_game = true;
    }
}
