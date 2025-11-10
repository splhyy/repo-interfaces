using System;

abstract class TextFormatter
{
    public abstract string Apply(string text);
}

sealed class UpperCaseFormatter : TextFormatter
{
    public override string Apply(string text) => text.ToUpperInvariant();
}

sealed class LowerCaseFormatter : TextFormatter
{
    public override string Apply(string text) => text.ToLowerInvariant();
}

sealed class TitleCaseFormatter : TextFormatter
{
    public override string Apply(string text) => 
        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== FASE 2 - OO SEM INTERFACE ===");
        
        TextFormatter upper = new UpperCaseFormatter();
        TextFormatter lower = new LowerCaseFormatter();
        TextFormatter title = new TitleCaseFormatter();
        
        Console.WriteLine(upper.Apply("hello world"));
        Console.WriteLine(lower.Apply("HELLO WORLD"));
        Console.WriteLine(title.Apply("hello world"));
    }
}