namespace CollectApple.ViewModels
{
    public class LanguageViewModel
    {
        public string Name { get; }
        public string Code { get; }

        public LanguageViewModel(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
}
