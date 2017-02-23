using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorChange : MonoBehaviour {

    public PlayerController player;
    public Texture _no_reversal_tex;
    public Texture _reversal_tex;

	// Use this for initialization
	void Start ( ) {
        player = GameObject.Find( "Player" ).GetComponent< PlayerController >( );
	}
	
	// Update is called once per frame
	void Update ( ) {
        if ( player.isReversal( ) ) {
            GetComponent<Renderer>( ).material.SetTexture( "_MainTex", _reversal_tex );
        } else {
            GetComponent<Renderer>( ).material.SetTexture( "_MainTex", _no_reversal_tex );        
        }
	}
}
