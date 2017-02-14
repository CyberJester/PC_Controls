using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace PC_Controls
{
	public class NumericUpDn : PC_Control
	{
		public enum CursorStyle
		{
			Vertical,
			Horizontal,
			Replace
		}
		private Button mUpButton, mDnButton;
		private int mButtonWidth;
		private TextBox mTextField;
		private bool mHasFocus;
		//private bool mIsUpper = false;
		private bool mIsHighlighted = false;
		private Color mHighlightColor;
		//private int mHighlightSize;
		private Texture2D mTexture;
		private TextCursor mTextCursor;
		private Point mGraphicsOrigin;
		private SpriteFont mFont;
		private Point mFontSize;
		private Color mFontColor;
		private int mPad;
		private string mTextBoxInputString;
		private int mValue, mMinValue, mMaxValue;
		private int mMaxStringLength;
		private int tPointer;
		private Rectangle mDrawRect = new Rectangle ( );
		private Rectangle mLocation;
		private Rectangle mSourceRect;
		private Vector2 mTextPosition = new Vector2 ( );
		private KeyboardState mCurrentKeyboardState, mPreviousKeyboardState;
		//private int mBlink;
		//private int BlinkTime = 30;
        private bool mIsDisabled = false;
        private Rectangle mDrawRectangle;
        private Point[] mStartPoints;
		private CursorStyle mCursorStyle;
		private CursorStyle mOldCursorStyle;
		private ControlStateArgs ControlState = new ControlStateArgs ();
		private string mLabel;
		private Point mSize;
		private bool mIsHovered;
		private MouseState mouse;

		#region EventHandlers
		/// <summary> HotKeyPressed Event Handler
		/// </summary>
		public override void HotKeyPressed() { }
		/// <summary> HotKeyReleased Event Handler
		/// </summary>
		public override void HotKeyReleased() { }
		/// <summary> MouseHover Event Handler.
		/// </summary>
		public override void MouseHover() 
		{
			mouse = PCMouseManager.CurrentMouseState;
			Point mousePos = new Point(mouse.X, mouse.Y);
			if (mUpButton.CheckForCollision(mousePos))
			{
				if (mUpButton.State == PC_Control.PCControlState.released)
				{
					mUpButton.MouseHover();
				}
			}
			else
				if (mUpButton.State == PCControlState.hover)
				{
					mUpButton.MouseUnHover();
				}
			if (mDnButton.CheckForCollision(mousePos))
			{
				if (mDnButton.State == PC_Control.PCControlState.released)
				{
					mDnButton.MouseHover();
				}
			}
			else
				if (mDnButton.State == PCControlState.hover)
				{
					mDnButton.MouseUnHover();
					ControlState.NewControlState = PCControlState.released;
					mUpButton.ChangeState(ControlState);
				}
		}
		/// <summary>  MouseUnHover Event Handler.
		/// </summary>
		public override void MouseUnHover() 
		{
			if (mUpButton.State == PCControlState.hover)
			{
				mUpButton.MouseUnHover();
			}
			if (mDnButton.State == PCControlState.hover)
			{
				mDnButton.MouseUnHover();
			}
		}
		/// <summary> LeftMousePress Event Handler.
		/// </summary>
		public override void LeftMousePress() 
		{
			mouse = PCMouseManager.CurrentMouseState;
			Point mousePos = new Point(mouse.X, mouse.Y);
			if (mUpButton.CheckForCollision(mousePos))
			{
				mUpButton.LeftMousePress();
				mValue++;
				if (mValue > mMaxValue)
					mValue = mMaxValue;
				mTextField.Text = mValue.ToString();
			}
			if (mDnButton.CheckForCollision(mousePos))
			{
				mDnButton.LeftMousePress();
				mValue--;
				if (mValue < mMinValue)
					mValue = mMinValue;
				mTextField.Text = mValue.ToString();
			}
			if(mTextField.CheckForCollision(mousePos))
			{
				mTextField.Click(mouse);
			}
		}
		/// <summary> LeftMouseRelease Event Handler.
		/// </summary>
		public override void LeftMouseRelease() { }
		/// <summary> IsReleased Event Handler.
		/// </summary>
		public override void IsReleased() 
		{
			if (mUpButton.State == PCControlState.pressed)
				mUpButton.IsReleased();
			if (mDnButton.State == PCControlState.pressed)
				mDnButton.IsReleased();
		}
		/// <summary> MiddleMousePress Event Handler.
		/// </summary>
		public override void MiddleMousePress() 
		{
			mouse = PCMouseManager.CurrentMouseState;
			Point mousePos = new Point(mouse.X, mouse.Y);
			if (mUpButton.CheckForCollision(mousePos))
			{
				mUpButton.LeftMousePress();
				mValue++;
				if (mValue > mMaxValue)
					mValue = mMaxValue;
				mTextField.Text = mValue.ToString();
			}
			if (mDnButton.CheckForCollision(mousePos))
			{
				mDnButton.LeftMousePress();
				mValue--;
				if (mValue < mMinValue)
					mValue = mMinValue;
				mTextField.Text = mValue.ToString();
			}
		}
		/// <summary> MiddleMouseRelease Event Handler.
		/// </summary>
		public override void MiddleMouseRelease() { }
		/// <summary> RightMousePress Event Handler.
		/// </summary>
		public override void RightMousePress() 
		{
			mouse = PCMouseManager.CurrentMouseState;
			Point mousePos = new Point(mouse.X, mouse.Y);
			if (mUpButton.CheckForCollision(mousePos))
			{
				mUpButton.LeftMousePress();
				mValue++;
				if (mValue > mMaxValue)
					mValue = mMaxValue;
				mTextField.Text = mValue.ToString();
			}
			if (mDnButton.CheckForCollision(mousePos))
			{
				mDnButton.LeftMousePress();
				mValue--;
				if (mValue < mMinValue)
					mValue = mMinValue;
				mTextField.Text = mValue.ToString();
			}
		}
		/// <summary> RightMouseRelease Event Handler.
		/// </summary>
		public override void RightMouseRelease() { }
		/// <summary>ScrollWheelScroll Event Handler.
		/// </summary>
		public override void ScrollWheelScroll() 
		{ 
			// Possibly try to design this area so that if the mouse is over the field
			// area and the scrollwheel is scrolled, the value will increase or decrease
			// based on the direction of the scroll.
		}
		/// <summary> Click Event Handler.
		/// </summary>
		public override void Click() { }
		/// <summary> ChangeState Event Handler.
		/// </summary>
		public override void ChangeState(ControlStateArgs ControlState) { }

		#endregion
		#region Properties
		/// <summary> HasFocus { get;} bool Type;
		/// 
		/// </summary>
		public bool HasFocus
		{
			get
			{
				return mHasFocus;
			}
		}
		/// <summary> Value { get;} int Type;
		/// 
		/// </summary>
		public int Value
		{
			get
			{
				return mValue;
			}
		}
		/// <summary> IsDisabled { get; set; } bool Type;
		/// Used to Disable or Enable the Control.
		/// </summary>
		public bool IsDisabled
		{
			get
			{
				return base._State == PCControlState.disabled;
			}
			set
			{
				mIsDisabled = value;
				if ( value )
				{
					mDrawRectangle.X = mStartPoints[ ( int ) PCControlState.disabled ].X;
					mDrawRectangle.Y = mStartPoints[ ( int ) PCControlState.disabled ].Y;
					base._State = PCControlState.disabled;
				}
				else
				{
					mDrawRectangle.X = mStartPoints[ ( int ) PCControlState.released ].X;
					mDrawRectangle.Y = mStartPoints[ ( int ) PCControlState.released ].Y;
					base._State = PCControlState.released;
				}
			}
		}
		/// <summary> Name { get; set; } String type;
		/// 
		/// </summary>
		public string Name
		{
			get
			{
				return base._Name;
			}
			set
			{
				base._Name = value;
			}
		}
		/// <summary> Label { get; set; } String Type;
		/// 
		/// </summary>
		public string Label
		{
			get { return base._Label; }
			set 
			{ 
				base._Label = value;
			}
		}
		public LabelLocation LabelLocation
		{
			get { return base._Label_Location; }
			set { base._Label_Location = value; }
		}
		/// <summary> Index { get; set; } int Type;
		/// 
		/// </summary>
		public int Index
		{
			get
			{
				return base._Index;
			}
			set
			{
				base._Index = value;
			}
		}
		/// <summary> TabIndex { get; set; } int Type;
		/// 
		/// </summary>
		public int TabIndex
		{
			get
			{
				return base._TabIndex;
			}
			set
			{
				base._TabIndex = value;
			}
		}
		/// <summary> Type { get; } PCControlType Type;
		/// 
		/// </summary>
		public PCControlType Type
		{
			get
			{
				return base._Type;
			}
		}
		/// <summary> ControlID { get; } Guid? Type;
		/// 
		/// </summary>
		public Guid? ControlID
		{
			get
			{
				return base._UniqueId;
			}
		}
		/// <summary> Cursor_Style { get; set; } CursorStyle Type;
		/// 
		/// </summary>
		public CursorStyle Cursor_Style
		{
			get
			{
				return mCursorStyle;
			}
			set
			{
				mCursorStyle = value;
				switch ( value )
				{
					case CursorStyle.Horizontal:
					#region
					{
						PC_Control.TextCursor.SourceRect.X = mGraphicsOrigin.X + mDrawRect.Width;
						PC_Control.TextCursor.SourceRect.Y = mGraphicsOrigin.Y;
						PC_Control.TextCursor.SourceRect.Height = ( int ) mFontHeight / 6;
						PC_Control.TextCursor.SourceRect.Width = mFontWidth;
						PC_Control.TextCursor.Location.Y = ( int ) mTextPosition.Y + mFontHeight + ( mPad / 2 );
						mOldCursorStyle = CursorStyle.Horizontal;
					}
					break;
					#endregion
					case CursorStyle.Vertical:
					#region
					{
						PC_Control.TextCursor.SourceRect.X = mGraphicsOrigin.X + mDrawRect.Width;
						PC_Control.TextCursor.SourceRect.Y = mGraphicsOrigin.Y;
						PC_Control.TextCursor.SourceRect.Height = mFontHeight;
						PC_Control.TextCursor.SourceRect.Width = ( int ) mFontWidth / 6;
						PC_Control.TextCursor.Location.Y = ( int ) mTextPosition.Y + mPad;
						mOldCursorStyle = CursorStyle.Vertical;
					}
					break;
					#endregion
					case CursorStyle.Replace:
					#region
					{
						PC_Control.TextCursor.SourceRect.X = mGraphicsOrigin.X + mDrawRect.Width;
						PC_Control.TextCursor.SourceRect.Y = mGraphicsOrigin.Y;
						PC_Control.TextCursor.SourceRect.Height = mFontHeight;
						PC_Control.TextCursor.SourceRect.Width = ( int ) mFontWidth;
						PC_Control.TextCursor.Location.Y = ( int ) mTextPosition.Y + mPad;
					}
					break;
					#endregion
				}
			}
		}
		/// <summary> HighlightColor { get; set; } Color Type;
		/// 
		/// </summary>
		public Color HighlightColor
		{
			get
			{
				return mHighlightColor;
			}
			set
			{
				mHighlightColor = value;
			}
		}
		#endregion

		public NumericUpDn ( Texture2D Texture, Point GraphicsOrigin, Rectangle Location, int buttonWidth, SpriteFont Font, Point FontSize, Color FontColor )
		 {
			 base._Type = PCControlType.NumericUpDn;
			 mTexture = Texture;
			 mGraphicsOrigin = GraphicsOrigin;
			 mDrawRect = Location;
			 mLocation = Location;
			 mButtonWidth = buttonWidth;
			 mFont = Font;
			 mFontSize = FontSize;
			 mFontColor = FontColor;
			 mLabel = "";
			 base._Label_Location = LabelLocation.Right;
			 mSize = new Point(Location.Width, Location.Height);
			 mStartPoints = new Point[sizeof(PCControlState)];
			 mSourceRect = new Rectangle 
				 ( mGraphicsOrigin.X, mGraphicsOrigin.Y, mDrawRect.Width, mDrawRect.Height );
			 PC_Control.TextCursor.FontColor = FontColor;
			 mHighlightColor = new Color ( 48, 247, 245, 1 );
			 PC_Control.TextCursor.Texture = mTexture;
			 mValue = 0;
			 mMinValue = 0;
			 mMaxValue = 100;
			 mMaxStringLength = mMaxValue.ToString( ).Length;
			 if ( ( int ) mFontSize.X / 6 <= 0 )
				 PC_Control.TextCursor.SourceRect.Width = 1;
			 mTextPosition.X = mDrawRect.X + mPad;
			 mTextPosition.Y = mDrawRect.Y;
			 PC_Control.TextCursor.Location.X = ( int ) mTextPosition.X;
			 mTextBoxInputString = "0";
			 tPointer = 0;
			 this.Cursor_Style = CursorStyle.Vertical;
			 ControlState.NewControlState = PCControlState.released;
			 CreateComponents();
			 mPad = (int)((mDrawRect.Height - mFontSize.Y) * .75) / 4;
		 }
		private void CreateComponents()
		{
			// Text Field
			Rectangle DrawRectangle = mDrawRect;
			DrawRectangle.Width -= mButtonWidth;
			mTextField = new TextBox(mTexture, mGraphicsOrigin, DrawRectangle,mFont,mFontSize,mFontColor);
			mTextField.Name = "NumericUpDn1_TextField";
			// Up Button
			Point graphicsOrigin = new Point( mGraphicsOrigin.X + DrawRectangle.Width, mGraphicsOrigin.Y);
			Rectangle UpButtonDrawRect = new Rectangle(mDrawRect.X + DrawRectangle.Width, mDrawRect.Y, mButtonWidth, mDrawRect.Height/2 );
			mUpButton = new Button(mTexture, graphicsOrigin, UpButtonDrawRect);
			mUpButton.Name = "NumericUpDn1_UpButton";
			// Dn Button
			graphicsOrigin = new Point(mGraphicsOrigin.X + DrawRectangle.Width, mGraphicsOrigin.Y + (mDrawRect.Height / 2));
			Rectangle DnButtonDrawRect = new Rectangle(mDrawRect.X + DrawRectangle.Width, mDrawRect.Y + (mDrawRect.Height / 2), mButtonWidth, mDrawRect.Height / 2);
			mDnButton = new Button(mTexture, graphicsOrigin, DnButtonDrawRect);
			mDnButton.Name = "NumericUpDn1_DnButton";
		}
		public override void Draw ( SpriteBatch spriteBatch )
		{
			mTextField.Draw(spriteBatch);
			mUpButton.Draw(spriteBatch);
			mDnButton.Draw(spriteBatch);
		}

		public override void DrawLabel ( SpriteBatch spriteBatch)
		{
			if (base._Label != "")
			{
				switch (base._Label_Location)
				{
					case LabelLocation.Right:
						{
							/// Right of control
							Vector2 dPoint = new Vector2 (mLocation.X + mSize.X + mPad, mLocation.Y + mPad);
							spriteBatch.DrawString(mFont, base._Label, dPoint, mFontColor);
						}
						break;
					case LabelLocation.Left:
						{
							/// Left of control
							Vector2 dPoint = new Vector2
(mLocation.X - (int)(((base._Label.Length * mFontSize.X) * .85) - mPad), mLocation.Y + mPad);
							spriteBatch.DrawString(mFont, base._Label, dPoint, mFontColor);
						}
						break;
					case LabelLocation.Above:
						{
							/// Above control
							Vector2 dPoint = new Vector2
							((mLocation.X + (mSize.X / 2)) - ((int)(((base._Label.Length / 2) * .85) * mFontSize.X)),
							(mLocation.Y - (mFontSize.Y + (int)(mFontSize.Y / 3) + mPad)));
							spriteBatch.DrawString(mFont, base._Label, dPoint, mFontColor);
						}
						break;
					case LabelLocation.Below:
						{
							/// Below control
							Vector2 dPoint = new Vector2
							((mLocation.X + (mSize.X / 2)) - ((int)(((base._Label.Length / 2) * .85) * mFontSize.X)),
							(mLocation.Y + mSize.Y + mPad));
							spriteBatch.DrawString(mFont, base._Label, dPoint, mFontColor);
						}
						break;
				}
			}
		}

		public override bool CheckForCollision ( Point position )
		{
			return mDrawRect.Contains(position);
		}

		public override void Update ( )
		{
			//This is a rather inefficient method for handling keyboard input, but it is
			//(currently) the only method I could devise.
			mPreviousKeyboardState = mCurrentKeyboardState;
			mCurrentKeyboardState = PCKeyboardManager.CurrentKeyboardState;
			PC_Control.TextCursor.Location.X = ( mDrawRect.X + mPad ) + ( mTextBoxInputString.Length * mFontSize.X );
			if ( !( mCurrentKeyboardState == mPreviousKeyboardState ) )
			{
				//Enter Key
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.Enter ) ||
					( mTextBoxInputString.Length > mMaxStringLength ) )
				{
					if ( mTextBoxInputString.Length > 0 )
					{
						//mTextString = mTextBoxInputString;
						mHasFocus = false;
						PCMouseManager.ShowCursor ( );
					}
				}
				#region Number Keys:
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.NumPad0 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "0" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.D0 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "0" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.NumPad1 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "1" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.D1 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "1" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.NumPad2 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "2" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.D2 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "2" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.NumPad3 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "3" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.D3 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "3" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.NumPad4 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "4" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.D4 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "4" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.NumPad5 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "5" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.D5 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "5" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.NumPad6 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "6" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.D6 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "6" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.NumPad7 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "7" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.D7 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "7" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.NumPad8 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "8" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.D8 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "8" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.NumPad9 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "9" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.D9 ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "9" );
					tPointer++;
				}
				#endregion
				#endregion
				#region Movement and Editing Keys
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.Home ) )
				#region
				{
					tPointer = 0;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.End ) )
				#region
				{
					tPointer = mTextBoxInputString.Length;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.Left ) )
				#region
				{
					if ( tPointer > 0 )
					{
						tPointer--;
					}
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.Right ) )
				#region
				{
					if ( ( tPointer < ( mMaxStringLength - 1 ) ) && ( tPointer < mTextBoxInputString.Length ) )
					{
						tPointer++;
					}
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.Insert ) )
				#region
				{
					if ( ( this.Cursor_Style == CursorStyle.Vertical ) ||
						( this.Cursor_Style == CursorStyle.Horizontal ) )
					{
						this.Cursor_Style = CursorStyle.Replace;
					}
					else
					{
						this.Cursor_Style = mOldCursorStyle;
					}
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.Delete ) )
				#region
				{
					string temp1, temp2;
					if ( mIsHighlighted )
					{
						mTextBoxInputString = "";
						tPointer = 0;
						mIsHighlighted = false;
					}
					else
					{
						if ( tPointer < mTextBoxInputString.Length )
						{
							temp1 = mTextBoxInputString.Substring ( 0, tPointer );
							temp2 = mTextBoxInputString.Substring
								( tPointer + 1, mTextBoxInputString.Length - ( tPointer + 1 ) );
							temp1 = temp2.Insert ( 0, temp1 );
							mTextBoxInputString = temp1;
						}
					}
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.Back ) )
				#region
				{
					if ( ( mTextBoxInputString.Length > 0 ) && ( tPointer > 0 ) )
					{
						string temp1, temp2;
						if ( mIsHighlighted )
						{
							mTextBoxInputString = "";
							tPointer = 0;
							mIsHighlighted = false;
						}
						else
						{
							if ( tPointer < mTextBoxInputString.Length )
							{
								temp1 = mTextBoxInputString.Substring ( 0, tPointer - 1 );
								temp2 = mTextBoxInputString.Substring
									( tPointer, mTextBoxInputString.Length - tPointer );
								temp1 = temp2.Insert ( 0, temp1 );
							}
							else
							{
								temp1 = mTextBoxInputString.Remove ( mTextBoxInputString.Length - 1 );
							}
							mTextBoxInputString = temp1;
							tPointer--;
						}
					}
				}
				#endregion
				#endregion
			}
		}
	}
}
