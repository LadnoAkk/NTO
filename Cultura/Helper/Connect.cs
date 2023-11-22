using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Cultura.data;
using MsBox.Avalonia;
using Cultura.Pages;
using Microsoft.CodeAnalysis.CSharp;

namespace Cultura.Helper
{
    public static class Connect
    {
        public static CulturnaBasaContext context = new CulturnaBasaContext();
        public static Window MainMainWindow = null;

        public static Applications OrigApp;
        public static ReApplications OrigReApp;

        public static Events Ev1;
        public static Events Ev2;
        public static EventTypes EvType1;
        public static EventTypes EvType2;

        public async static Task<MsBox.Avalonia.Enums.ButtonResult> ShowQuestion(string message)
        {
            return await MessageBoxManager.GetMessageBoxStandard("Вопрос", message,
                MsBox.Avalonia.Enums.ButtonEnum.OkCancel,
                MsBox.Avalonia.Enums.Icon.Question,
                WindowStartupLocation.CenterScreen).ShowAsync();
        }
    }
}
