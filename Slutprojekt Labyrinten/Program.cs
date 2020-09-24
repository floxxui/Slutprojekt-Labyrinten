using System;

namespace Slutprojekt_Labyrinten
{
    class Program
    {
        //Inlämnad version 3
        //Programmet är inte helt färdigt ennu. Det finns fortfarande inget sätt att vinna på eller att ta sig in i basement
        //Vissa event excisterar inte heller.
       
        static bool roomSwitch = true; //En bool som gör det möjligt att byta rum när man är redo att byta rum. 
        static bool eventActivation = false; //Gör det möjligt att aktivera ett event.
        //Behöver vara accessable överallt
        static bool[] roomEvents = { false, false, false, false, false, false };
        //walking closet, bedroom, kitchen, library, gaming room, basement
        //När ett event har skett i något rum så kommer boolen ändras till true. På så sätt kan inte samma event upprepas

        static void Main(string[] args)
        {
            string currentRoom = "starting room"; //Rummet som man befinner sig i. Kommer ändras när man gått in i ett nytt rum

            Random generator = new Random();

            bool[] keys = { false, false }; //första är basement key, andra är exit key.
            //Nycklarna som kommer användas under programmet för att låsa upp nya områden i huset.

            bool activated = false; //När man aktiverar ett event
            
            int[] characterStats =
            {
                5, //hp
                0, //attack
                0, //defense
                //Sina stats som kommer användas i fighten i basement (Hann inte programmera in)
            }; 

            string[] rooms =
            {
                "Starting room", //0
                "Living room", //1
                "Bedroom", //2
                "Kitchen", //3
                "Basement", //4
                "Walking closet", //5
                "Main hall", //6
                "Library", //7
                "Gaming Room", //8
                "Hallway", //9
                "Exit" //10 (Detta är egentligen inget rum, men värdet kommer användas och står därför med

                //Namnen på alla rum i huset. Nummret visar vilket rum som är kopplat till vilka beskrivningar och vart man kan ta sig därifrån
            };

            string[] roomInitial =
            {
                "sr", "lr", "br", "k", "b", "wc", "mh", "l", "gr", "h", "e"
                //Namnen på alla initialer till rummen. Att ha med dessa hjälpte till att kunna röra sig från ett rum till ett annat, eftersom det annars blev fel
            };

            string[] roomDesc = 
            { 
                "There are some cracks in the walls and the floor. The room was completely empty.\n", //0
                "living room description\n", //1
                "There's a large bed next to the wall with a bedside table next to it. On the flor, you can see a matress stretching from one side of the room to the other. On the other side of the room, there's a cabinet with a small mirror ontop.\n", //2
                "The room is quite small. There's a fridge crambed next to the counter with a few cabinets over. On the other side of the room, there are a few paintings hanging on the wall.\n", //3
                "The floor and walls are made out of wood. It seem to have molded in some corners. By the end of the room, you see a staircase leading down. You dont know where it's headed.\n", //4
                "It's quite a small walking closet. There are old clothes hanging from the ceiling, and small boxes of clothes laying everywhere.\n", //5
                "Main hall description\n", //6 
                "It's a large open space, where there are large bookshelfes covering all the walls. They are filled with books from a - z. In the middle of the room, there are small tables with chairs. Books and papers are stacked on the tables.\n", //7
                "The room sure was impressive. There's a computer standig on a table next to the wall. You can also find consoles from several companies which have excisted throughout the years, and a large TV hung up on the wall.\n", //8
                "Hallway description\n" //9

                //Basic beskrivning på varje rum
            }; 

            string[] investigateDesc =
            {
                "You take a closer look at the cracks in the walls and the floor. However, you dont seem to find anything interesting.\n", //0
                "room2\n", //1
                "You walk up to the mirror and take a look at yourself. You feel sick. It looks like you've been rottening away for ages. You look incredibly sick. Maybe some medicine and hygiene products can help...\n", //2 //Ide: Mirror. Confidense boost or loss (random) (+5 or -1 hp) Can only be done with makeup kit
                "On the cabinet next to the fridge, there's a bowl with apples. They look newly picked and completely harmless. You start to wonder if anyone would notice if you ate one...\n", //3 //Ide: Food on counter. Extra stamina (+1 hp)
                "You walk up to the stairs and look down. It's quite a long staircase. Suddenly, you hear a noice coming from downstairs. What could be down there..?\n", //4 //Ide: after enemy defeat: exit key = true
                "You look around for a bit. By the far corner of the room, you can find a small box containing medicine and hygiene products. Maybe you could use these for your own benefit...\n", //5 //Ide: Find makeup kit
                "room7\n", //6
                "You look around the books, and you can find 4 interesting ones. One is about defending yourself, one about combat, one about how to avoid a fight and one about how to stay healthy. This could be useful...\n", //7 //Ide: Read books, first enemy attack will always be a miss (random)
                "You find yourself picking up the gameboy laying on a cabinet. You used to own one when you were young. There's seems to be a pokemon yellow gamecard inside, however something was off about it...\n", //8 //Ide: Win game = +2 attack +2 defense (ett försök)
                "room10\n" //9

                //Beskrivning på rum om man bestämmer sig för att investigera rummet. Kan leda till att man hittar nödvändiga saker
            };

            string[] roomMovement =
            {
                "Kitchen(k)", //0
                "Hallway(h), Gaming room (gr), Bedroom(br), Main hall(mh)", //1
                "Livingroom(lr), Walking closet(wc)", //2
                "Starting room(sr), Hallway(h)", //3
                "Main hall(mh)", //4
                "Bedroom(br)", //5
                "Hallway(h), Living room(lr), Basement(b), Library(l)", //6
                "Main hall(mh)", //7
                "Living room(lr)", //8
                "Kitchen(k), Living room (lr), Main hall(mh), Exit(e)", //9

                //Vart man kan ta sig från varje rum
            };
            
            while (true)
            {

                if (currentRoom == "starting room")
                {
                    BaseRoom(rooms, roomDesc, 0, investigateDesc); //en metod som beskriver rummet och även frågar om man vill investigatea
                    

                    while (roomSwitch == true) //En loop som kommer fortsätta tills man bytt rum. Detta händer när roomSwitch ändrats till av, vilket den gör i någon av "ANSWER"-metoderna
                    {
                        string playerAnswer = HeadingNext(roomMovement, 0);
                        //En metod som frågar vart man vill gå näst.
                        //cred till Sebastian Dankwardt TE18D som påminnde mig om hur man använder ett returnat värde från en metod

                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 3); //kitchen
                        //En metod som checkar om man förflytta sig till någon av rummen med förflyttningstyp 1. Den ser lite annorlunda ut för basement och exit.
                        
                        if (playerAnswer.ToLower() != "k")
                        {
                            Console.WriteLine("Please type the initial of one of the rooms listed above");
                            //Om man inte svarat någon av rummen som man kan förflytta sig till så kommer detta meddelande visas
                        }
                        /* Lösningen är inte den snyggaste, framförallt inte när det finns flera värden. Här skulle det ha använts en 
                         * else-statement om det inte hade blivit en syntax error pga att man inte kan använda en else statement utan att 
                         * ha något annat att jämföra med. Detta är exempelvis en if-statement som i detta fall inte kunde avläsas pga att 
                         * den fanns i en metod ovanför*/
                    }                    
                }

                else if (currentRoom == "living room")
                {
                    /*Varje rum är uppbyggt på samma sätt. Det är såhär:
                     * - Rummet beskriver hur det ser ut i metoden. 
                     * - Där har man möjligheten att välja ifall man vill titta vidare efter något mer (investigate)
                     * - Loopen startar för att man ska kunna byta rum. Den går in i en metod där den visar alternativ man har
                     * - Man väljer ett rum och förflyttar sig dit
                     * - Om man gett ett ogiltigt svar så kommer den be en att svara igen
                     *Dock finns det en del undantag. Då finns det extra-quests som kommer visas om man väljer att kolla vidare i rummet
                     *Det skiljer sig också med rum som är låsta eller i basement.*/

                    BaseRoom(rooms, roomDesc, 1, investigateDesc);

                    while (roomSwitch == true)
                    {
                        string playerAnswer = HeadingNext(roomMovement, 1);

                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 9); //Hallway
                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 8); //Gaming room
                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 2); //bedroom
                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 6); //mainhall
                        //Checkar ifall playerAnswer är samma sak som någon av dessa och byter isf till det rummet. Annars visas meddelandet under
                        //Siffrorna är nummer på det rummet man vill förflytta sig till

