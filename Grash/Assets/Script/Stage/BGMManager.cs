using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour {

    public enum BGM {
        BGM_MAIN,
        BGM_AFTER_GOAL,
        BGM_MAX,
    };

    public AudioClip[] _audio = new AudioClip[ ( int )BGM.BGM_MAX ];
    private AudioSource _souce;
    private int _before_se;

	void Start () {
        _souce = GetComponent<AudioSource>( );
        _souce.loop = true;
        //playBGM( 0 );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playBGM ( int bgm ) {
        _souce.clip = _audio[ bgm ];
        _souce.Play( );

    }
}
