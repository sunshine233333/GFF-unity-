using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour {

    public enum SE { 
        SE_GRAVITY,
        SE_TRUBO,
        SE_MAX,
    };

    public AudioClip[ ] _audio = new AudioClip[ ( int )SE.SE_MAX ];
    private AudioSource _souce;
    private int _before_se;


	// Use this for initialization
	void Start () {
        _souce = gameObject.GetComponent<AudioSource>( );
        _before_se = (int)SE.SE_MAX;
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void playSE( int se ) {
        if ( se == _before_se && _souce.isPlaying ) {
            return;
        }
        _souce.clip = _audio[ se ];
        _souce.Play( );
        _before_se = se;
    }
}
