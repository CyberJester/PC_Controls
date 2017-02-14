using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PC_Controls
{
	public class PCMouseManager
	{
		private static MouseState mCurrentMouseState, mPreviousMouseState;
		private static Texture2D mCursorTexture;
		private static Point mGraphicsOrigin;
		private static Point mCursorSize;
		private static Rectangle mSourceRect;
		private static bool mIsVisable;
		private static int[ ] mHoldDuration; //amount of time the buttons must be held to fire the event
		private static int mPressLength; //the amount of time required for a MouseButtonPressed event
		private static int mScrollThreshhold; //The amount of scrolls on the middle wheel for a scroll event
		private static int mDoubleClickTime; //The maxiumum time between clicks for a double click event
		private static int mTimeSinceLastLeftClickPress; //The time since the last left mouse button click (used for double clicking)
		private static int mTimeSinceLastRightClickPress; //The time since the last right mouse button click (used for Right double clicking) FOR FUTURE USE.

		#region Getter/Setter methods
		/// <summary>
		/// The current MouseState of the mouse (as of last time the MouseManager.Update was called)
		/// </summary>
		public static MouseState CurrentMouseState
		{
			get
			{
				return mCurrentMouseState;
			}
		}
		/// <summary>
		/// The MouseState from the previous update call (null before first update call)
		/// </summary>
		public static MouseState PreviousMouseState
		{
			get
			{
				return mPreviousMouseState;
			}
		}
		/// <summary>
		/// The position of the mouse on the screen with 0,0 the top left hand corner
		/// </summary>
		public static Point MousePosition
		{
			get
			{
				return new Point ( mCurrentMouseState.X, mCurrentMouseState.Y );
			}
		}
		/// <summary>
		/// The distance between the previuos mouse position and the current mouse position in pixels
		/// </summary>
		public static Vector2 DeltaMouse
		{
			get
			{
				float xDist = mCurrentMouseState.X - mPreviousMouseState.X;
				float yDist = mCurrentMouseState.Y - mPreviousMouseState.Y;
				return new Vector2 ( xDist, yDist );
			}
		}
		/// <summary>
		/// The amount of miliseconds a button must be depressed for the ButtonHeld events to be triggered (by default 500)
		/// </summary>
		public static int PressLength
		{
			get
			{
				return mPressLength;
			}
			set
			{
				mPressLength = value;
			}
		}
		/// <summary>
		/// The amount of clicks on the scroll wheel to fire the scroll event (by default 5)
		/// </summary>
		public static int ScrollThreshold
		{
			get
			{
				return mScrollThreshhold;
			}
			set
			{
				mScrollThreshhold = value;
			}
		}
		/// <summary>
		/// The amount of miliseconds allowed between two clicks for it to count as double click (default 500)
		/// </summary>
		public static int DoubleClickTime
		{
			get
			{
				return mDoubleClickTime;
			}
			set
			{
				if ( value > 0 )
				{
					mDoubleClickTime = value;
					mTimeSinceLastLeftClickPress = mDoubleClickTime + 1;
				}
				else
				{
					mDoubleClickTime = -1;
				}
			}
		}
		/// <summary>
		/// The amount of time in miliseconds since the last click press
		/// </summary>
		public static int TimeSinceLastClickPress
		{
			get
			{
				return mTimeSinceLastLeftClickPress;
			}
		}
		public static Point GraphicsOrigin
		{
			get
			{
				return mGraphicsOrigin;
			}
			set
			{
				mGraphicsOrigin = value;
			}
		}
		#endregion

		#region Mouse Events
		/// <summary>
		/// Occurs when the mouse moves between frames
		/// </summary>
		public static event EventHandler MouseMove = delegate { };
		/// <summary>
		/// Occurs when the left mouse buttons is first pressed
		/// </summary>
		public static event EventHandler LeftMousePress = delegate { };
		/// <summary>
		/// Occurs when the left mouse button is released
		/// </summary>
		public static event EventHandler LeftMouseRelease = delegate { };
		/// <summary>
		/// Occurs when the left mouse button has been held for more than Duration miliseconds
		/// </summary>
		public static event EventHandler LeftMouseHeld = delegate { };
		/// <summary>
		/// Occurs when the left mouse button is double clicked (two click presses within 
		/// DoubleClickTime miliseconds of each other)
		/// </summary>
		public static event EventHandler LeftMouseDoubleClick = delegate { };
		/// <summary>
		/// Occurs when the middle mouse button was pressed
		/// </summary>
		public static event EventHandler MiddleMousePress = delegate { };
		/// <summary>
		/// Occurs when the middle mouse button was relased
		/// </summary>
		public static event EventHandler MiddleMouseRelease = delegate { };
		/// <summary>
		/// Occurs when the middle mouse button has been held for more than PressLenght miliseconds
		/// </summary>
		public static event EventHandler MiddleMouseHeld = delegate { };
		/// <summary>
		/// Occurs when the right mouse button is pressed
		/// </summary>
		public static event EventHandler RightMousePress = delegate { };
		/// <summary>
		/// Occurs when the right mouse button was relased
		/// </summary>
		public static event EventHandler RightMouseRelease = delegate { };
		/// <summary>
		/// Occurs when the right mouse button has been held for more than PressLenght miliseconds
		/// </summary>
		public static event EventHandler RightMouseHeld = delegate { };
		/// <summary>
		/// Occurs when the right mouse button is double clicked (two click presses within 
		/// DoubleClickTime miliseconds of each other)
		/// </summary>
		public static event EventHandler RightMouseDoubleClick = delegate { };
		/// <summary>
		/// Occurs when the scroll wheel is scrolled more than than scroll threshhold number of clicks
		/// </summary>
		public static event EventHandler ScrollWheelScrolled = delegate { };

		#endregion

		/// <summary>
		/// Initalise the MouseManager, must be called before calling Update
		/// </summary>
		public static void Init ( )
		{
			mCurrentMouseState = Mouse.GetState ( );
			//Set defaults for properties
			mDoubleClickTime = 500;
			mScrollThreshhold = 5;
			mPressLength = 500;
			mHoldDuration = new int[ ( int ) MouseButtons.RightButton + 1 ];
			for ( int i = 0; i < ( int ) MouseButtons.RightButton + 1; i++ )
			{
				//To insure first clicks don't count as double clicks
				mHoldDuration[ i ] = mTimeSinceLastLeftClickPress = mDoubleClickTime + 1;
				mTimeSinceLastLeftClickPress = mDoubleClickTime + 1;
			}
		}

		/// <summary> Update
		/// Update the MouseManager with the latest information about the current mouse state. 
		/// This fires the events
		/// </summary>
		/// <param name="gameTime">The local GameTime reference - used for calculating how long button 
		/// has been pressed</param>
		public static void Update ( GameTime gameTime )
		{
			mPreviousMouseState = mCurrentMouseState;
			mCurrentMouseState = Mouse.GetState ( );

			if ( mPreviousMouseState.X != mCurrentMouseState.X || mCurrentMouseState.Y != mPreviousMouseState.Y )
			{
				MouseMove ( null, new EventArgs ( ) );
			}
			mPressLength += gameTime.ElapsedGameTime.Milliseconds;
			#region LMB Events
			//Update time since last pressed  (double click goes from pressed not released)

			if ( mCurrentMouseState.LeftButton == ButtonState.Pressed )
			{
				if ( mPreviousMouseState.LeftButton == ButtonState.Released )
				{
					if ( mPressLength <= mDoubleClickTime )
					{
						LeftMouseDoubleClick ( null, new EventArgs ( ) );
					}
					else
					{
						LeftMousePress ( null, new EventArgs ( ) );
						mPressLength = 0;
					}
				}
				else
				{
					if ( mPressLength > mHoldDuration[ ( int ) MouseButtons.LeftButton ] )
					{
						LeftMouseHeld ( null, new EventArgs ( ) );
					}
				}
			}
			else if ( mPreviousMouseState.LeftButton == ButtonState.Pressed )
			{
				LeftMouseRelease ( null, new EventArgs ( ) );
			}
			#endregion

			#region MMB Events
			if ( mCurrentMouseState.MiddleButton == ButtonState.Pressed )
			{
				if ( mPreviousMouseState.MiddleButton == ButtonState.Released )
				{
					MiddleMousePress ( null, new EventArgs ( ) );
				}
				if ( mPressLength > mHoldDuration[ ( int ) MouseButtons.MiddleButton ] )
				{
					MiddleMouseHeld ( null, new EventArgs ( ) );
				}
			}
			else if ( mPreviousMouseState.MiddleButton == ButtonState.Pressed )
			{
				MiddleMouseRelease ( null, new EventArgs ( ) );
			}
			#endregion

			#region RMB Events
			if ( mCurrentMouseState.RightButton == ButtonState.Pressed )
			{
				if ( mPreviousMouseState.RightButton == ButtonState.Released )
				{
					if ( mPressLength <= mDoubleClickTime )
					{
						RightMouseDoubleClick ( null, new EventArgs ( ) );
					}
					else
					{
						RightMousePress ( null, new EventArgs ( ) );
						mPressLength = 0;
					}
				}
				else
				{
					if ( mPressLength > mHoldDuration[ ( int ) MouseButtons.RightButton ] )
					{
						RightMouseHeld ( null, new EventArgs ( ) );
					}
				}
			}
			else if ( mPreviousMouseState.RightButton == ButtonState.Pressed )
			{
				RightMouseRelease ( null, new EventArgs ( ) );
			}
			#endregion

			if ( Math.Abs ( mCurrentMouseState.ScrollWheelValue - mPreviousMouseState.ScrollWheelValue ) >= mScrollThreshhold )
			{
				ScrollWheelScrolled ( null, new EventArgs ( ) );
			}

		}
		public static void CreateCursor ( Texture2D iTexture, Point iGraphicsOrigin, Point iSize )
		{
			mCursorTexture = iTexture;
			mGraphicsOrigin = iGraphicsOrigin;
			mCursorSize = iSize;
			mSourceRect = new Rectangle (mGraphicsOrigin.X,mGraphicsOrigin.Y,mCursorSize.X,mCursorSize.Y);

		}
		public static void ShowCursor ( )
		{
			mIsVisable = true;
		}
		public static void HideCursor ( )
		{
			mIsVisable = false;
		}
		public static void DrawCursor ( SpriteBatch spriteBatch )
		{
			if ( mIsVisable )
			{
				Rectangle dRect = new Rectangle
					( mCurrentMouseState.X, mCurrentMouseState.Y, mCursorSize.X, mCursorSize.Y );
				mSourceRect = new Rectangle 
					( mGraphicsOrigin.X, mGraphicsOrigin.Y, mCursorSize.X, mCursorSize.Y );
				spriteBatch.Draw ( mCursorTexture, dRect, mSourceRect, Color.White );
			}
		}
	}

	/// <summary>
	/// The differnt buttons on the mouse
	/// </summary>
	public enum MouseButtons
	{
		/// <summary>
		/// The Left button
		/// </summary>
		LeftButton = 0,
		/// <summary>
		/// The Middle button
		/// </summary>
		MiddleButton,
		/// <summary>
		/// The Right button
		/// </summary>
		RightButton,
		/// <summary>
		/// The ScrollWheel
		/// </summary>
		ScrollWheel
	};
}