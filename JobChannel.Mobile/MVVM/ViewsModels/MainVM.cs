using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobChannel.Mobile.MVVM.ViewsModels
{
    public class MainVM : ViewModelBase
    {
        public string Titre { get; set; }

        public void Test()
        {
            string t = Titre switch
            {
                "" => Titre,
                _ => "Test",
            };
        }
    }
}
