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
	public class RadioButton : PC_Control
	{
		#region Variables
		private bool mIsChecked, mIsDisabled, mIsHovered, mHotKeyIsPressed = false;
		private Rectangle mDrawRectangle;
		private Point[ ] mStartPoints;
		private Point mSize;
		private Rectangle mLocation;
		private Texture2D mTexture;
		private string mLabel;
		private Color mLabelColor;
		private ControlStateArgs ControlState = new ControlStateArgs ();
		#endregion

		#region Events and Handlers
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
				if (base._State == PCControlState.hover)
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
			if (base ._State == PCControlState.released || base._State == PCControlState.hover)
			{
				ControlState.NewControlState = PCControlState.pressed;
				ChangeState(ControlState);
				mIsChecked = true;
			}
			else
				if(base ._State ==  PCControlState.pressed)
				{
					ControlState.NewControlState = PCControlState.released;
					ChangeState(ControlState);
					mIsChecked = false;
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
				mIsChecked = false;
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
				mIsChecked = true;
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
				mIsChecked = true;
			}
		}
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
		public override void ChangeState(ControlStateArgs ControlState)
        {
			base._State = ControlState.NewControlState;
			switch (ControlState.NewControlState)
            {
                case PCControlState.pressed:
                {
                    mDrawRectangle.X = mStartPoints[(int)PCControlState.pressed].X;
                    mDrawRectangle.Y = mStartPoints[(int)PCControlState.pressed].Y;
                    mIsChecked = true;
                }
				break;
				case PCControlState.released:
				{
					mDrawRectangle.X = mStartPoints[(int)PCControlState.released].X;
					mDrawRectangle.Y = mStartPoints[(int)PCControlState.released].Y;
					mIsChecked = false;
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
		/// RadioButton LabelPosition. The allowable positions for the CheckBox's Label.
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
				return mIsChecked;
			}
			set
			{
				if ( !mIsDisabled )
				{
					if ( mIsChecked != value )
					{
						LeftMousePress();
					}
				}
			}
		}
		/// <summary>
		/// Get: Returns true if the control is currently disabled, false otherwise.
		/// Set: Changes the current Disabled status of the control.
		/// </summary>
		public bool IsDisabled
		{
			get
			{
				return mIsDisabled;
			}
			set
			{
				mIsDisabled = value;
				mIsChecked = false;
				if ( value == true )
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
				return mLabelColor;
			}
			set
			{
				mLabelColor = value;
			}
		}
		#endregion

		/// <summary>
		/// Default Constructor
		/// </summary>
		public RadioButton ( )
		{
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="texture"> Texture for the RadioButton</param>
		/// <param name="graphicsOrigin"> Origin Point within the texture</param>
		/// <param name="location"> Screen location of the control, and size.</param>
		public RadioButton ( Texture2D texture, Point graphicsOrigin, Rectangle location )
		{
			mLabel = "";
			CreateRadioButton ( texture, graphicsOrigin, location );
		}
		public RadioButton(Texture2D texture, Point graphicsOrigin, Rectangle location, string Label)
		{
			mLabel = Label;
			CreateRadioButton(texture, graphicsOrigin, location);
		}
		private void CreateRadioButton(Texture2D texture, Point graphicsOrigin, Rectangle location)
		{
			mTexture = texture;
			mStartPoints = new Point[sizeof(PCControlState)];
			mLocation = location;
			mSize.X = location.Width;
			mSize.Y = location.Height;
			mDrawRectangle = new Rectangle(graphicsOrigin.X, graphicsOrigin.Y, location.Width, location.Height);
			mStartPoints[(int)PCControlState.released] = graphicsOrigin;
			mStartPoints[(int)PCControlState.pressed] = new Point(graphicsOrigin.X + mDrawRectangle.Width, graphicsOrigin.Y);
			mStartPoints[(int)PCControlState.disabled] = new Point(graphicsOrigin.X + (mDrawRectangle.Width * 2), graphicsOrigin.Y);
			mStartPoints[(int)PCControlState.hover] = new Point(graphicsOrigin.X + (mDrawRectangle.Width * 3), graphicsOrigin.Y);
			base._Location = location;
			base._State = PCControlState.released;
			base._Type = PCControlType.RadioButton;
			base._Label_Location = LabelLocation.Right;
			mLabelColor = Color.White;
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
		/// Draws the RadioButton's Label, if it has one.
		/// This will be modified to allow for placing the label AboveCentered, AboveLeft, AboveRight,
		/// BelowCentered, BelowLeft, BelowRight, left, or right of the control.
		/// For now the label is centered vertically on the control and positioned to the right.
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="font"></param>
		/// <param name="fontSize"></param>
		public override void DrawLabel ( SpriteBatch spriteBatch, SpriteFont font, int fontSize )
		{
			if ( mLabel != "" )
			{
				switch ( base._Label_Location )
				{
					case LabelLocation.Right:
					{
						/// Right of control
						Vector2 dPoint = new Vector2
						( mLocation.X + mSize.X + ( fontSize / 2 ),
						( mLocation.Y - 2 ) + ( fontSize / 2 ) );
						spriteBatch.DrawString ( font, mLabel, dPoint, mLabelColor );
					}
					break;
					case LabelLocation.Left:
					{
						/// Left of control
						Vector2 dPoint = new Vector2
						( mLocation.X - ( ( mLabel.Length * fontSize ) + ( fontSize / 2 ) ),
						( mLocation.Y - 2 ) + ( fontSize / 2 ) );
						spriteBatch.DrawString ( font, mLabel, dPoint, mLabelColor );
					}
					break;
					case LabelLocation.Above:
					{
						/// Above control
						Vector2 dPoint = new Vector2
						( mLocation.X - ( ( ( mLabel.Length / 2 ) * fontSize ) ),
						( mLocation.Y - ( fontSize + ( int ) ( fontSize / 3 ) ) ) );
						spriteBatch.DrawString ( font, mLabel, dPoint, mLabelColor );
					}
					break;
					case LabelLocation.Below:
					{
						/// Below control
						Vector2 dPoint = new Vector2
						( mLocation.X - ( ( ( mLabel.Length / 2 ) * fontSize ) ),
						( mLocation.Y + mSize.Y + ( fontSize + ( int ) ( fontSize / 3 ) ) ) );
						spriteBatch.DrawString ( font, mLabel, dPoint, mLabelColor );
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
