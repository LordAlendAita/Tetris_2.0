using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Threading;
using Tetris_2.Database;

namespace Tetris
{


    class Program
    {
        static int[,] board = new int[20, 10];
        static int score = 0;
        static int currentX = 0;
        static int currentY = 0;
        static int[,] currentShape;
        public static DatabaseDefiner dbContext = new DatabaseDefiner();
        User user = new User();
        //dbContext.SaveChanges();
        static void Main(string[] args)
        {
            string read = "";
            User user;
            do
            {
                Console.Clear();
                Console.WriteLine("[r]Register/[l]Login");
                read = Console.ReadLine();
                if (read == "r")
                    user = RegisterScreen();
                else if (read == "l")
                    user = LoginScreen();
            } while (read != "r" & read != "l");
            Console.CursorVisible = false;
            Console.Title = "Tetris";
            Console.SetWindowSize(30, 25);
            Console.SetBufferSize(30, 25);
            while (true)
            {
                Console.Clear();
                DrawBoard();
                MoveDown();
                Thread.Sleep(300);
                //Condition
                //break;
            }//break condition
            //Scoreboard aktualisieren
        }

        static void DrawBoard()
        {
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (board[y, x] == 0)
                        Console.Write(".");
                    else
                        Console.Write("#");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Score: " + score);
        }
        
        static void scoreboardvisual()
        {
            foreach (Scoreboard score in dbContext.scoreboards)
            {
                Console.WriteLine("{0,-20}||{1,10}||{2}",score.user.usern,score.user.HighScore,score.user.Timestamp);
            }

        }
      
        
        static void MoveDown()
        {
            if (currentShape == null)
                GenerateShape();

            if (CanMoveTo(currentX, currentY + 1, currentShape))
            {
                currentY++;
                UpdateBoard();
            }
            else
            {
                MergeShape();
                CheckLines();
                GenerateShape();
            }
        }

        static void MergeShape()
        {
            for (int y = 0; y < currentShape.GetLength(0); y++)
            {
                for (int x = 0; x < currentShape.GetLength(1); x++)
                {
                    if (currentShape[y, x] != 0)
                        board[currentY + y, currentX + x] = 1;
                }
            }
        }

        static bool CanMoveTo(int x, int y, int[,] shape)
        {
            for (int sy = 0; sy < shape.GetLength(0); sy++)
            {
                for (int sx = 0; sx < shape.GetLength(1); sx++)
                {
                    if (shape[sy, sx] != 0)
                    {
                        int nx = x + sx;
                        int ny = y + sy;

                        if (nx < 0 || nx >= 10 || ny >= 20 || (ny >= 0 && board[ny, nx] != 0))
                            return false;
                    }
                }
            }

            return true;
        }

        static void UpdateBoard()
        {
            Console.SetCursorPosition(0, 0);
            DrawBoard();
        }

        static void GenerateShape()
        {
            Random rand = new Random();
            int shapeIndex = rand.Next(0, 7);

            switch (shapeIndex)
            {
                case 0:
                    currentShape = new int[,] { { 1, 1, 1, 1 } };
                    break;
                case 1:
                    currentShape = new int[,] { { 1, 1, 1 }, { 0, 1, 0 } };
                    break;
                case 2:
                    currentShape = new int[,] { { 1, 1, 1 }, { 1, 0, 0 } };
                    break;
                case 3:
                    currentShape = new int[,] { { 1, 1, 1 }, { 0, 0, 1 } };
                    break;
                case 4:
                    currentShape = new int[,] { { 1, 1 }, { 1, 1 } };
                    break;
                case 5:
                    currentShape = new int[,] { { 1, 1, 0 }, { 0, 1, 1 } };
                    break;
                case 6:
                    currentShape = new int[,] { { 0, 1, 1 }, { 1, 1, 0 } };
                    break;
            }

            currentX = 4;
            currentY = 0;
        }

        static void CheckLines()
        {
            for (int y = 0; y < 20; y++)
            {
                bool isLine = true;
                for (int x = 0; x < 10; x++)
                {
                    if (board[y, x] == 0)
                    {
                        isLine = false;
                        break;
                    }
                }

                if (isLine)
                {
                    score += 100;
                    RemoveLine(y);
                }
            }
        }

        static void RemoveLine(int line)
        {
            for (int y = line; y > 0; y--)
            {
                for (int x = 0; x < 10; x++)
                {
                    board[y, x] = board[y - 1, x];
                }
            }

            for (int x = 0; x < 10; x++)
            {
                board[0, x] = 0;
            }
        }
        public static User RegisterScreen()
        {
            Console.Clear();
            Console.WriteLine("Register");
            User user = new User();
            Console.WriteLine("Username:");
            user.usern = Console.ReadLine();
            Console.WriteLine("Passwort:");
            user.Pw = Console.ReadLine();
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return user;
        }

        public static User LoginScreen()
        {
            string read;
            User? user;
            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Login");
                    user = new User();
                    Console.WriteLine("Username:");
                    user.usern = Console.ReadLine();
                    user = dbContext.Users.FirstOrDefault(x => x.usern == user.usern);
                } while (user == null);
                Console.WriteLine("Passwort:");
                read = Console.ReadLine();
            } while (user.Pw != read);
            return user;
        }
    }
}
