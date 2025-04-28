using System; // Импортируем пространство имен System для доступа к базовым классам, таким как Console и Convert.
using System.Collections.Generic; // Импортируем пространство имен для использования коллекций, таких как List.
using System.IO; // Импортируем пространство имен для работы с файлами, например, для StreamWriter.

namespace CalorieCounterApp // Определяем пространство имен для нашего приложения, чтобы избежать конфликтов имен.
{
// Определяем статический класс, содержащий вспомогательные методы. Статический класс не требует создания экземпляра.
    public static class CalorieHelper
    {
        // Константа для дневной нормы калорий для мужчин.
        public const int MaleCalorieLimit = 2000;
        // Константа для дневной нормы калорий для женщин.
        public const int FemaleCalorieLimit = 1500;
        // Имя файла для сохранения данных о съеденной пище.
        public const string LogFileName = "calories_log.txt";

        // Метод для получения от пользователя информации о поле и определения дневного лимита калорий.
        public static int GetDailyCalorieLimit()
        {
            // Бесконечный цикл, который будет выполняться до тех пор, пока пользователь не введет корректный пол.
            while (true)
            {
                // Выводим запрос пользователю ввести свой пол.
                Console.Write("Sind Sie ein Mann (m) oder eine Frau (f)? ");
                // Считываем введенную пользователем строку и преобразуем ее в нижний регистр для удобства сравнения.
                string genderInput = Console.ReadLine().ToLower();

                // Проверяем, ввел ли пользователь "м" или "мужчина".
                if (genderInput == "m" || genderInput == "mann" || genderInput == "ein mann")
                {
                    // Если да, выводим установленный лимит.
                    Console.WriteLine($"Ein tägliches Kalorienlimit ist festgelegt: {MaleCalorieLimit}");
                    // Возвращаем лимит калорий для мужчин.
                    return MaleCalorieLimit;
                }
                // Проверяем, ввел ли пользователь "ж" или "женщина".
                else if (genderInput == "f" || genderInput == "frau" || genderInput == "eine frau")
                {
                     // Если да, выводим установленный лимит.
                   Console.WriteLine($"Ein tägliches Kalorienlimit ist festgelegt: {FemaleCalorieLimit} kKal.");
                    // Возвращаем лимит калорий для женщин.
                    return FemaleCalorieLimit;
                }
                // Если ввод не соответствует ни одному из ожидаемых вариантов.
                else
                {
                    // Выводим сообщение об ошибке.
                    Console.WriteLine("Ungültige Eingabe. Geben Sie bitte 'm' oder 'f'.  ");
                    // Цикл продолжится, запрашивая ввод снова.
                }
            }
        }

        // Метод для получения от пользователя информации о съеденном продукте и его калорийности.
        // Возвращает кортеж (Tuple), содержащий название продукта (string) и количество калорий (int).
        public static Tuple<string, int> GetFoodItemFromUser()
        {
            // Запрашиваем у пользователя название съеденного продукта.
            Console.Write("Was haben Sie heute gegesen?  ");
            // Считываем название продукта.
            string foodName = Console.ReadLine();

            // Переменная для хранения калорийности продукта.
            int calories;
            // Бесконечный цикл для получения корректного числового значения калорий.
            while (true)
            {
                // Запрашиваем у пользователя количество калорий.
                Console.Write($"Wie viel Kalorien in '{foodName}'?  ");
                // Считываем ввод пользователя.
                string calorieInput = Console.ReadLine();
                // Пытаемся преобразовать введенную строку в целое число (int).
                // int.TryParse возвращает true, если преобразование успешно, и false в противном случае.
                // Результат преобразования записывается в переменную calories (через out).
                if (int.TryParse(calorieInput, out calories) && calories >= 0) 
                // Добавляем проверку, что калории не отрицательные.
                {
                    // Если преобразование успешно и калории не отрицательные, выходим из цикла.
                    break;
                }
                // Если ввод не является корректным положительным числом.
                else
                {
                    // Выводим сообщение об ошибке.
                    Console.WriteLine("Ungültige Eingabe. Bitte geben Sie einen positiven Kalorienwert ein.  ");
                    // Цикл продолжится, запрашивая ввод снова.
                }
            }
            // Возвращаем кортеж с названием продукта и его калорийностью.
            return Tuple.Create(foodName, calories);
        }

        // Метод для сохранения списка съеденных продуктов и их калорийности в файл.
        // Принимает список кортежей (название продукта, калории).
        public static void SaveFoodLogToFile(List<Tuple<string, int>> foodLog)
        {
            // Используем конструкцию 'using' для StreamWriter. Это гарантирует, что файл будет корректно закрыт
            // и ресурсы освобождены, даже если возникнет ошибка во время записи.
            // StreamWriter создает или перезаписывает файл с указанным именем (LogFileName).
            using (StreamWriter writer = new StreamWriter(LogFileName, false)) 
            // false означает перезапись файла, true - добавление в конец
            {
                // Получаем текущую дату и время для записи в лог.
                writer.WriteLine($"Kalorienlog für {DateTime.Now}");
                // Записываем разделительную строку для лучшей читаемости.
                writer.WriteLine("--------------------");
                // Проходим по каждому элементу (кортежу) в списке foodLog.
                foreach (var item in foodLog)
                {
                    // Записываем в файл строку в формате "Название продукта: Калории ккал".
                    writer.WriteLine($"{item.Item1}: {item.Item2} kKal");
                }
                // Записываем разделительную строку в конце.
                 writer.WriteLine("--------------------");
            }
            // Выводим сообщение пользователю о том, что данные сохранены в файл.
            Console.WriteLine($"\ndie Daten wurden in Datei {LogFileName} erfolgreicht gespeichert");
        }

        // Метод для вычисления общей суммы калорий из списка съеденных продуктов.
        // Принимает список кортежей (название продукта, калории).
        public static int CalculateTotalCalories(List<Tuple<string, int>> foodLog)
        {
            // Инициализируем переменную для хранения общей суммы калорий нулем.
            int totalCalories = 0;
            // Проходим по каждому элементу (кортежу) в списке foodLog.
            foreach (var item in foodLog)
            {
                // Добавляем калорийность текущего продукта (второй элемент кортежа, item.Item2) к общей сумме.
                totalCalories += item.Item2;
            }
            // Возвращаем вычисленную общую сумму калорий.
            return totalCalories;
        }

        // Метод для вывода итогового результата: сравнение потребленных калорий с дневным лимитом.
        public static void DisplayResult(int totalCalories, int dailyLimit)
        {
            // Выводим общее количество потребленных калорий.
            Console.WriteLine($"\nInsgesamt verbrauchte Kalorien pro Tag: {totalCalories} kKal");
            // Выводим установленный дневной лимит калорий.
            Console.WriteLine($"Ihr tägliches Limit: {dailyLimit} kKal");

            // Сравниваем общее количество калорий с дневным лимитом.
            if (totalCalories <= dailyLimit)
            {
                // Если потреблено меньше или равно лимиту, выводим поздравительное сообщение.
                Console.WriteLine("Glückwunsch! Sie haben Ihr tägliches Kalorienlimit eingehalten!");
            }
            // Если потреблено больше лимита.
            else
            {
                // Вычисляем, на сколько калорий превышен лимит.
                int excessCalories = totalCalories - dailyLimit;
                // Выводим сообщение о превышении лимита и количество избыточных калорий.
                Console.WriteLine($"Sie haben Ihr Tageslimit um {excessCalories} kKal überschritten!");
                Console.WriteLine("Sie sollten entweder weniger essen oder mehr Sport treiben!");
            }
        }
    }
}