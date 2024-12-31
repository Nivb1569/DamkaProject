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
- אם יש דילוג חייב לבצע אותו - שחקן
- בעיה קטנה בבדיקה של אם יש לי קפיצה נוספת, כי אם הייתי מלך אני לא יודע אם קפצתי קדימה או אחורה checkIfCanJumpAgain

 */

