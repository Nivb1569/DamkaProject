using System;

namespace DamkaProject
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
-מחיקת המסך בין כל תור 
- אם באלנו בינה מלאכותית אז אם המחשב מגיע לשורה אחת לפני אחרונה הוא יבחר להפוך למלך
- מה קורה במצב שאין מהלכים-מחשב+שחקן choseNextMove
- 

 */

