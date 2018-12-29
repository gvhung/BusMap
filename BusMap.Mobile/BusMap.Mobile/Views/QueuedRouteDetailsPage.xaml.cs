using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Iconize;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusMap.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QueuedRouteDetailsPage : IconTabbedPage
    {
		public QueuedRouteDetailsPage ()
		{
			InitializeComponent ();
		}

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = sender as ListView;
            listView.SelectedItem = null;
        }
    }
}