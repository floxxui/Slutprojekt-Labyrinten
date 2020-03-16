using System;

namespace Slutprojekt_Labyrinten
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentRoom = ""; //Rummet som man befinner sig i. Kommer vara viktig

            string[] roomDesc = 
            { 
                "There are some cracks in the walls and the floor. The room was completely empty", //starting room
                "room2", //living room
                "room3", //bedroom
                "room4", //kitchen
                "room5", //basement
                "room6", //walking closet
                "room7", //main hall 
                "room8", //library
                "room9", //gaming room
                "room10" //hallway
            }; 

            string[] investigateDesc =
            {
                "You take a closer look at the cracks in the walls and the floor. However, you dont seem to find anything.", //starting room
                "room2", //living room
                "room3", //bedroom
                "room4", //kitchen
                "room5", //basement
                "room6", //walking closet
                "room7", //main hall 
                "room8", //library
                "room9", //gaming room
                "room10" //hallway
            };

            while (true)
            {
                if (currentRoom == "starting room")
                {
                    Console.WriteLine(currentRoom[0]);
                    
                }

                else if (currentRoom == "living room")
                {

                }

                else if (currentRoom == "bedroom")
                {

                }

                else if (currentRoom == "kitchen")
                {

                }

                else if (currentRoom == "basement")
                {

                }
            }
        }

        static string BASIC_ROOM()
        {

            Console.WriteLine("Would you like to investigate? (y/n)");

            string playerAnswer = Console.ReadLine();

            while (true)
            {
                if (playerAnswer.ToLower() == "y")
                {

                    break;
                }
                else if (playerAnswer.ToLower() == "n")
                {
                    break;
                }
                else
                {

                }
            }
            //En metod för ett repetativt rum där ingenting händer
        }

        static void MAP()
        {
            //displaya en karta 
        }
    }
}
