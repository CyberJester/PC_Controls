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
	public class Checkbox : PC_Control
	{
		#region Variables
		private bool mCheckState = false;
		private bool mIsDisabled = false;
        private bool mIsHovered = false;
        private bool mWasLeftClicked, mWasMiddleClicked, mWasRightClicked = false;
        private bool mHotKeyIsPressed = false;
        private Rectangle mDrawRectangle;
		private Point[ ] mStartPoints;
		private Point mSize;
		private Rectangle mLocation;
		private Texture2D mTexture;
		private string mLabel;
        private ControlStateArgs ControlState = new ControlStateArgs ();
		private SpriteFont mFont;
		private Point mFontSize;
		private Color mFontColor;

		#endregion
		#region Events and Handlers
        /// <summary> HotKeyPressed Event Handler
        /// </summary>
        public override void HotKeyPressed()
        {
            if (!mIsDisabled)
            {
                ControlState.NewControlState = PCControlState.pressed;
                ChangeState(ControlState);
                mHotKeyIsPressed = true;
            }
        }
        /// <summary> HotKeyReleased Event Handler
        /// </summary>
        public override void HotKeyReleased()
        {
            if (!mIsDisabled)
            {
                ControlState.NewControlState = PCControlState.released;
                ChangeState(ControlState);
                mHotKeyIsPressed = false;
            }
        }
        /// <summary> MouseHover Event Handler.
        /// </summary>
        public override void MouseHover()
        {
            if (base._State == PCControlState.released)
            {
				ControlState.NewControlState = PCControlState.hover;
				ChangeState(ControlState);
				mIsHovered = true;
				/// TODO: Check for and if enabled, show tool-tip.
				/// set a start timer here so that on subsequent passes through
				/// when state is already hover it falls through and after a given 
				/// number of passes, shows the tooltip.
            }
			else
			if(base._State ==  PCControlState.hover)
			{

			}
        }
        /// <summary>  MouseUnHover Event Handler.
        /// </summary>
        public override void MouseUnHover()
        {
            if (base._State == PCControlState.hover)
            {
					ControlState.NewControlState = PCControlState.released;
					ChangeState(ControlState);
					mIsHovered = false;
            }
        }
        /// <summary> LeftMousePress Event Handler.
        /// </summary>
        public override void LeftMousePress()
        {
			if (!mIsDisabled)
			{
				if(mCheckState)
				{
					ControlState.NewControlState = PCControlState.released;
					ChangeState(ControlState);
					mCheckState = false;
					return;
				}
				else
				{
					ControlState.NewControlState = PCControlState.pressed;
					ChangeState(ControlState);
					mCheckState = true;
					return;
				}
			}
        }
		/// <summary> LeftMouseRelease Event Handler.
		/// </summary>
		public override void LeftMouseRelease() { }
		/// <summary> IsReleased Event Handler.
		/// </summary>
		public override void IsReleased()
		{
			if (!mIsDisabled)
			{
				ControlState.NewControlState = PCControlState.released;
				ChangeState(ControlState);
				mWasLeftClicked = mWasMiddleClicked = mWasRightClicked = false;
			}
		}
		/// <summary> MiddleMousePress Event Handler.
		/// </summary>
		public override void MiddleMousePress()
		{
			if (!mIsDisabled)
			{
				ControlState.NewControlState = PCControlState.pressed;
				ChangeState(ControlState);
				mWasMiddleClicked = true;
			}
		}
		/// <summary> MiddleMouseRelease Event Handler.
		/// </summary>
		public override void MiddleMouseRelease() { }
		/// <summary> RightMousePress Event Handler.
		/// </summary>
		public override void RightMousePress()
		{
			if (!mIsDisabled)
			{
				ControlState.NewControlState = PCControlState.pressed;
				ChangeState(ControlState);
				mWasRightClicked = true;
			}
		}
		/// <summary> RightMouseRelease Event Handler.
		/// </summary>
		public override void RightMouseRelease() { }
		/// <summary>ScrollWheelScroll Event Handler.
		/// </summary>
		public override void ScrollWheelScroll()
		{
			if (!mIsDisabled)
			{
			}
		}
		/// <summary> Click Event Handler.
		/// </summary>
		public override void Click()
		{
			if (!mIsDisabled)
			{
			}
		}
		/// <summary> ChangeState Event Handler.
		/// </summary>
		public override void ChangeState(ControlStateArgs ControlState)
        {
           base._State = ControlState.NewControlState;
		   switch (ControlState.NewControlState)
		   {
			   case PCControlState.pressed:
				   {
					   mDrawRectangle.X = mStartPoints[(int)PCControlState.pressed].X;
					   mDrawRectangle.Y = mStartPoints[(int)PCControlState.pressed].Y;
					   mCheckState = true;
				   }
				   break;
			   case PCControlState.released:
				   {
					   mDrawRectangle.X = mStartPoints[(int)PCControlState.released].X;
					   mDrawRectangle.Y = mStartPoints[(int)PCControlState.released].Y;
					   mCheckState = false;
				   }
				   break;
			   case PCControlState.hover:
				   {
					   mDrawRectangle.X = mStartPoints[(int)PCControlState.hover].X;
					   mDrawRectangle.Y = mStartPoints[(int)PCControlState.hover].Y;
				   }
				   break;
			   case PCControlState.disabled:
				   {
					   mDrawRectangle.X = mStartPoints[(int)PCControlState.disabled].X;
					   mDrawRectangle.Y = mStartPoints[(int)PCControlState.disabled].Y;
				   }
				   break;
		   }
        }

		#endregion
		#region Properties
		/// <summary>
		/// CheckBox LabelPosition. The allowable positions for the CheckBox's Label.
		/// </summary>
		public enum LabelPosition
		{
			AboveCentered,
			AboveLeft,
			AboveRight,
			BelowCentered,
			BelowLeft,
			BelowRight,
			Left,
			Right
		}
		/// <summary>
		/// Get returns current state of the CheckBox.
		/// Set fires the CheckedStateChanged Event if the control is not disabled.
		/// </summary>
		public bool IsChecked
		{
			get
			{
				return mCheckState;
			}
			set
			{
				if ( !mIsDisabled )
				{
					if ( mCheckState != value )
					{
						this.LeftMousePress ();
					}
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public PCControlState State
		{
			get
			{
				return base._State;
			}
			set
			{
				ControlState.NewControlState = value;
				ChangeState(ControlState);
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
		/// <summary>
		/// 
		/// </summary>
		public string Label
		{
			get
			{
				return mLabel;
			}
			set
			{
				mLabel = value;
			}
		}
		public Color LabelTextColor
		{
			get
			{
				return mFontColor;
			}
			set
			{
				mFontColor = value;
			}
		}
		#endregion
		/// <summary>
		/// Default Constructor
		/// </summary>
		public Checkbox ( )
		{
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="texture"> Texture for the CheckBox</param>
		/// <param name="graphicsOrigin"> Origin Point within the texture</param>
		/// <param name="location"> Screen location of the control, and size.</param>
		public Checkbox ( Texture2D texture, Point graphicsOrigin, Rectangle location )
		{
			mLabel = "";
			CreateCheckbox ( texture, graphicsOrigin, location );
		}
        public Checkbox (Texture2D texture, Point graphicsOrigin, Rectangle location, string Label)
        {
            mLabel = Label;
            CreateCheckbox(texture, graphicsOrigin, location);
        }
        private void CreateCheckbox(Texture2D texture, Point graphicsOrigin, Rectangle location)
        {
            mTexture = texture;
			mStartPoints = new Point[sizeof(PCControlState)];
			mLocation = location;
			mSize.X = location.Width;
			mSize.Y = location.Height;
			mDrawRectangle = new Rectangle (graphicsOrigin.X, graphicsOrigin.Y, location.Width, location.Height);
			mStartPoints[(int)PCControlState.released] = graphicsOrigin;
			mStartPoints[(int)PCControlState.pressed] = new Point (graphicsOrigin.X + mDrawRectangle.Width, graphicsOrigin.Y);
			mStartPoints[(int)PCControlState.disabled] = new Point(graphicsOrigin.X + (mDrawRectangle.Width * 2), graphicsOrigin.Y);
			mStartPoints[(int)PCControlState.hover] = new Point(graphicsOrigin.X + (mDrawRectangle.Width * 3), graphicsOrigin.Y);
			base._Location = location;
			base._Type = PCControlType.CheckBox;
			base._Label_Location = LabelLocation.Right;
			ControlState.NewControlState = State;
			mFontColor = Color.White;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="spriteBatch"></param>
		public override void Draw ( SpriteBatch spriteBatch )
		{
			spriteBatch.Draw ( mTexture, mLocation, mDrawRectangle, Color.White );
		}
		/// <summary>
		/// Draws the CheckBox's Label, if it has one.
		/// This will be modified to allow for placing the label on the left of the checkbox
		/// as well as above and below it.
		/// For now the label is centered vertically on the control and positioned to the right.
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="font"></param>
		/// <param name="fontHeight"></param>
		public override void DrawLabel ( SpriteBatch spriteBatch )
		{
			if ( mLabel != "" )
			{
				switch ( base._Label_Location )
				{
					case LabelLocation.Right:
					{
						/// Right of control
						Vector2 dPoint = new Vector2
						( mLocation.X + mSize.X  + ( mFontSize.X / 2 ), ( mLocation.Y - 2 ) + ( mFontSize.X / 2 ) );
						spriteBatch.DrawString ( mFont, mLabel, dPoint, mFontColor );
					}
					break;
					case LabelLocation.Left:
					{
						/// Left of control
						Vector2 dPoint = new Vector2
	(mLocation.X - ((mLabel.Length * mFontSize.X) + (mFontSize.X / 2)), (mLocation.Y - 2) + (mFontSize.Y / 2));
						spriteBatch.DrawString(mFont, mLabel, dPoint, mFontColor);
					}
					break;
					case LabelLocation.Above:
					{
						/// Above control
						Vector2 dPoint = new Vector2
	(mLocation.X - ((mLabel.Length / 2) * mFontSize.X), (mLocation.Y - (mFontSize.Y + (int)(mFontSize.Y / 3))));
						spriteBatch.DrawString(mFont, mLabel, dPoint, mFontColor);
					}
					break;
					case LabelLocation.Below:
					{
						/// Below control
						Vector2 dPoint = new Vector2
(mLocation.X - (((mLabel.Length / 2) * mFontSize.X)), (mLocation.Y + mSize.Y + (mFontSize.Y + (int)(mFontSize.Y / 3))));
						spriteBatch.DrawString(mFont, mLabel, dPoint, mFontColor);
					}
					break;
				}
			}
		}
		public override bool CheckForCollision ( Point position )
		{
			return mLocation.Contains ( position );
		}
		public override void Update ( )
		{
		}
	}
}
