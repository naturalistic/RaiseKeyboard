using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;

namespace RaiseKeyboard
{
	public class DemoPage : ContentPage
	{
		ListView listView;
		private ObservableCollection<string> items = new ObservableCollection<string> { "Apple", "Banana", "Cantaloupe", "Date", "Elderberry", "Fig", "Grape", "Honeydew melon" };
		private Entry textEntry;
		private double bottomOffset;
		public DemoPage ()
		{
			this.Padding = 0;
			var mainStack = new StackLayout () {
				Padding = new Thickness(0,20,0,0),
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
			};

			listView = new ListView () {};
			listView.ItemsSource = items;
			mainStack.Children.Add (listView);

			textEntry = new Entry () {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Placeholder = "Type something...",
			};

			var textStack = new StackLayout () {
				Orientation = StackOrientation.Horizontal,
				Padding = 10,
				BackgroundColor = Color.White,
			};
			textStack.Children.Add (textEntry);

			var textButton = new Button () {
				Text = "Post",
				FontAttributes = FontAttributes.Bold,
				FontSize = 16,
				HorizontalOptions = LayoutOptions.End,
			};

			textButton.Clicked += (object sender, EventArgs e) => {
				System.Diagnostics.Debug.WriteLine ("listview height: " + listView.Height);
				items.Add(textEntry.Text);
				textEntry.Text = string.Empty;
				listView.ScrollTo(items.Last(), ScrollToPosition.MakeVisible, true);
			};
			textStack.Children.Add (textButton);
			mainStack.Children.Add (textStack);

			var spacer = new BoxView () { 
				HorizontalOptions = LayoutOptions.FillAndExpand,
				HeightRequest = 0,
			};
			mainStack.Children.Add (spacer);

			// Only required for iOS
			KeyboardHelper.KeyboardChanged += (sender, e) => {
				bottomOffset = mainStack.Bounds.Bottom - textStack.Bounds.Bottom;	// This offset allows us to only raise the stack by the amount required to stay above the keyboard. 
				textStack.TranslationY -= e.Visible ? e.Height - bottomOffset : bottomOffset - e.Height;	// The textStack is translated up, and then returned to original position.
			};
			this.Content = mainStack;
		}
	}
}

