using System;
using System.Reflection;
using DesktopToast;

namespace ProjectEPIC
{
    class Notifier
    {

        /// <summary>
        /// 创建一个Toast Notification并显示
        /// </summary>
        /// <param name="Body">第一行文本</param>
        /// <param name="BodyExtra">第二行文本</param>
        public static void CreateToastNotification(string Body, string BodyExtra = "")
        {
            var request = new ToastRequest
            {
                ToastHeadline = "EPIC",
                ToastBody = Body,
                ToastBodyExtra = BodyExtra,
                ToastAudio = ToastAudio.Default,
                ShortcutFileName = "EPIC.lnk",
                ShortcutTargetFilePath = Assembly.GetExecutingAssembly().Location,
                AppId = "pw.baka.epic",
            };

            var result = ToastManager.ShowAsync(request);

        }
    }
}
