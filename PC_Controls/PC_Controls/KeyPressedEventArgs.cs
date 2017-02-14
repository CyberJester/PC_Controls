using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace PC_Controls
{
	public class KeyPressedEventArgs : EventArgs
	{
		/// <summary>
		/// The key that has been pressed
		/// </summary>
		public Keys KeyPressed;

		public KeyPressedEventArgs ( Keys lKeyPressed )
		{
			KeyPressed = lKeyPressed;
		}
	}

	public class KeyHeldEventArgs : EventArgs
	{
		/// <summary>
		/// The key that has been pressed
		/// </summary>
		public Keys KeyHeld;
		/// <summary>
		/// The duration in miliseconds the key has been depressed :(
		/// </summary>
		public int KeyHeldDuration;

		public KeyHeldEventArgs ( Keys lKeyHeld, int lKeyHeldDuration )
		{
			KeyHeld = lKeyHeld;
			KeyHeldDuration = lKeyHeldDuration;
		}
	}
}
