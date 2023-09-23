using Microsoft.Data.Sqlite;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using WongmaneeB_QueryBuilder;
using WongmaneeB_QueryBuilder.Models;

namespace WongmaneeB_QueryBuilder
{
    internal class Program
    {
        private static string root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
        static string dbPath = root + "\\data\\data.db";

        static void Main(string[] args)
        {
            // ======================== ERASE ALL RECORDS ======================== //
            string eraseHeader = "========== ERASING ALL RECORDS ==========";

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(eraseHeader);
            Console.ResetColor();
            EraseAllRecords<Pokemon>();
            EraseAllRecords<BannedGame>();

            Console.Write(Environment.NewLine);



            // ======================== LOAD CSV COLLECTIONS ======================== //
            string csvText = " LOADING CSV COLLECTIONS ";
            int csvBorderPadding = (eraseHeader.Length - csvText.Length) / 2;
            string csvHeader = new string('=', csvBorderPadding) + csvText + new string('=', csvBorderPadding);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(csvHeader);
            Console.ResetColor();
            WriteAllToDb(ReadAllPokemonCSV());
            WriteAllToDb(ReadBannedGameCSV());

            Console.Write(Environment.NewLine);



            // ======================== LOAD SAMPLE COLLECTIONS ======================== //
            string sampleCollectionText = " LOADING SAMPLE COLLECTIONS ";
            int sampleCollectionBorderPadding = (csvHeader.Length - sampleCollectionText.Length) / 2;
            string sampleCollectionHeader = new string('=', sampleCollectionBorderPadding) + sampleCollectionText + new string('=', sampleCollectionBorderPadding);

            List<Pokemon> pokeCollection = new List<Pokemon>()
            {
                new Pokemon(1046, 899, "Bellum", "Fakemon", "Fire", "Dragon", 110, 150, 110, 45, 100, 120, 9),
                new Pokemon(1047, 900, "Lues", "Fakemon", "Water", "Poison", 140, 65, 150, 105, 150, 35, 9),
                new Pokemon(1048, 901, "Fames", "Fakemon", "Grass", "Ghost", 120, 135, 130, 70, 110, 80, 9),
                new Pokemon(1049, 902, "Libitina", "Fakemon", "Dark", "Flying", 90, 90, 80, 145, 125, 115, 9)
            };

            List<BannedGame> bgCollection = new List<BannedGame>()
            {
                new BannedGame(137, "Phantasy Star Online 2", "Phantasy Star", "Belgium", "Usage of lootboxes (gambling)."),
                new BannedGame(138, "PUBG: Battlegrounds", "PUBG: Battlegrounds", "Afghanistan", "Protecting younger generations from a bad influence."),
                new BannedGame(139, "Paladins: Champions of the Realm", "Paladins", "Mainland China", "Over-revealing female characters, blood, gore, and vulgar content."),
                new BannedGame(140, "Roblox", "Roblox", "Guatemala", "Potential violation of the safety of children and adolescents.")
            };

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(sampleCollectionHeader);
            Console.ResetColor();
            WriteAllToDb(pokeCollection);
            WriteAllToDb(bgCollection);

            Console.Write(Environment.NewLine);




            // ======================== SAMPLE OBJECTS ======================== //
            string sampleObjText = $" LOADING SAMPLE OBJECTS ";
            int sampleObjBorderPadding = (sampleCollectionHeader.Length - sampleObjText.Length) / 2;
            string sampleObjHeader = new string('=', sampleObjBorderPadding) + sampleObjText + new string('=', sampleObjBorderPadding);

            Pokemon pokemon = new Pokemon(1050, 903, "Metus", "Fakemon", "Electric", "Ground", 110, 160, 110, 45, 100, 120, 9);
            BannedGame bannedGame = new BannedGame(141, "Grand Theft Auto: San Andreas", "Grand Theft Auto", "Thailand", "A case where an 18-year-old Thai player supplosedly influenced by Grand Theft Auto killed a taxi driver in Bangkok.");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(sampleObjHeader);
            Console.ResetColor();
            WriteToDB(pokemon);
            WriteToDB(bannedGame);

            Console.Write(Environment.NewLine);
        }



