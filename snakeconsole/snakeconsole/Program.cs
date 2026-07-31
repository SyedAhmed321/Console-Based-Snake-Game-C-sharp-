using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace snakeconsole
{
    class Snake
    {
        int height = 20; //board height
        int width = 40; //board width

        int[] X = new int[50];
        int[] Y = new int[50];

        int fruitX;
        int fruitY;

        int score = 0;
        int parts = 2;

        ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
        char key = 'w';

        Random rndm = new Random();

        Snake()
        {
            X[0] = 15;
            Y[0] = 15;
            Console.CursorVisible = false;
            fruitX = rndm.Next(2, (width - 2));//drawing fruit ki location 
            fruitY = rndm.Next(2, (height - 2));//drawing fruit ki location 
        }
        
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("--WELCOME TO THE SNAKE GAME---\n");
            Console.Write("Enter your name to play the game : ");
            string name = Console.ReadLine();
            Console.WriteLine("How To Play : \nuse wasd to play('w' to go up, 'a' to go left, 's' to go down, 'd' to go right)");
            Console.WriteLine("press any key to continue");
            Console.ReadKey();

        start:
            {
                Snake snake = new Snake();
                try
                {
                    while (true)
                    {
                        snake.BoardBox();
                        snake.Input();
                        snake.Logic();
                    }
                }

                catch
                {
                    DateTime dateTime = DateTime.Now;
                    Console.WriteLine("\n" + dateTime);
                    Console.CursorVisible = true;
                    Console.WriteLine("\n\nGame Over");
                    try
                    {
                        Console.Write("\n\ndo you want to play again ? (y/n) : ");
                        char reply = char.Parse(Console.ReadLine().ToLower());
                        if (reply == 'y')
                        {
                            goto start;
                        }
                        else
                        {
                            Console.WriteLine("Thanks for playing \nGoodbye :)");
                        }
                    }
                    catch(Exception)
                    {
                        Console.WriteLine("You gave no answer");
                    }
                }
            }
            
            Console.ReadKey();

        }


        public void BoardBox()
        {
            Console.Clear();

            for (int i = 1; i <= (width + 2); i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("`");
            }

            for (int i = 1; i <= (width + 2); i++)
            {
                Console.SetCursorPosition(i, (height + 2));
                Console.Write("-");
            }

            for (int i = 1; i <= (height + 1); i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write("|");
            }

            for (int i = 1; i <= (height + 1); i++)
            {
                Console.SetCursorPosition((width + 2), i);
                Console.Write("|");
            }

            Console.WriteLine("\n\nSCORE : " + score);

        }
        
        public void Input()
        {
            if (Console.KeyAvailable) //checks if the button is pressed
            {
                keyInfo = Console.ReadKey(true); //keyboard se key lega
                key = keyInfo.KeyChar; // convert krega key ko character value mn aur phir key k variable ko assign krdega
            }
        }

        public void WritePoint(int x, int y) //snake ki body k parts draw krega
        {
            Console.SetCursorPosition(x, y);
            Console.Write("o");
        }

        public void Logic()
        {
            if (X[0] == fruitX)
            {
                if(Y[0]== fruitY)
                {
                    Console.Beep();
                    parts++;
                    score += 2;
                    fruitX = rndm.Next(2, (width-2));//drawing fruit ki location 
                    fruitY = rndm.Next(2, (height - 2));//drawing fruit ki location 
                }
            }

            for(int i = parts; i > 1; i--) //logic of snake's subsequent parts
            {
                X[i - 1] = X[i - 2];
                Y[i - 1] = Y[i - 2];
            }

            switch (key)
            {
                case 'w':
                    {
                        Y[0]--;
                        break;
                    }
                case 's':
                    {
                        Y[0]++;
                        break;
                    }
                case 'd':
                    {
                        X[0]++;
                        break;
                    }
                case 'a':
                    {
                        X[0]--;
                        break;
                    }
            }

            if (X[0] > width || Y[0] > height)
            {
                Console.Beep();
                Console.Beep();
                Console.Beep();
                gameover();
            }

            for (int i = 0; i <= (parts - 1); i++) //draws snake
            {
                WritePoint(X[i], Y[i]);
                WritePoint(fruitX, fruitY);
            }
            Thread.Sleep(60);
            
        }

        public void gameover()
        {
            string data = "\nyour score is : " + score;

            var path = @"E:\CP PROJECT\snakeconsole\scores\scorelist.txt";
            File.AppendAllText(path, data);
            throw new NotImplementedException();
        }
        
    }
}
