using Connect4.enumerations;

namespace Connect4
{
    internal class GameState
    {
        internal char[,] Board { private set; get; }
        internal int Height { private set; get; }
        internal int Width { private set; get; }
        internal Turn Turn { private set; get; } = Turn.Blue;
        internal State State { private set; get; } = State.GameInProgress;

        private int[] numberOfDiscsPerCol;
        internal GameState(int width, int height)
        {
            Board = new char[width, height];
            numberOfDiscsPerCol = new int[width];
            for (int col = 0; col < width; col++)
            {
                numberOfDiscsPerCol[col] = 0;
                for (int row = 0; row < height; row++)
                    Board[col, row] = '_';
            }
        
            Width = width;
            Height = height;
        }

        internal bool TryToAddDisc(int col)
        {
            // validate input:
            if (!ValidateInput(col)) return false;
            Board[col, numberOfDiscsPerCol[col]] = (char)Turn;
            numberOfDiscsPerCol[col]++;
            if (CheckIfWon(col))
            {
                if (Turn == Turn.Blue) State = State.BlueWon;
                else State = State.RedWon;
            }
            else if (CheckForStaleMate()) State = State.Stalemate;

            if (Turn == Turn.Blue) Turn = Turn.Red;
            else Turn = Turn.Blue;
  
            return true;
        }

        private bool ValidateInput(int col)
        {
            if (State != State.GameInProgress) return false;
            if (col < 0 || col >= Width) return false;
            if (Board[col, Height - 1] != '_') return false;
            return true;
        }

        private bool CheckIfWon(int col)
        {
            int row = numberOfDiscsPerCol[col] - 1;

            // check horizontal
            if (CheckSequenceAroundPositionForOneDirectionOnly(col, row, 1, 0)) return true;

            // check vertical
            if (CheckSequenceAroundPositionForOneDirectionOnly(col, row, 0, 1)) return true;

            // check diagonally, left bottom to right top
            if (CheckSequenceAroundPositionForOneDirectionOnly(col, row, 1, 1)) return true;

            // check diagonally, left top to right bottom
            if (CheckSequenceAroundPositionForOneDirectionOnly(col, row, 1, -1)) return true;

            return false;
        }

        private bool CheckSequenceAroundPositionForOneDirectionOnly(int col, int row, int signHorizontal, int signVertical)
        {
            // check diagonally, left bottom to right top
            int sequence = 0;
            for (int delta_pos = -3; delta_pos <= 3; delta_pos++)
            {
                int currentRow = row + signVertical * delta_pos;
                int currentCol = col + signHorizontal * delta_pos;
                if (currentRow < 0 || currentCol < 0 || currentRow >= Height || currentCol >= Width) continue;
                if (Board[currentCol, currentRow] == (char)Turn) sequence++;
                else sequence = 0;
                if (sequence == 4) return true;
            }
            return false;
        }

        private bool CheckForStaleMate()
        {
            foreach(var numberOfDiscs in numberOfDiscsPerCol)
            {
                if (numberOfDiscs < Height) return false;
            }
            return true;
        }
    }
}
