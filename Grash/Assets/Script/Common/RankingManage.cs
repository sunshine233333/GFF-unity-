using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManage : MonoBehaviour {

    private const int MAX_RANK_NUM = 3;
    private const int MAX_STAGE_NUM = 3;

    private float[ ] _rank;

    private string[  ] RANK_KEY = new string[ MAX_STAGE_NUM ] { "ranking1", "ranking2", "ranking3" };

	// Use this for initialization
	void Awake ( ) {
        _rank = new float[ MAX_RANK_NUM ];
        for ( int i = 0; i < MAX_RANK_NUM; i++ ) {
            _rank[ i ] = 99;
        }
	}
	
	// Update is called once per frame
	void Update ( ) {
	    
    }

   public void resetRanking( int stage ) {
        string ranking = PlayerPrefs.GetString( RANK_KEY[ stage ] );
        if ( ranking.Length > 0 ) {
            var _score = ranking.Split( "," [ 0 ] );
            for ( int i = 0; i < _score.Length && i < MAX_RANK_NUM; i++ ) {
                _rank[ i ] = float.Parse( _score[ i ] );
            }
        }
    }

    public void saveRanking( int stage, float rank ) { 
        float _tmp = 0.0f;
        for ( int i = 0 ; i < MAX_RANK_NUM; i++ ) {
            if ( _rank[ i ] > rank ) {
                _tmp = _rank[ i ];
                _rank[ i ] = rank;
                rank = _tmp;
            }
        }
        string[] string_rank = new string[MAX_RANK_NUM];
        for ( int i = 0; i < MAX_RANK_NUM; i++ ) {
            string_rank[ i ] = string.Format( "{0}", _rank[ i ] );
        }
        // 配列を文字列に変換して PlayerPrefs に格納
        string ranking_string = string.Join( ",", string_rank );
        PlayerPrefs.SetString( RANK_KEY[ stage ], ranking_string );
        PlayerPrefs.Save( );
    }

    public float getRank( int num ) {
        return _rank[ num ];
    }
}
