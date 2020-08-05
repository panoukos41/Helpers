using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace App
{
    public static class PageKeys
    {
        public const string HomePage = "HomePage";
        public const string FilesPage = "FilesPage";
        public const string GesturePage = "GesturePage";
        public const string TabsPage = "TabsPage";
        public const string ThemePage = "ThemePage";

        public static IEnumerable<string> GetValues() =>
            typeof(PageKeys)
            .GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(x => x.IsLiteral)
            .Select(x => (string)x.GetValue(null));
    }
}