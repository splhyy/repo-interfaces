using System;
using System.Collections.Generic;

public interface ITextFormatter
{
    string Apply(string text);
}

public sealed class UpperCaseFormatter : ITextFormatter
{
    public string Apply(string text) => text.ToUpperInvariant();
}

public sealed class LowerCaseFormatter : ITextFormatter
{
    public string Apply(string text) => text.ToLowerInvariant();
}

public sealed class TitleCaseFormatter : ITextFormatter
{
    public string Apply(string text) => 
        System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
}

public static class TextRenderer
{
    public static string Render(ITextFormatter formatter, string text)
    {
        return formatter.Apply(text);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== FASE 3 - COM INTERFACES ===");
        
        ITextFormatter upper = new UpperCaseFormatter();
        ITextFormatter lower = new LowerCaseFormatter();
        ITextFormatter title = new TitleCaseFormatter();
        
        // Uso com injeção por parâmetro
        Console.WriteLine(TextRenderer.Render(upper, "hello world"));
        Console.WriteLine(TextRenderer.Render(lower, "HELLO WORLD"));
        Console.WriteLine(TextRenderer.Render(title, "hello world"));
        
        // Catálogo de formatters
        var formatters = new Dictionary<string, ITextFormatter>(StringComparer.OrdinalIgnoreCase)
        {
            ["upper"] = new UpperCaseFormatter(),
            ["lower"] = new LowerCaseFormatter(),
            ["title"] = new TitleCaseFormatter()
        };
        
        string RenderWithMode(string mode, string text) => formatters[mode].Apply(text);
        
        Console.WriteLine("\nUsando catálogo:");
        Console.WriteLine(RenderWithMode("upper", "teste catálogo"));
        Console.WriteLine(RenderWithMode("LOWER", "TESTE CATÁLOGO"));
    }
}