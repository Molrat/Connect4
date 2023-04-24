using Connect4.enumerations;

namespace Connect4
{
    internal static class Visualization
    {
        internal static void VisualizeGame(GameState gameState)
        {
            Console.WriteLine();
            for (int row = gameState.Height  - 1; row >= 0; row--)
            {
                string printedRow = "|";
                for (int col = 0; col < gameState.Width; col ++)
                {
                    printedRow += gameState.Board[col, row] + "|";
                }
                Console.WriteLine(printedRow);
            }

            // print collumn numbers for clarity
            string columnnumbers = " ";
            for(int col = 0; col < gameState.Width; col++)
            {
                columnnumbers += $"{col} ";
            }
            Console.WriteLine(columnnumbers);

            // print turn or win or draw
            if (gameState.State == State.BlueWon) Console.WriteLine("Blue won!");
            else if (gameState.State == State.RedWon) Console.WriteLine("Red won!");
            else if (gameState.State == State.Stalemate) Console.WriteLine("Stalemate!");
            else if (gameState.Turn == Turn.Blue) Console.WriteLine("Blue's turn");
            else if (gameState.Turn == Turn.Red) Console.WriteLine("Red's turn");

            Console.WriteLine();
        }
    }
}
