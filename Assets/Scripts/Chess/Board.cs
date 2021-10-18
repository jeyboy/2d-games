using UnityEngine;
using UnityEngine.UI;

namespace Chess {
    public enum CellState { 
        None,
        Friendly,
        Enemy,
        Free,
        OutOfBounds
    }

    public class Board : MonoBehaviour
    {
        public const int cellsLength = 8;

        public GameObject mCellPrefab;

        public Cell[,] mAllCells = new Cell[cellsLength, cellsLength];

        public void Create()
        {
            for (int y = 0; y < cellsLength; y++)
            {
                int offset = (y % 2 != 0) ? 0 : 1;

                for (int x = 0; x < cellsLength; x++)
                {
                    GameObject newCell = Instantiate(mCellPrefab, transform);

                    RectTransform rectTransform = newCell.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = new Vector2((x * 100) + 50, (y * 100) + 50);

                    mAllCells[x, y] = newCell.GetComponent<Cell>();
                    mAllCells[x, y].Setup(new Vector2Int(x, y), this);

                    if (x % 2 == offset)
                    {
                        mAllCells[x, y].GetComponent<Image>().color = new Color32(230, 220, 187, 255);
                    }
                }
            }
        }

        public CellState ValidateCell(int targetX, int targetY, BasePiece checkingPiece) {
            if (targetX < 0 || targetY < 0 || targetX > Board.cellsLength - 1 || targetY > Board.cellsLength - 1) {
                return CellState.OutOfBounds;
            }

            Cell targetCell = mAllCells[targetX, targetY];

            if (targetCell.mCurrentPiece != null) {
                if (checkingPiece.mColor == targetCell.mCurrentPiece.mColor)
                    return CellState.Friendly;
                else
                    return CellState.Enemy;
            }

            return CellState.Free;
        }
    }
}
