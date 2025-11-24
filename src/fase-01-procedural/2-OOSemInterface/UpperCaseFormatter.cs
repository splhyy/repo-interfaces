namespace Fase01.OOSemInterface
{
    public class UpperCaseFormatter : TextFormatter
    {
        public override string Format(string text) => text.ToUpperInvariant();
    }
}
