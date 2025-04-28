using System; // Импортируем пространство имен System для доступа к базовым классам, таким как Console.
using System.Collections.Generic; // Импортируем пространство имен для использования коллекций, таких как List.

namespace CalorieCounterApp 
// Указываем то же пространство имен, что и в CalorieHelper.cs, чтобы классы были видны друг другу.
{
    // Основной класс программы.
    class Program
    {
        // Точка входа в приложение - метод Main.
        static void Main(string[] args)
        {
            // Выводим приветственное сообщение.
            Console.WriteLine("--- Kalorienzähler ---");

            // Получаем дневной лимит калорий, вызывая статический метод из класса CalorieHelper.
            int dailyCalorieLimit = CalorieHelper.GetDailyCalorieLimit();

            // Создаем пустой список для хранения съеденных продуктов и их калорийности.
            // List<Tuple<string, int>> хранит пары: название продукта (string) и калории (int).
            List<Tuple<string, int>> foodLog = new List<Tuple<string, int>>();

            // Бесконечный цикл для добавления продуктов. Выход из цикла осуществляется с помощью 'break'.
            while (true)
            {
                // Получаем информацию о съеденном продукте от пользователя, вызывая статический метод.
                Tuple<string, int> foodItem = CalorieHelper.GetFoodItemFromUser();
                // Добавляем полученный продукт (кортеж) в список foodLog.
                foodLog.Add(foodItem);

                // Выводим текущее общее количество добавленных продуктов.
                Console.WriteLine($"Hinzugefügt: {foodItem.Item1} ({foodItem.Item2} kKal). Einträge gesamt: {foodLog.Count}");

                // Спрашиваем пользователя, хочет ли он добавить еще один продукт.
                Console.Write("Möchten Sie ein weiteres Produkt hinzufügen? (ja/nein): ");
                // Считываем ответ пользователя и приводим его к нижнему регистру.
                string addMore = Console.ReadLine().ToLower();

                // Проверяем, если ответ не начинается с "д" или "да".
                if (addMore != "ja" && addMore != "j" && addMore != "y" && addMore != "yes")
                {
                    // Если пользователь не хочет добавлять еще, выходим из цикла добавления продуктов.
                    break;
                }
            } // Конец цикла while для добавления продуктов.

            // Проверяем, были ли добавлены какие-либо продукты.
            if (foodLog.Count > 0)
            {
                // Если продукты были добавлены:
                // Сохраняем список съеденных продуктов в файл, вызывая статический метод.
                CalorieHelper.SaveFoodLogToFile(foodLog);
                // Вычисляем общее количество потребленных калорий, вызывая статический метод.
                int totalCaloriesConsumed = CalorieHelper.CalculateTotalCalories(foodLog);
                // Выводим итоговый результат (сравнение с лимитом), вызывая статический метод.
                CalorieHelper.DisplayResult(totalCaloriesConsumed, dailyCalorieLimit);
            }
            // Если пользователь не добавил ни одного продукта.
            else
            {
                // Выводим сообщение, что не было добавлено ни одного продукта.
                Console.WriteLine("\nSie haben kein Produkt hinzugefügt.");
            }

            // Выводим прощальное сообщение.
            Console.WriteLine("\n--- Das Programm wurde beendet ---");
            // Ожидаем нажатия любой клавиши перед закрытием консольного окна (удобно при запуске из IDE).
            Console.WriteLine("Zum Beenden beliebige Taste drücken...");
            // Читаем нажатие клавиши.
            Console.ReadKey();
            //Очищаем консоль
            Console.Clear();
        } 
    } 
} 
