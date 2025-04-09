
using System.Data.Common;

internal class Program {

static List<string> words = [];

    private static void Main(string[] args) {
        Console.Clear(); 
        LoadWordFile("words.txt");
        InitalsetupMethod();
    }

    private static void LoadWordFile(string filePath) {
        try {
            words = new List<string>(File.ReadAllLines(filePath));
        }
        catch (Exception ex) {
            Console.WriteLine("Error loading words from file: " + ex.Message);
        }
    }

    private static void InitalsetupMethod() {
        Console.WriteLine("Welcome to Hangman!");
        MainMenu();
    }

    private static void MainMenu(){
        Console.WriteLine("- - - - - - - - - - - - - -- - - - - -- - - - -");
        Console.WriteLine("Select a following option...");
        Console.WriteLine("> New game - random word (r)");
        Console.WriteLine("> New game - select word (s)");
        Console.WriteLine("> Exit program - (e)");
        Console.WriteLine("- - - - - - - - - - - - - -- - - - - -- - - - -");
        char userInput = Console.ReadKey().KeyChar;
        userInput = char.ToUpper(userInput);
        ProcessUserInput(userInput);    
    }

    private static void ProcessUserInput(char userInput) {
       if (userInput=='R'){
        string chosenWord = RandomWord();
        NewGame(chosenWord);
       } else if (userInput=='S'){
        string chosenWord = InputNewWord();
        NewGame(chosenWord);
       } else if (userInput=='E'){
         Console.Clear(); 
         Environment.Exit(0);
       } else {
        Console.Clear(); 
        Console.WriteLine("Invalid Input");
        MainMenu();
       }
    }

    private static void NewGame(string chosenWord) {
            var lives = 5;
            Console.Clear(); 
            Console.WriteLine("Starting new game...");
            // Console.WriteLine("DEBUG ONLY : RANDOM WORD = "+chosenWord);
            var unguessedWord = new string('_', chosenWord.Length);
            char[] unguessedWordArray = unguessedWord.ToCharArray();
            List<char> guessedLetters = [];
            while (lives>0 && unguessedWordArray.Contains('_')){
                if (guessedLetters.Count>0){
                var guessedString = string.Join(" ", guessedLetters);
                Console.WriteLine();
                Console.WriteLine("Previous guesses : "+guessedString);
                Console.WriteLine();   
                }
                Console.WriteLine("You have "+lives+" lives left");
                Console.WriteLine();
                foreach (char letter in unguessedWordArray)
                {
                    Console.Write(letter+" ");
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Guess a letter...");
                Console.WriteLine();
                char userInput = Console.ReadKey().KeyChar;
                userInput = char.ToUpper(userInput);
                Console.Clear(); 
                Console.WriteLine();
                if(char.IsLetter(userInput)){
                if (guessedLetters.Contains(userInput)){
                    Console.WriteLine("You have already guessed "+userInput);
                } else {
                    guessedLetters.Add(userInput);
                    if (chosenWord.Contains(userInput)){
                        for (int i = 0; i < chosenWord.Length; i++){
                            //Issues with arrays and set lengths and updating values in strings 
                            if (chosenWord[i].Equals(userInput))
                                unguessedWordArray[i] = userInput;
                            }
                    } else {
                         Console.WriteLine("The word does not contain "+userInput+".");
                         lives--;
                    }
                }
                } else {
                    Console.WriteLine("Please enter a valid A-Z letter");
                }
   
            }
            if(lives==0){
                Console.WriteLine();
                Console.WriteLine("You did not correctly guess the word!");
                Console.WriteLine("Correct word : "+chosenWord);
            }
            if(!unguessedWordArray.Contains('_')){
                Console.WriteLine("Congratulations! You guessed the word!");
                Console.WriteLine("Correct word : "+chosenWord);
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
            Console.Clear(); 
            MainMenu();
       
    }

    private static string RandomWord() {
            return words[new Random().Next(0, words.Count)];
    }
    private static string InputNewWord() {
            Console.WriteLine("Please enter the word you would like to use");
            string userInput = Console.ReadLine();
            string capitalUserInput = userInput.ToUpper();
            return capitalUserInput;
       
    }
}