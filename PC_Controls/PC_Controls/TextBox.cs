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
	/// <summary> PCTextBox Control
	/// A standard text entry control, suitable for the entering of a player name, saved file name, etc.
	/// 
	/// </summary>
	 public class TextBox : PC_Control
	{
		 private bool mHasFocus;
		 private bool mIsUpper = false;
		 private bool mIsHighlighted = false;
		 private Color mHighlightColor;
		 private int mHighlightSize;
		 private Texture2D mTexture;
		 private TextCursor mTextCursor;
		 private Point mGraphicsOrigin;
		 private SpriteFont mFont;
		 private int mFontWidth, mFontHeight;
		 private Color mFontColor;
		 private int mPad;
		 private string mTextBoxInputString;
		 private string mTextString;
		 private int mMaxStringLength;
		 private int tPointer;
		 private Rectangle mDrawRect = new Rectangle( );
		 private Rectangle mSourceRect;
		 private Vector2 mTextPosition = new Vector2 ( );
		 private KeyboardState mCurrentKeyboardState, mPreviousKeyboardState;
		 private int mBlink;
		 private int BlinkTime = 30;
		 private CursorStyle mCursorStyle;
		 private CursorStyle mOldCursorStyle;
		 private ControlStateArgs ControlState = new ControlStateArgs();

		 #region EventHandlers

		 /// <summary> HotKeyPressed Event Handler
		 /// </summary>
		 public override void HotKeyPressed() { }
		 /// <summary> HotKeyReleased Event Handler
		 /// </summary>
		 public override void HotKeyReleased() { }
		 /// <summary> MouseHover Event Handler.
		 /// </summary>
		 public override void MouseHover() { }
		 /// <summary>  MouseUnHover Event Handler.
		 /// </summary>
		 public override void MouseUnHover() { }
		 /// <summary> LeftMousePress Event Handler.
		 /// </summary>
		 public override void LeftMousePress() { }
		 /// <summary> LeftMouseRelease Event Handler.
		 /// </summary>
		 public override void LeftMouseRelease() { }
		 /// <summary> IsReleased Event Handler.
		 /// </summary>
		 public override void IsReleased() { }
		 /// <summary> MiddleMousePress Event Handler.
		 /// </summary>
		 public override void MiddleMousePress() { }
		 /// <summary> MiddleMouseRelease Event Handler.
		 /// </summary>
		 public override void MiddleMouseRelease() { }
		 /// <summary> RightMousePress Event Handler.
		 /// </summary>
		 public override void RightMousePress() { }
		 /// <summary> RightMouseRelease Event Handler.
		 /// </summary>
		 public override void RightMouseRelease() { }
		 /// <summary>ScrollWheelScroll Event Handler.
		 /// </summary>
		 public override void ScrollWheelScroll() { }
		 /// <summary> Click Event Handler.
		 /// </summary>
		 public override void Click() { }
		 /// <summary> ChangeState Event Handler.
		 /// </summary>
		 public override void ChangeState(ControlStateArgs ControlState) { }

		 #endregion
		 #region Properties
		 public bool HasFocus
		 {
			 get
			 {
				 return mHasFocus;
			 }
		 }
		 public string Text
		 {
			 get { return mTextString; }
			 set 
			 { 
				mTextBoxInputString = value;
				mTextString = value;
			 }
		 }
		 /// <summary>
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
		 /// <summary>
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
		 /// <summary>
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
		 /// <summary>
		 /// 
		 /// </summary>
		 public PCControlType Type
		 {
			 get
			 {
				 return base._Type;
			 }
		 }
		 /// <summary>
		 /// 
		 /// </summary>
		 public Guid? ControlID
		 {
			 get
			 {
				 return base._UniqueId;
			 }
		 }
		 public enum CursorStyle
		 {
			 Vertical,
			 Horizontal,
			 Replace
		 }
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
		 public TextBox ( )
		 {
		 }
		 public TextBox ( Texture2D Texture, Point GraphicsOrigin, Rectangle DrawRect, SpriteFont Font, Point FontSize, Color FontColor )
		 {
			 base._Type = PCControlType.TextBox;
			 mTexture = Texture;
			 mGraphicsOrigin = GraphicsOrigin;
			 mDrawRect = DrawRect;
			 mSourceRect = new Rectangle 
				 ( mGraphicsOrigin.X, mGraphicsOrigin.Y, mDrawRect.Width, mDrawRect.Height );
			 mFont = Font;
			 mFontWidth = FontSize.X;
			 mFontHeight = FontSize.Y;
			 mFontColor = FontColor;
			 PC_Control.TextCursor.FontColor = FontColor;
			 mHighlightColor = new Color ( 48, 247, 245, 1 );
			 PC_Control.TextCursor.Texture = mTexture;
			 mPad = (mDrawRect.Height - mFontHeight) / 2;
			 mMaxStringLength = (int)(mDrawRect.Width - ( mPad * 2 )) / mFontWidth;
			 if ( ( int ) mFontWidth / 6 <= 0 )
				 PC_Control.TextCursor.SourceRect.Width = 1;
			 mTextPosition.X = mDrawRect.X + mPad;
			 mTextPosition.Y = mDrawRect.Y;
			 PC_Control.TextCursor.Location.X = ( int ) mTextPosition.X;
			 mTextBoxInputString = "";
			 mTextString = "";
			 tPointer = 0;
			 this.Cursor_Style = CursorStyle.Vertical;
			 ControlState.NewControlState = PCControlState.released;
		 }
		public override void Update ( )
		{
			//This is a rather inefficient method for handling keyboard input, but it is
			//(currently) the only method I could devise.
			mPreviousKeyboardState = mCurrentKeyboardState;
			mCurrentKeyboardState = PCKeyboardManager.CurrentKeyboardState;
			PC_Control.TextCursor.Location.X = ( mDrawRect.X + mPad ) + ( mTextBoxInputString.Length * mFontWidth );
			if ( !( mCurrentKeyboardState == mPreviousKeyboardState ) )
			{
				//Enter Key
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.Enter ) ||
					( mTextBoxInputString.Length > mMaxStringLength ) )
				{
					if ( mTextBoxInputString.Length > 0 )
					{
						mTextString = mTextBoxInputString;
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
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, ")" );
					else
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
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "!" );
					else
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
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "@" );
					else
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
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "#" );
					else
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
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "$" );
					else
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
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "%" );
					else
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
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "^" );
					else
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
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "&" );
					else
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
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "*" );
					else
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
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "(" );
					else
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
				#region Letter Keys:
				if ( ( PCKeyboardManager.CurrentHeldKey == Keys.LeftShift ) || 
					(PCKeyboardManager.CurrentHeldKey == Keys.RightShift ) )
						mIsUpper = true;
				else
					mIsUpper = false;
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.A ) )
				#region 
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "A" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "a" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.B ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "B" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "b" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.C ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "C" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "c" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.D ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "D" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "d" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.E ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "E" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "e" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.F ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "F" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "f" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.G ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "G" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "g" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.H ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "H" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "h" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.I ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "I" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "i" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.J ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "J" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "j" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.K ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "K" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "k" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.L ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "L" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "l" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.M ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "M" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "m" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.N ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "N" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "n" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.O ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "O" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "o" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.P ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "P" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "p" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.Q ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "Q" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "q" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.R ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "R" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "r" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.S ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "S" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "s" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.T ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "T" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "t" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.U ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "U" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "u" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.V ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "V" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "v" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.W ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "W" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "w" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.X ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "X" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "x" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.Y ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "Y" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "y" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.Z ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "Z" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "z" );
					tPointer++;
				}
				#endregion
				#endregion
				#region Other Allowable Keys
				if ( mCurrentKeyboardState.IsKeyDown ( Microsoft.Xna.Framework.Input.Keys.Space ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, " " );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.OemMinus ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "_" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "-" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.OemPlus ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "+" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "=" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.OemOpenBrackets ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "{" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "[" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.OemCloseBrackets ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "}" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "]" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.OemPipe ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "|" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "\\" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.OemSemicolon ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, ":" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, ";" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.OemQuotes ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "\"" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "'" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.OemComma ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "<" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "," );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.OemPeriod ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, ">" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "." );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.OemQuestion ) )
				#region
				{
					if ( mIsUpper )
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "?" );
					else
						mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "/" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.Divide ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "/" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.Multiply ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "*" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.Subtract ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "-" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.Add ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "+" );
					tPointer++;
				}
				#endregion
				if ( mCurrentKeyboardState.IsKeyDown ( Keys.Decimal ) )
				#region
				{
					mTextBoxInputString = mTextBoxInputString.Insert ( tPointer, "." );
					tPointer++;
				}
				#endregion
				#endregion
			}
		}

		public override void Draw ( SpriteBatch spriteBatch )
		{
			spriteBatch.Draw ( mTexture, mDrawRect, mSourceRect, Color.White );
			if ( mHasFocus )
			{
				Update ( );
				if ( mIsHighlighted )
				{
					Rectangle hDrawRect = new Rectangle
					( ( int ) mTextPosition.X, ( int ) mTextPosition.Y + mPad, mHighlightSize, mFontHeight );
					Rectangle hSourceRect = new Rectangle
						( mSourceRect.X + ( hDrawRect.X - mDrawRect.X ), 
						mSourceRect.Y + ( hDrawRect.Y - mDrawRect.Y ), 
						mHighlightSize, 
						mFontHeight );
					spriteBatch.Draw ( mTexture, hDrawRect, hSourceRect, mHighlightColor );
				}
				spriteBatch.DrawString ( mFont, mTextBoxInputString, mTextPosition, mFontColor );
				mBlink++;
				if ( mBlink > BlinkTime )
				{
					mBlink = 0;
					PC_Control.TextCursor.IsVisable = !PC_Control.TextCursor.IsVisable;
				}
				PC_Control.TextCursor.Location.X = ( tPointer * mFontWidth ) + ( int ) mTextPosition.X;
				mTextCursor.Draw ( spriteBatch );
			}
			else
			{
				spriteBatch.DrawString ( mFont, mTextBoxInputString, mTextPosition, mFontColor );
			}
		}

		public override void DrawLabel ( SpriteBatch spriteBatch )
		{
		}

		public override bool CheckForCollision ( Point position )
		{
			return mDrawRect.Contains ( position );
		}
		 /// <summary>
		 /// PCTextBox.Click ( ): Handles mouse clicks on the TextBox.
		 /// </summary>
		public void Click ( MouseState mouseState )
		{
			int lPos = ( mouseState.X - ( int ) mTextPosition.X ) / mFontWidth;
			/// If the TextBox does not have focus when it is clicked, this means that this is the
			/// first time through and we need to set the focus.
			if ( !mHasFocus )
			{
				mHasFocus = true;
				tPointer = 0;
				return;
			}
			/// Otherwise if we already have the focus, we need to see if the TextBox is  Highlighted.
			 if(mHasFocus)
			 {
				 /// If not, we turn on the Highlight and return.
				 if ( !mIsHighlighted )
				 {
					 /// If the length of the input string is 0, then the user has clicked on
					 /// the empty TextBox and we can ignore it.
					 if ( mTextBoxInputString.Length == 0 )
					 {
						 mIsHighlighted = false;
						 return;
					 }
					 /// If the input string length is > 0 then we need to check if the tPointer
					 /// is within the input string or at its end.
					 if ( tPointer < mTextBoxInputString.Length )
					 {
						 /// If the pointer is within the input string, and the cursor position is either past
						 /// the end of the string, or somewhere else in the string, we leave the highlight off
						 /// and move the pointer instead.
						 if ( lPos > mTextBoxInputString.Length )
						 {
							 mIsHighlighted = false;
							 tPointer = mTextBoxInputString.Length;
							 return;
						 }
						 mIsHighlighted = false;
						 tPointer = lPos;
						 return;
					 }
					 /// Otherwise, since we only want to Highlight the text, not the entire box,
					 /// we set the Highlight size to the length of the input string multiplyed by 
					 /// the FontWidth.
					 mHighlightSize = mTextBoxInputString.Length * mFontWidth;
					 mIsHighlighted = true;
					 return;
				 }
				 /// If it is Highlighted, we need to determine where in the TextBox the mouse was clicked.
				 /// If it is past the end of the input string then the user is "most likely" simply
				 /// clearing the Highlight, so we do not adjust the tPointer.
				 /// However; if the mouse is within the area of the input string we determine the
				 /// nearest letter, set the tPointer to that position and then turn off the Highlight.
				 if ( mIsHighlighted )
				 {
					 if ( lPos > tPointer )
					 {
						 mIsHighlighted = false;
						 return;
					 }
					 tPointer = ( mouseState.X - (int)mTextPosition.X ) / mFontWidth;
					 mIsHighlighted = false;
				 }
			 }
		}
	}
}
