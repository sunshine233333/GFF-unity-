using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private GameObject _goal;
    private GameObject _player;

    private void Awake( ) {
        _goal = GameObject.Find( "Goal" );
        _player = GameObject.Find( "Player" );
    }
    // Use this for initialization
    void Start ( ) {
		
	}
	
	// Update is called once per frame
	void Update ( ) {
		
	}
}
