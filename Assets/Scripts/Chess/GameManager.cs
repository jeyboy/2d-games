using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    public class GameManager : MonoBehaviour
    {
        public Board mBoard;

        public PieceManager mPieceManager;

        // Start is called before the first frame update
        void Start()
        {
            mBoard.Create();

            mPieceManager.Setup(mBoard);
        }

        //// Update is called once per frame
        //void Update()
        //{

        //}
    }
}
