//using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chess {
    public class PieceManager : MonoBehaviour {
        public GameObject mPiecePrefab;
        [HideInInspector] public bool mIsKingAlive = true;

        private List<BasePiece> mWhitePieces = null;
        private List<BasePiece> mBlackPieces = null;
        //private List<BasePiece> mRedPieces = null;
        private List<BasePiece> mPromotedPieces = new List<BasePiece>();

        private string[] mPieceOrder = new string[16] {
            "P", "P", "P", "P", "P", "P", "P", "P",
            "R", "KN", "B", "Q", "K", "B", "KN", "R"
        };

        private Dictionary<string, Type> mPieceLibrary = new Dictionary<string, Type>() {
            {"P", typeof(Pawn)},
            {"R", typeof(Rook)},
            {"KN", typeof(Knight)},
            {"B", typeof(Bishop)},
            {"K", typeof(King)},
            {"Q", typeof(Queen)},
        };

        public void Setup(Board board) {
            mWhitePieces = CreatePieces(Color.white, new Color32(80, 124, 159, 255));
            mBlackPieces = CreatePieces(Color.black, new Color32(210, 95, 64, 255));

            PlacePieces(1, 0, mWhitePieces, board);
            PlacePieces(6, 7, mBlackPieces, board);

            SwitchSides(Color.black);
        }

        private List<BasePiece> CreatePieces(Color teamColor, Color32 spriteColor)
        {
            List<BasePiece> newPieces = new List<BasePiece>();

            for (int i = 0; i < mPieceOrder.Length; i++)
            {
                string key = mPieceOrder[i];
                Type pieceType = mPieceLibrary[key];

                BasePiece newPiece = CreatePiece(pieceType);
                newPiece.Setup(teamColor, spriteColor, this);
                newPieces.Add(newPiece);
            }

            return newPieces;
        }

        private BasePiece CreatePiece(Type pieceType)
        {
            // Create new object
            GameObject newPieceObject = Instantiate(mPiecePrefab);
            newPieceObject.transform.SetParent(transform);

            // Set scale and position
            newPieceObject.transform.localScale = new Vector3(1, 1, 1);
            newPieceObject.transform.localRotation = Quaternion.identity;

            // Store new piece
            BasePiece newPiece = (BasePiece)newPieceObject.AddComponent(pieceType);

            return newPiece;
        }

        private void PlacePieces(int pawnRow, int royaltyRow, List<BasePiece> pieces, Board board) {
            int limit = pieces.Count / 2;

            for (int i = 0; i < limit; i++) {
                pieces[i].Place(board.mAllCells[i, pawnRow]);

                pieces[i + limit].Place(board.mAllCells[i, royaltyRow]);
            }
        }

        private void SetInteractive(List<BasePiece> pieces, bool value) {
            foreach (BasePiece piece in pieces)
                piece.Activate(value);
                //piece.enabled = value;
        }

        public void SwitchSides(Color color) {
            if (!mIsKingAlive) {
                ResetPieces();

                mIsKingAlive = true;

                color = Color.black;
            }

            bool isBlackTurn = color == Color.white;

            SetInteractive(mWhitePieces, !isBlackTurn);
            SetInteractive(mBlackPieces, isBlackTurn);

            foreach (BasePiece piece in mPromotedPieces) {
                bool isBlackPiece = piece.mColor != Color.white;
                bool isPartOfTeam = isBlackPiece ? isBlackTurn : !isBlackTurn;

                piece.enabled = isPartOfTeam;
            }
        }

        public void ResetPieces() {
            foreach (BasePiece piece in mPromotedPieces) {
                piece.Kill();
                Destroy(piece.gameObject);
            }

            mPromotedPieces.Clear();

            foreach (BasePiece piece in mWhitePieces)
                piece.Reset();

            foreach (BasePiece piece in mBlackPieces)
                piece.Reset();
        }

        public void PromotePiece(Pawn pawn, Cell cell, Color teamColor, Color spriteColor) {
            pawn.Kill();

            BasePiece promotedPiece = CreatePiece(typeof(Queen));
            promotedPiece.Setup(teamColor, spriteColor, this);

            promotedPiece.Place(cell);

            mPromotedPieces.Add(promotedPiece);
        }
    }
}
