using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Установка заголовка консоли
        Console.Title = "IP Checker";

        // Настройка цветовой схемы консоли
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();

        // Вывод заголовка
        await PrintHeaderAsync();

        // Вывод сообщения ожидания с анимацией
        Console.ForegroundColor = ConsoleColor.Gray;
        await PrintTextWithEffectAsync("Please wait 5 seconds to find out your IP...", 30); // Умеренная анимация

        // Задержка в 5 секунд перед продолжением
        await Task.Delay(5000);

        // Очистка консоли после задержки
        Console.Clear();

        // Вывод сообщения ожидания
        Console.ForegroundColor = ConsoleColor.Gray;
        await PrintTextWithEffectAsync("Fetching your IP address, please wait...", 30); // Умеренная анимация

        // Убираем задержку здесь
        // Очищаем строку сообщения
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(new string(' ', Console.WindowWidth)); // Очистка строки
        Console.SetCursorPosition(0, Console.CursorTop);

        try
        {
            string externalIP = await GetExternalIPAddressAsync();
            Console.ForegroundColor = ConsoleColor.Green;

            // Вывод текста с анимацией
            await PrintTextWithEffectAsync("\n" + new string('=', 60), 30); // Умеренная анимация
            Console.WriteLine("                                                        ");
            await PrintTextWithEffectAsync($"Your external IP Address is: {externalIP}", 30); // Умеренная анимация
            await PrintTextWithEffectAsync("\n" + new string('=', 60) + "\n", 30); // Умеренная анимация

            // Вывод текстового блока с анимацией и сменой цветов
            await PrintTextWithColorEffectAsync(
                "╔═══╗╔═══╗╔══╗╔═══╗╔═══╗╔══╗╔╗  ╔╗   ╔══╗ ╔╗╔╗   ╔═══╗╔══╗╔══╗╔═══╗╔══╗╔╗╔╗╔╗ ╔╗\r\n" +
                "║╔═╗║║╔═╗║║╔╗║║╔══╝║╔═╗║║╔╗║║║  ║║   ║╔╗║ ║║║║   ║╔══╝╚═╗║║╔═╝║╔═╗║║╔═╝║║║║║╚═╝║\r\n" +
                "║╚═╝║║╚═╝║║║║║║║╔═╗║╚═╝║║╚╝║║╚╗╔╝║   ║╚╝╚╗║╚╝║   ║╚══╗  ║╚╝║  ║╚═╝║║╚═╗║║║║║╔╗ ║\r\n" +
                "║╔══╝║╔╗╔╝║║║║║║╚╗║║╔╗╔╝║╔╗║║╔╗╔╗║   ║╔═╗║╚═╗║   ║╔══╝  ║╔╗║  ║╔══╝║╔═╝║║║║║║╚╗║\r\n" +
                "║║   ║║║║ ║╚╝║║╚═╝║║║║║ ║║║║║║╚╝║║   ║╚═╝║ ╔╝║   ║╚══╗╔═╝║║╚═╗║║   ║║  ║╚╝║║║ ║║\r\n" +
                "╚╝   ╚╝╚╝ ╚══╝╚═══╝╚╝╚╝ ╚╝╚╝╚╝  ╚╝   ╚═══╝ ╚═╝   ╚═══╝╚══╝╚══╝╚╝   ╚╝  ╚══╝╚╝ ╚╝\r\n", 10); // Умеренная анимация

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(); // Ожидание нажатия любой клавиши перед завершением программы
        }
        catch (HttpRequestException httpRequestException)
        {
            PrintError($"Error fetching IP address: {httpRequestException.Message}");
        }
        catch (Exception ex)
        {
            PrintError($"An unexpected error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Gets the external IP address by making an HTTP request to an external service.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the external IP address.</returns>
    private static async Task<string> GetExternalIPAddressAsync()
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync("https://api.ipify.org?format=text");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// Prints the header of the console application with a smooth effect.
    /// </summary>
    private static async Task PrintHeaderAsync()
    {
        // Сначала просто выводим заголовок без анимации
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("****************************************************");
        Console.WriteLine(" ╔══╗╔═══╗   ╔══╗╔╗╔╗╔═══╗╔══╗╔╗╔══╗╔═══╗╔═══╗   ");
        Console.WriteLine(" ╚╗╔╝║╔═╗║   ║╔═╝║║║║║╔══╝║╔═╝║║║╔═╝║╔══╝║╔═╗║   ");
        Console.WriteLine("  ║║ ║╚═╝║   ║║  ║╚╝║║╚══╗║║  ║╚╝║  ║╚══╗║╚═╝║  ");
        Console.WriteLine("  ║║ ║╔══╝   ║║  ║╔╗║║╔══╝║║  ║╔╗║  ║╔══╝║╔╗╔╝ ");
        Console.WriteLine(" ╔╝╚╗║║      ║╚═╗║║║║║╚══╗║╚═╗║║║╚═╗║╚══╗║║║║");
        Console.WriteLine(" ╚══╝╚╝      ╚══╝╚╝╚╝╚═══╝╚══╝╚╝╚══╝╚═══╝╚╝╚╝ ");
        Console.WriteLine("****************************************************");
        Console.WriteLine("                                                    ");
        Console.WriteLine("                                                    ");
    }

    /// <summary>
    /// Prints text to the console with a typing effect.
    /// </summary>
    /// <param name="text">The text to print.</param>
    /// <param name="delay">The delay between characters in milliseconds.</param>
    private static async Task PrintTextWithEffectAsync(string text, int delay)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            await Task.Delay(delay); // Задержка для более медленного появления текста
        }
        Console.WriteLine(); // Перевод строки после завершения текста
    }

    /// <summary>
    /// Prints text with color effects to the console with a changing color effect.
    /// </summary>
    /// <param name="text">The text to print.</param>
    /// <param name="delay">The delay between color changes in milliseconds.</param>
    private static async Task PrintTextWithColorEffectAsync(string text, int delay)
    {
        var colors = Enum.GetValues(typeof(ConsoleColor));
        int colorCount = colors.Length;
        int currentColorIndex = 0;
        string[] lines = text.Split('\n');

        foreach (string line in lines)
        {
            foreach (char c in line)
            {
                Console.ForegroundColor = (ConsoleColor)colors.GetValue(currentColorIndex);
                Console.Write(c);
                await Task.Delay(delay); // Задержка для более медленного появления текста

                // Переход к следующему цвету
                currentColorIndex = (currentColorIndex + 1) % colorCount;
            }
            Console.WriteLine(); // Перевод строки после завершения текста
        }

        // Восстановление стандартного цвета после завершения текста
        Console.ForegroundColor = ConsoleColor.White;
    }

    /// <summary>
    /// Prints colorful text to the console with a changing color effect.
    /// </summary>
    /// <param name="text">The text to print.</param>
    /// <param name="delay">The delay between color changes in milliseconds.</param>
    private static async Task PrintColorfulTextAsync(string text, int delay)
    {
        var colors = Enum.GetValues(typeof(ConsoleColor));
        int colorCount = colors.Length;
        int currentColorIndex = 0;

        foreach (char c in text)
        {
            Console.ForegroundColor = (ConsoleColor)colors.GetValue(currentColorIndex);
            Console.Write(c);

            currentColorIndex = (currentColorIndex + 1) % colorCount; // Переход к следующему цвету
            await Task.Delay(delay); // Задержка для смены цвета
        }

        // Восстановление стандартного цвета после завершения текста
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
    }

    /// <summary>
    /// Prints an error message in red color.
    /// </summary>
    /// <param name="message">The error message to print.</param>
    private static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n" + new string('!', 60));
        Console.WriteLine(message);
        Console.WriteLine(new string('!', 60) + "\n");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
