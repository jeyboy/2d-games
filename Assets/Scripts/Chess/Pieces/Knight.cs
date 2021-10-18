using UnityEngine;
using UnityEngine.UI;

namespace Chess
{
    public class Knight : BasePiece
    {
        public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
        {
            base.Setup(newTeamColor, newSpriteColor, newPieceManager);

            //mMovement = new Vector3Int(7, 7, 0);
            GetComponent<Image>().sprite = Resources.Load<Sprite>("T_Knight");
        }

        private void CreateCellPath(int flipper) {
            int currentX = mCurrentCell.mBoardPosition.x;
            int currentY = mCurrentCell.mBoardPosition.y;

            MatchesState(currentX - 2, currentY + flipper);
            MatchesState(currentX - 1, currentY + (2 * flipper));
            MatchesState(currentX + 1, currentY + (2 * flipper));
            MatchesState(currentX + 2, currentY + flipper);
        }

        protected override void CheckPathing()
        {
            CreateCellPath(1);
            CreateCellPath(-1);

        }

        private void MatchesState(int targetX, int targetY) {
            CellState cellState = mCurrentCell.mBoard.ValidateCell(targetX, targetY, this);

            if (cellState != CellState.Friendly && cellState != CellState.OutOfBounds) {
                mHighlightedCells.Add(mCurrentCell.mBoard.mAllCells[targetX, targetY]);
            }
        }
    }
}
