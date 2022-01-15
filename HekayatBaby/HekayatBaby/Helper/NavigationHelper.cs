using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HekayatBaby.Helper
{
    public class NavigationHelper
    {
        public Task NavigateTo(Page page)
        {

            return Application.Current.MainPage.Navigation.PushAsync(page);

        }
        public Task NaviagteToModel(Page page)
        {
            return Application.Current.MainPage.Navigation.PushModalAsync(page);
        }
        public Task NavigateBack()
        {
            return Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}
