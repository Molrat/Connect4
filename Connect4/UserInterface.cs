namespace Connect4
{
    internal class UserInterface
    {
        private GameState gamestate = new(8, 7);
        internal void StartGame()
        {
            while(true)
            {
                Visualization.VisualizeGame(gamestate);

                // game is in progress:
                if (gamestate.State == enumerations.State.GameInProgress)
                {
                    // tell user what to do:
                    Console.WriteLine($"enter column number from 0 to {gamestate.Width - 1}");

                    // wait for correct input:
                    while (!(int.TryParse(Console.ReadLine(), out int col) && gamestate.TryToAddDisc(col)))
                    {
                        Console.WriteLine("wrong input");
                        Console.WriteLine($"enter column number from 0 to {gamestate.Width - 1}");
                    }
                }
                // game is finished:
                else
                {
                    // tell user what to do:
                    Console.WriteLine($"type 'r' to restart game");

                    // wait for correct input:
                    while (Console.ReadLine() != "r")
                    {
                        Console.WriteLine($"type 'r' to restart game");
                    }
                    gamestate = new(7, 8);
                }

            }
        }
    }
}
