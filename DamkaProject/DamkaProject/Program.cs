using System;

namespace Ex02
{
    class Program
    {
        public static void Main()
        {
            Game game = new Game(); 
            game.Run();
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}
/*

משימות שבוצעו:
-אם יש דילוג אז מחשב מבצע אותו
-לאחר ביצוע צעד מודפס הצעד הקודם
-להוסיף לחיצה אם אני רוצה לראות את מהלך המחשב
-אם יש דילוג נוסף מאותו הכלי אז התור נשאר  



משימות שנותרו: 
- מה קורה במצב שאין מהלכים-מחשב+שחקן choseNextMove
- 

 */