                        if (playerAnswer.ToLower() != "h" && playerAnswer.ToLower() != "gr" && playerAnswer.ToLower() != "br" && playerAnswer.ToLower() != "mh" )
                        {
                            Console.WriteLine("Please type the initial of one of the rooms listed above");
                        } //Som sagt, inte snyggaste lösningen när det blir flera alternativa rum.
                    }
                }

                else if (currentRoom == "bedroom")
                {
                    //Samma som i rummet ovan
                    BaseRoom(rooms, roomDesc, 2, investigateDesc);

                    while (eventActivation == true)
                    {
                        activated = Event(activated);
                        //En metod som frågar om man vill aktivera eventet. Om man svarar nej fortsätter activated vara false

                        if (activated == true && roomEvents[1] == false)
                        {
                            //Om man svarat y så activated blir true och om eventet inte skätt tidigare så kommer detta hända
                            if (roomEvents[0] == true)
                            {
                                int successRate = generator.Next(1, 2);
                                // 50 50 chans att få -1 hp eller +3 hp

                                if (successRate == 1)
                                {
                                    Console.WriteLine("You applied the hygiene products and took the medicine, but you started feeling worse. You took a look at the expiring date of the medicine, and it was way off.");
                                    characterStats[0]--;
                                    Console.WriteLine("Your hp stats are now " + characterStats[0]);                                    
                                }
                                else if (successRate == 2)
                                {
                                    Console.WriteLine("You applied the hygiene products and took the medicine, and shortly after starts feeling better.");
                                    characterStats[0] =+ 3;
                                    Console.WriteLine("Your hp stats are now " + characterStats[0]);
                                }
                                roomEvents[1] = true;
                                //När eventet är klart så ändras detta event till true vilket betyder att det inte kan ske igen.
                            }
                            else if (roomEvents[0] == false)
                            {
                                Console.WriteLine("You don't have any medicine or hygiene products. Come back when you've found them");
                                //Om man inte aktiverat eventet i walking closet så kommer detta event inte kunna ske
                            }
                            eventActivation = false;
                        }
                        else if (activated == true && roomEvents[1] == true)
                        {
                            //Om eventet dock har skätt innan så kommer programmet inte låta dig få köra det igen
                            Console.WriteLine("This event has already been activated. An event can only be activated once.");
                            eventActivation = false;
                        }
                    }

                    while (roomSwitch == true)
                    {
                        string playerAnswer = HeadingNext(roomMovement, 2);

                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 1); //Living room
                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 5); //Walking closet

                        //currentRoom = ANSWER_LIVINGROOM(playerAnswer, currentRoom);
                        //currentRoom = ANSWER_WALKINGCLOSET(playerAnswer, currentRoom);
                        //Ovan var min förra lösning. Dock blev det 10 olika metoder vilket inte var en effektiv lösning över huvud taget. Jag bestämde att göra om dessa 10 metoder till två som skulle kunna användas 

                        if (playerAnswer.ToLower() != "lr" && playerAnswer.ToLower() != "wc")
                        {
                            Console.WriteLine("Please type the initial of one of the rooms listed above");
                        }
                    }
                }

                else if (currentRoom == "kitchen")
                {
                    //Samma som i rummet ovan
                    BaseRoom(rooms, roomDesc, 3, investigateDesc);
                    
                    while (roomSwitch == true)
                    {
                        string playerAnswer = HeadingNext(roomMovement, 3);

                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 0); //Starting room
                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 9); // Hallway

                        //currentRoom = ANSWER_STARTINGROOM(playerAnswer, currentRoom);
                        //currentRoom = ANSWER_HALLWAY(playerAnswer, currentRoom);
                        if (playerAnswer.ToLower() != "sr" && playerAnswer.ToLower() != "h")
                        {
                            Console.WriteLine("Please type the initial of one of the rooms listed above");
                        }
                    }
                }

                else if (currentRoom == "basement")
                {
                    //Samma som i rummet ovan
                    BaseRoom(rooms, roomDesc, 4, investigateDesc);

                    while (roomSwitch == true)
                    {
                        string playerAnswer = HeadingNext(roomMovement, 4);

                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 6); //Main hall

                        //currentRoom = ANSWER_MAINHALL(playerAnswer, currentRoom);
                        if (playerAnswer.ToLower() != "mh")
                        {
                            Console.WriteLine("Please type the initial of one of the rooms listed above");
                        }
                    }
                }

                else if (currentRoom == "walking closet")
                {
                    //Samma som i rummet ovan
                    BaseRoom(rooms, roomDesc, 5, investigateDesc);

                    while (eventActivation == true)
                    {
                        activated = Event(activated); //Metod som checkar om man vill aktivera eventet eller inte

                        if (activated == true && roomEvents[0] == false)
                        {
                            Console.WriteLine("You picked up the medicine box. It would be easier if you applied the hygiene products by a mirror...");
                            roomEvents[0] = true;
                            eventActivation = false;
                            //Ifall man aktiverar eventet för första gången
                        }
                        else if (activated == true && roomEvents[0] == true)
                        {
                            Console.WriteLine("This event has already been activated. An event can only be activated once.");
                            eventActivation = false;
                            //Ifall man försöker aktivera eventet mer än en gång
                        }
                    }

                    while (roomSwitch == true)
                    {
                        string playerAnswer = HeadingNext(roomMovement, 5);

                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 2); //Bedroom
                        //currentRoom = ANSWER_BEDROOM(playerAnswer, currentRoom);
                        if (playerAnswer.ToLower() != "br")
                        {
                            Console.WriteLine("Please type the initial of one of the rooms listed above");
                        }
                    }
                }

                else if (currentRoom == "main hall")
                {
                    //Samma som i rummet ovan
                    BaseRoom(rooms, roomDesc, 6, investigateDesc);

                    while (roomSwitch == true)
                    {
                        string playerAnswer = HeadingNext(roomMovement, 6);

                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 9); //Hallway
                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 1); //Living room
                        currentRoom = AnswerRoomtype2(playerAnswer, roomInitial, currentRoom, rooms, 4, keys, 0); //Basement
                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 7); //Library

                        //currentRoom = ANSWER_HALLWAY(playerAnswer, currentRoom);
                        //currentRoom = ANSWER_LIVINGROOM(playerAnswer, currentRoom);
                        //currentRoom = ANSWER_BASEMENT(playerAnswer, currentRoom, basementKey);
                        //currentRoom = ANSWER_LIBRARY(playerAnswer, currentRoom);
                        if (playerAnswer.ToLower() != "h" && playerAnswer.ToLower() != "lr" && playerAnswer.ToLower() != "b" && playerAnswer.ToLower() != "l")
                        {
                            Console.WriteLine("Please type the initial of one of the rooms listed above");
                        }
                    }
                }

                else if (currentRoom == "library")
                {
                    //Samma som i rummet ovan
                    BaseRoom(rooms, roomDesc, 7, investigateDesc);

                    while (roomSwitch == true)
                    {
                        string playerAnswer = HeadingNext(roomMovement, 7);

                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 6); //Main hall

                        //currentRoom = ANSWER_MAINHALL(playerAnswer, currentRoom);
                        if (playerAnswer.ToLower() != "mh")
                        {
                            Console.WriteLine("Please type the initial of one of the rooms listed above");
                        }
                    }
                }

                else if (currentRoom == "gaming room")
                {
                    //Samma som i rummet ovan
                    BaseRoom(rooms, roomDesc, 8, investigateDesc);

                    while (roomSwitch == true)
                    {
                        string playerAnswer = HeadingNext(roomMovement, 8);
                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 1); //Living room
                        //currentRoom = ANSWER_LIVINGROOM(playerAnswer, currentRoom);
                        if (playerAnswer.ToLower() != "lr")
                        {
                            Console.WriteLine("Please type the initial of one of the rooms listed above");
                        }

                    }
                }

                else if (currentRoom == "hallway")
                {
                    //Samma som i rummet ovan
                    BaseRoom(rooms, roomDesc, 9, investigateDesc);

                    while (roomSwitch == true)
                    {
                        string playerAnswer = HeadingNext(roomMovement, 9);

                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 4); //Kitchen
                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 1); //Living room
                        currentRoom = AnswerRoomtype1(playerAnswer, roomInitial, currentRoom, rooms, 6); //Main hall
                        currentRoom = AnswerRoomtype2(playerAnswer, roomInitial, currentRoom, rooms, 10, keys, 1); //Exit

                        if (playerAnswer.ToLower() != "k" && playerAnswer.ToLower() != "lr" && playerAnswer.ToLower() != "mh" && playerAnswer.ToLower() != "e")
                        {
                            Console.WriteLine("Please type the initial of one of the rooms listed above");
                        }
                    }
                }

                else if (currentRoom == "exit")
                {
                    Console.WriteLine("Congratulations! You have escaped the house!");
                    Console.WriteLine("Thank you for playing!");
                    break;
                }

                roomSwitch = true;
                eventActivation = false;
                //ändrar tillbaka till orginella värde så processen kan ske igen
            }
        }

        static void BaseRoom(string[] room, string[] roomDesc, int roomNumber, string[] investigateDesc)
        {
            //En metod för hur ett rum ska se ut på basen
            Console.WriteLine("\nCurrent room: " + room[roomNumber] + "\n");
            Console.WriteLine(roomDesc[roomNumber]);
            //Simpel beskrivning på rummet man befinner sig i

            Console.WriteLine("Would you like to investigate? (y/n)");
            string playerAnswer;

            while (true)
            {
                playerAnswer = Console.ReadLine();

                if (playerAnswer.ToLower() == "y")
                {
                    Console.WriteLine(investigateDesc[roomNumber]);
                    eventActivation = true;
                    break;
                    //Om spelaren vill investigatea så får den upp beskrivningar. 
                }
                else if (playerAnswer.ToLower() == "n")
                {
                    break;
                    //Annars skippar den och bryter loopen
                }
                else
                {
                    Console.WriteLine("Would you like to investigate? (y/n)");
                    //Frågan kommer igen om man inte gav ett giltigt svar. Loopen bryts inte förän man svarat y eller n.
                }
            }
        }

        static bool Event(bool activated)
        {
            Console.WriteLine("Activate event? (y/n)");
            string playerAnswer = Console.ReadLine();

            if (playerAnswer.ToLower() == "y")
            {
                activated = true;
                //Aktiverar eventet ifall man svarar ja
            }
            else if (playerAnswer.ToLower() == "n")
            {
                eventActivation = false;
                //Avslutar loopen ifall man inte vill aktivera eventet
            }
            else
            {
                Console.WriteLine("Please answer with y or n");
                //säger till en att svara ja eller nej
            }
            return activated;
        }

        static string HeadingNext(string[] roomMovement, int n)
        {         
                Console.WriteLine("Where would you like to head next?");

                Console.WriteLine(roomMovement[n]); 
            //Den skriver ut vilka val man har från en string arrayer där stringen som kommer visas är densamma som till rummet man befinner sig i

                string playerAnswer = Console.ReadLine();

            return playerAnswer;
            //playerAnswer returnas så att värdet ska kunna användas till att ändra rum.
        }

        static string AnswerRoomtype1(string playerAnswer, string[] roomInitial, string currentRoom, string[] rooms, int whichRoom)
        {
            if (playerAnswer.ToLower() == roomInitial[whichRoom])
            {
                currentRoom = rooms[whichRoom].ToLower();
                roomSwitch = false;
            }
            //Detta är en metod som kommer jämföra användarens svar mot initialen som kommer vara olika baserat på vilket nummer (int whichRoom) som den har. På så sätt kan jag använda den till varje room-option i varje rum
            //Efter att currentRoom ändrat värde så kommer roomSwitch ändras till false, vilket bryter loopen som metoden befinner sig i
            return currentRoom;
        }

        static string AnswerRoomtype2(string playerAnswer, string[] roomInitial, string currentRoom, string[] rooms, int whichRoom, bool[] keys, int keyNumber)
        {
            //Denna metod är andra versionen på hur ett rum kan se ut. Den används till basement och exit.
            if (playerAnswer.ToLower() == roomInitial[whichRoom])
            {
                if (keys[keyNumber] == true)
                {
                    currentRoom = rooms[whichRoom].ToLower();
                    roomSwitch = false;
                    //Om man har nyckeln som behövs så kommer den byta rum och avsluta loopen precis som vanligt
                }
                else if (keys[keyNumber] == false)
                {
                    Console.WriteLine("You tried to open the door, but it was locked. Maybe you could find a key somewhere..?");
                    //Annars kommer programmet säga till att hitta den. Loopen avslutas inte så man slipper gå igenom samma text i rummet igen
                }
            }
            return currentRoom;
            //Denna brukade vara två olika metoder. Dock gjorde jag exakt som ovan och ändrade till en metod för att minska min kod.
        }
    }
}