        // ======================== ERASE ALL RECORDS ======================== //
        public static void EraseAllRecords<T>() where T : IClassModel
        {
            // ========== DELETEALL ========== //
            using (var queryBuilder = new QueryBuilder(dbPath))
            {
                queryBuilder.DeleteAll<T>();
            }

            // ========== CONFIRMATION MSG ========== //
            Console.Write("All records for ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(typeof(T).Name);
            Console.ResetColor();
            Console.WriteLine(" have been erased.");
        }



        // ======================== WRITE ALL OBJECTS IN A COLLECTION TO THE DB ======================== //
        public static void WriteAllToDb<T>(List<T> objCollection) where T : IClassModel
        {
            // ========== WRITE ALL (CREATE) TO DB ========== //
            using (var queryBuilder = new QueryBuilder(dbPath))
            {
                foreach (var obj in objCollection)
                {
                    queryBuilder.Create(obj);
                }

                if (typeof(T) == typeof(Pokemon))
                {
                    Console.Write("All ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"Pokemon ({objCollection.Count} pokemon)");
                    Console.ResetColor();
                    Console.WriteLine(" have successfully been added to the Pokedex.");
                }
                else if (typeof(T) == typeof(BannedGame))
                {
                    Console.Write("All ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"Banned Games ({objCollection.Count} games)");
                    Console.ResetColor();
                    Console.WriteLine(" have successfully been added to the list of banned games.");
                }
            }
        }



        // ======================== WRITE ALL OBJECTS IN A COLLECTION TO THE DB ======================== //
        public static void WriteToDB<T>(T obj) where T : IClassModel
        {
            // ========== WRITE (CREATE) TO DB ========== //
            using (var queryBuilder = new QueryBuilder(dbPath))
            {
                queryBuilder.Create(obj);
            }

            if (typeof(T) == typeof(Pokemon))
            {
                Pokemon pokemon = obj as Pokemon;

                Console.Write("Fakemon '");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(pokemon.Name);
                Console.ResetColor();
                Console.WriteLine("' has successfully been added to the Pokedex.");
            }
            else if (typeof(T) == typeof(BannedGame))
            {
                BannedGame bannedGame = obj as BannedGame;

                Console.Write("'");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(bannedGame.Title);
                Console.ResetColor();
                Console.WriteLine("' has successfully been added to the list of banned games.");
            }
        }



        // ======================== READ FROM CSV ======================== //
        static List<Pokemon> ReadAllPokemonCSV()
        {
            // ========== DECLARE VARIABLES ========== //
            List<Pokemon> pokemonFromFile = new List<Pokemon>();
            var filePath = root + "\\data\\AllPokemon.csv";
            StreamReader sr = new StreamReader(filePath);
            List<string> lines = new List<string>();

            // ========== STREAMREADER ========== //
            using (sr)
            {
                while (!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());
                }

                for (int i = 0; i < lines.Count; i++)
                {
                    // ========== ID ========== //
                    int id = i + 1;

                    // ========== BASIC INFO ========== //
                    int dexNumber = ParseInt(lines[i].Split(',')[0]);
                    string name = lines[i].Split(',')[1];
                    string form = lines[i].Split(',')[2];
                    string type1 = lines[i].Split(',')[3];
                    string type2 = lines[i].Split(',')[4];

                    // ========== STATS ========== //
                    int hp = ParseInt(lines[i].Split(',')[5]);
                    int atk = ParseInt(lines[i].Split(',')[6]);
                    int def = ParseInt(lines[i].Split(',')[7]);
                    int spAtk = ParseInt(lines[i].Split(',')[8]);
                    int spDef = ParseInt(lines[i].Split(',')[9]);
                    int speed = ParseInt(lines[i].Split(',')[10]);

                    // ========== MISC ========== //
                    int gen = ParseInt(lines[i].Split(',')[11]);

                    // ========== CREATE POKEMON OBJECT ========== //
                    Pokemon p = new Pokemon(id, dexNumber, name, form, type1, type2, hp, atk, def, spAtk, spDef, speed, gen);
                    pokemonFromFile.Add(p);
                }
            }

            return pokemonFromFile;
        }

        static List<BannedGame> ReadBannedGameCSV()
        {
            // ========== DECLARE VARIABLES ========== //
            List<BannedGame> gameFromFile = new List<BannedGame>();
            var filePath = root + "\\data\\BannedGames.csv";
            StreamReader sr = new StreamReader(filePath);
            List<string> lines = new List<string>();

            // ========== STREAMREADER ========== //
            using (sr)
            {
                while (!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());
                }

                for (int i = 0; i < lines.Count; i++)
                {
                    // ========== ID ========== //
                    int id = i + 1;

                    // ========== BASIC INFO ========== //
                    string title = lines[i].Split(',')[0];
                    string series = lines[i].Split(',')[1];
                    string country = lines[i].Split(',')[2];
                    string details = lines[i].Split(',')[3];

                    // ========== CREATE BANNED GAME OBJECT ========== //
                    BannedGame bg = new BannedGame(id, title, series, country, details);
                    gameFromFile.Add(bg);
                }
            }

            return gameFromFile;
        }



        // ======================== PARSE METHODS ======================== //
        static int ParseInt(string valueAsString)
        {
            int valueAsInt;
            Int32.TryParse(valueAsString, out valueAsInt);
            return valueAsInt;
        }
    }
}