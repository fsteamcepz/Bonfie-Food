using System;
using System.Threading;
using System.Globalization;

namespace BonfieFood
{
    public class Language
    {
        public static event Action<string> OnLanguageChanged;

        public static void ChangeLanguage(string cultureCode)
        {
            // встановлюємо глобальну культуру на основі мови
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);

            // зберігаємо вибір мови у налаштуваннях
            Properties.Settings.Default.Language = cultureCode;
            Properties.Settings.Default.Save();

            OnLanguageChanged?.Invoke(cultureCode);
        }
    }
}
