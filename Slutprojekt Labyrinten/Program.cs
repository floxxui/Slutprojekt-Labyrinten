using System;

namespace Slutprojekt_Labyrinten
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentRoom = "starting room"; //Rummet som man befinner sig i

            /* Bild på house layout finns i ibispaint x*/

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
                "You take a closer look at the cracks in the walls and the floor. However, you dont seem to find anything.", //starting room :3
                "room2", //living room
                "room3", //bedroom :3
                "room4", //kitchen :3
                "room5", //basement :3
                "room6", //walking closet :3
                "room7", //main hall :3
                "room8", //library 
                "room9", //gaming room
                "room10" //hallway :3
            };
            
            while (true)
            {
                if (currentRoom == "starting room")
                {
                    BASE_ROOM(roomDesc, 2, investigateDesc);
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

        static string BASE_ROOM(string[] roomDesc, int roomNumber, string [] investigateDesc)
        {
            Console.WriteLine(roomDesc[roomNumber]);

            Console.WriteLine("Would you like to investigate? (y/n)");

            string playerAnswer = Console.ReadLine();

            while (true)
            {
                if (playerAnswer.ToLower() == "y")
                {
                    Console.WriteLine(investigateDesc[roomNumber]);
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
            //En metod för hur ett rum ska se ut på basen
        }

        static void MAP()
        {
            //displaya en karta 
        }
    }
}
