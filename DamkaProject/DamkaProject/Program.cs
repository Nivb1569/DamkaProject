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
- לשנות פורמט הדפסה שלאחר ההדפסה יוצג המהלך הקודם
-אם יש דילוג נוסף אז התור נשאר והשהמלך היידי שניתן לעשות זה דילוג
-להוסיף בדיקה שהשם של השחקן השני יהיה שונה מהשחקן הראשון

משימות ניב:
- פונקציה GAMEOVER בודקת כל תור אם זה תיקו הפסד או ניצחון
- \להוסיף יציאה מהמשחק על ידי Q
- להוסיף צד מחשב
- \להוסיף ניקוד בין השחקנים
- מחיקת הלוח בכל תור 
-לשאול את ניב על קונבנציה בENUM במחלקה PIECE
-checkAndAddPossiblePositions לבדוק עם ניב על קונבנציה עם איקס ווי
- להגביל את השם השני שלא יהיה כמו הראשון
 */

