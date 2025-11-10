using System;

class Program
{
    static string FormatText(string text, string mode)
    {
        return mode switch
        {
            "upper" => text.ToUpperInvariant(),
            "lower" => text.ToLowerInvariant(),
            "title" => System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text),
            _ => text
        };
    }

    static void Main()
    {
        // Testes da funcionalidade
        Console.WriteLine("=== FASE 1 - PROCEDURAL ===");
        Console.WriteLine(FormatText("hello world", "upper"));
        Console.WriteLine(FormatText("HELLO WORLD", "lower"));
        Console.WriteLine(FormatText("hello world", "title"));
        Console.WriteLine(FormatText("hello world", "unknown"));
    }
}