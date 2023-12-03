using System.Transactions;

namespace TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Initial Data Array
            string[,] grid =
            {
                {"1", "2", "3"},
                {"4", "5", "6"},
                {"7", "8", "9"}
            };

            // Draw the initial grid
            GameStart(grid);
            // Randomly choose who starts
            string currentPlayer = StartPlayer();
            //Game Loop
            while (CheckWinner(grid) == "No winner")
            {
                // Ask for input
                Console.WriteLine("\nEnter a number between 1-9");
                // Check if input is valid
                string input = Console.ReadLine();
                bool isValid = IsValidInput(grid, input);
                // If input is invalid, ask for input again
                while (!isValid)
                {
                    Console.WriteLine("Invalid input");
                    Console.WriteLine("Enter a number between 1-9");
                    input = Console.ReadLine();
                    isValid = IsValidInput(grid, input);
                }
                // If input is valid, update grid
                if (isValid)
                {
                    grid = UpdateGrid(grid, input, currentPlayer);
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
                // Check if there is a winner
                string winner = CheckWinner(grid);
                // If there is a winner, end the game
                if (winner != "No winner")
                {
                    Console.WriteLine($"The winner is {winner}!");
                }
                // If there is no winner yet, see if there are any slots left
                else if (NoSlotsLeft(grid))
                {
                    Console.WriteLine("It's a draw");
                    break;
                }
                // Else change player
                else
                {
                    currentPlayer = currentPlayer == "X" ? "O" : "X";
                }
                Console.WriteLine($"Player {currentPlayer}'s turn:");
            }
        }
        public static void DrawGrid(string[,] grid)
        {
            //Draws the grid
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                Console.WriteLine("-------------");
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write("| " + grid[i, j] + " ");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("-------------");
        }
        public static void GameStart(string[,] grid)
        {
            DrawGrid(grid);
        }
        public static string StartPlayer()
        {
            string player1 = "X";
            string player2 = "O";

            //Randomly choose between player1 and player2
            Random rnd = new Random();
            int player = rnd.Next(1, 3) == 1 ? 1 : 2;
            string token = player == 1 ? player1 : player2;
            string output = player == 1 ? "Player 1" : "Player 2";
            Console.WriteLine($"\n{output} ('{token}') starts");
            return token;
        }
        public static string[,] UpdateGrid(string[,] grid, string input, string currentPlayer)
        {
            //Update grid with player input
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {

                    if (grid[i, j] == input)
                    {
                        grid[i, j] = currentPlayer;
                    }
                }
            }
            DrawGrid(grid);
            return grid;
        }
        public static bool IsValidInput(string[,] grid, string input)
        {
            bool isNumber = false;
            bool isInRange = false;
            bool isAvailable = false;

            if (int.TryParse(input, out int number))
                isNumber = true;
            if (number > 0 && number < 10)
                isInRange = true;
            if (isNumber && isInRange)
            {
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        if (grid[i, j] == input)
                            isAvailable = true;
                    }
                }
            }
            return isNumber && isInRange && isAvailable;
        }
        public static string CheckWinner(string[,] grid)
        {
            //Check rows
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                if (grid[i, 0] == grid[i, 1] && grid[i, 1] == grid[i, 2])
                {
                    return grid[i, 0];
                }
            }
            //Check columns
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                if (grid[0, i] == grid[1, i] && grid[1, i] == grid[2, i])
                    return grid[0, i];
            }
            //Check diagonals
            if (grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2])
            {
                return grid[0, 0];
            }
            if (grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0])
            {
                return grid[0, 2];
            }
            return "No winner";
        }
        public static bool NoSlotsLeft(string[,] grid)
        {
            //Check if there are any slots left
            bool noSlotsLeft = true;
            foreach (string slot in grid)
            {
                if (slot != "X" && slot != "O")
                {
                    noSlotsLeft = false;
                }
            }
            return noSlotsLeft;
        }
    }
}
