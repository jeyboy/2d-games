using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chess
{
    public class Cell : MonoBehaviour
    {
        public Image mOutlineImage;

        public Vector2Int mBoardPosition = Vector2Int.zero;
        public Board mBoard = null;
        public RectTransform mRectTransform = null;
        public BasePiece mCurrentPiece = null;

        public void Setup(Vector2Int newBoardPosition, Board newBoard)
        {
            mBoardPosition = newBoardPosition;
            mBoard = newBoard;

            mRectTransform = GetComponent<RectTransform>();
        }

        public void RemovePiece() {
            if (mCurrentPiece != null) {
                mCurrentPiece.Kill();
            }
        }
    }
}
