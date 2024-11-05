using Microsoft.Maui.Platform;

namespace FinancialApp.Libs.Utils;

public class KeyboardBugFixAndroid
{
    public static void HideKeyboard()
    {
#if ANDROID
        if (Platform.CurrentActivity.CurrentFocus is not null)
        {
            Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
        }
#endif
    }
}