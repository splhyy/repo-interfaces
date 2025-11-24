namespace Fase01.OOSemInterface
{
    public class LowerCaseFormatter : TextFormatter
    {
        public override string Format(string text) => text.ToLowerInvariant();
    }
}
