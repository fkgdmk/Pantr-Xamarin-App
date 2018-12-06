using Pantr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pantr
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditPost : ContentPage
	{
        PostViewModelCopy post;
		public EditPost (PostViewModelCopy post)
		{
            this.post = post;
			InitializeComponent ();
		}
	}
}