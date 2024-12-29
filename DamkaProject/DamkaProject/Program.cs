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
- אם יש דילוג חייב לבצע אותו
- לשנות את האותיות בהדפסה
- לשנות פורמט הדפסה שלאחר ההדפסה יוצג המהלך הקודם
- להוסיף יציאה מהמשחק על ידי Q
- להוסיף מחשב
-אם יש דילוג נוסף אז התור נשאר
-הכרזה על ניצחון
-להראות לניב את השינוי ב updateNumberOfPices במחלקה GAME
 */

