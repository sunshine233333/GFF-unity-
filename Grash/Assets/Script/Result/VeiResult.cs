using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VeiResult : MonoBehaviour {

    public int StageNum;
    private Text _text;
    private RankingManage _rank;

	// Use this for initialization
	void Start ( ) {
        _text = GetComponent<Text>( );
        _rank = GameObject.Find("GameManager").GetComponent<RankingManage>( );
        if ( StageNum <= 2 ) {
            _rank.resetRanking( StageNum );
        }
	}
	
	// Update is called once per frame
	void Update () {
        float first = _rank.getRank( 0 );
        float second = _rank.getRank( 1 );
        float third = _rank.getRank( 2 );

        _text.text = first + "\n" + second + "\n" + third;
	}
}
