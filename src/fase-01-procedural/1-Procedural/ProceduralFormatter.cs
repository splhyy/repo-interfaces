namespace Fase01.Procedural
{
    public static class ProceduralFormatter
    {
        public static string FormatText(string text, string mode)
        {
            return mode switch
            {
                "upper" => text.ToUpperInvariant(),
                "lower" => text.ToLowerInvariant(),
                "title" => ToTitleCase(text),
                _ => text
            };
        }

        private static string ToTitleCase(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;
            
            var words = text.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                }
            }
            return string.Join(" ", words);
        }
    }
}
