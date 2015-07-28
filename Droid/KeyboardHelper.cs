using System;
using RaiseKeyboard.Droid;

[assembly: Xamarin.Forms.Dependency (typeof (KeyboardHelper))]
namespace RaiseKeyboard.Droid
{
	// Android handles keyboard adjustment automatically, so this event is not raised
	public class KeyboardHelper : IKeyboardHelper
	{
		// Never used or raised
		public event EventHandler<KeyboardHelperEventArgs> KeyboardChanged;
	}
}

