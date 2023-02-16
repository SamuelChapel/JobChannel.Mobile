using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace JobChannel.Mobile.Extensions
{
    public static class MessageExt
    {
        public static string Message(this string key)
        {
            return ResourceLoader.GetForCurrentView("Messages").GetString(key);
        }
    }
}
