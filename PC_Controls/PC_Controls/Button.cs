using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace PC_Controls
{
    public class Button : PC_Control
    {
        /// <summary> Private Variables
        /// 
        /// </summary>
        private Texture2D mButtonSprite;
        private SpriteFont mFont;
        private Point mFontsize;
        private Rectangle mDrawRectangle;
		private Point mSize;
        private Rectangle mLocation;
        private Point[] mStartPoints;
        //private static int mButtonDuration; /// For use by the H_Scrollbar, H_Slider, V_Scrollbar, and V_Slider.
        private bool mIsHovered = false;
        private bool mWasLeftClicked, mWasMiddleClicked, mWasRightClicked = false;
        private bool mHotKeyIsPressed = false;
        private string mLabel;
        private Color mFontColor;
		private ControlStateArgs ControlState = new ControlStateArgs();

        #region EventHandlers

        /// <summary> HotKeyPressed Event Handler
        /// </summary>
        public override void HotKeyPressed()
        {
			if (base._State != PCControlState.disabled)
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
			if (base._State != PCControlState.disabled)
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
			if (base._State != PCControlState.disabled)
            {
                ControlState.NewControlState = PCControlState.pressed;
                ChangeState(ControlState);
                mWasLeftClicked = true;
            }
        }
        /// <summary> LeftMouseRelease Event Handler.
        /// </summary>
        public override void LeftMouseRelease()
        {
			if (base._State != PCControlState.disabled)
            {
                ControlState.NewControlState = PCControlState.released;
                ChangeState(ControlState);
            }
        }
        /// <summary> IsReleased Event Handler.
        /// </summary>
        public override void IsReleased()
        {
			if (base._State != PCControlState.disabled)
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
			if (base._State != PCControlState.disabled)
            {
                ControlState.NewControlState = PCControlState.pressed;
                ChangeState(ControlState);
                mWasMiddleClicked = true;
            }
        }
        /// <summary> MiddleMouseRelease Event Handler.
        /// </summary>
        public override void MiddleMouseRelease()
        {
			if (base._State != PCControlState.disabled)
            {
                ControlState.NewControlState = PCControlState.released;
                ChangeState(ControlState);
            }
        }
        /// <summary> RightMousePress Event Handler.
        /// </summary>
        public override void RightMousePress()
        {
			if (base._State != PCControlState.disabled)
            {
                ControlState.NewControlState = PCControlState.pressed;
                ChangeState(ControlState);
                mWasRightClicked = true;
            }
        }
        /// <summary> RightMouseRelease Event Handler.
        /// </summary>
        public override void RightMouseRelease()
        {
			if (base._State != PCControlState.disabled)
            {
                ControlState.NewControlState = PCControlState.released;
                ChangeState(ControlState);
            }
        }
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
                case PCControlState.released:
                    {
                        mDrawRectangle.X = mStartPoints[(int)PCControlState.released].X;
                        mDrawRectangle.Y = mStartPoints[(int)PCControlState.released].Y;
                    }
                    break;
                case PCControlState.pressed:
                    {
                        mDrawRectangle.X = mStartPoints[(int)PCControlState.pressed].X;
                        mDrawRectangle.Y = mStartPoints[(int)PCControlState.pressed].Y;
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
        /// <summary> UniqueId {get;} Guid? Type; Inherited.
        /// UniqueId property for every object
        /// </summary>
        public Guid? UniqueId
        {
            get
            {
                return base.mUniqueId;
            }
        }
        /// <summary> Index {get;set;} int Type; Inherited.
        /// Object Index number.
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
        /// <summary> TabIndex {get;set;} int Type; Inherited.
        /// Object Tab Index number.
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
        /// <summary> Name {get;set;} String Type; Inherited.
        /// Object Name
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
        /// <summary> Type {get;set;} PCControlType Type; Inherited.
        /// Object type
        /// </summary>
        public PCControlType Type
        {
            get
            {
                return base._Type;
            }
            set
            {
                base._Type = value;
            }
        }
        /// <summary> Location {get;set;} Rectangle Type; Inherited.
        /// Object location
        /// </summary>
        public Rectangle Location
        {
            get
            {
                return base._Location;
            }
            set
            {
                base._Location = value;
            }
        }
        /// <summary> Label_Location {get;set;} LabelLocation Type; Inherited.
        /// Object Label location
        /// </summary>
        public LabelLocation Label_Location
        {
            get
            {
                return base._Label_Location;
            }
            set
            {
                base._Label_Location = value;
            }
        }
        /// <summary> State {get;set} PCControlState Type; Inherited.
        /// Object current state
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
        /// <summary> IsHovered {get;} bool Type;
        /// Returns true if the button is being hovered over. Used to show ToolTips.
        /// </summary>
        public bool IsHovered
        {
            get
            {
                return mIsHovered;
            }
        }
        /// <summary> HotKeyIsPressed {get;} bool Type;
        /// Returns true if the buttons HotKey is currently being pressed.
        /// </summary>
        public bool HotKeyIsPressed
        {
            get
            {
                return mHotKeyIsPressed;
            }
        }
        /// <summary> WasLeftClicked {get;} bool Type;
        /// returns true if the control was clicked with the Left mouse button.
        /// </summary>
        public bool WasLeftClicked
        {
            get
            {
                return mWasLeftClicked;
            }
        }
		/// <summary> WasMiddleClicked {get;} bool Type;
		/// returns true if the control was clicked with the Middle mouse button.
		/// </summary>
		public bool WasMiddleClicked
		{
			get
			{
				return mWasMiddleClicked;
			}
		}
		/// <summary> WasRightClicked {get;} bool Type;
		/// returns true if the control was clicked with the Right mouse button.
		/// </summary>
		public bool WasRightClicked
		{
			get
			{
				return mWasRightClicked;
			}
		}
		/// <summary> Label {get;set;} string Type;
        /// Allows the reading and setting of the controls label. 
        /// Not Required to be set. Defaults to an empty string.
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
        /// <summary> LabelTextColor {get;set} Color Type.
        /// Not Required to be used. Defaults to Color.White from constructor.
        /// </summary>
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
        /// <summary> Font {get;set;} SpriteFont Type;
        /// If the Control does not have a label, then this will be null;
        /// </summary>
        public SpriteFont Font
        {
            get { return mFont; }
            set { mFont = value; }
            //mFontsize = mFont.}
        }

        /// <summary> HotKey
        /// HotKey allows the setting of a HotKey for the button and returns the key if it is set 
        /// or null.
        /// Set initializes the event handlers.
        /// </summary>
        public Keys HotKey
        {
            set { base._HotKey = value; }
            get { return base._HotKey; }
        }

        #endregion
        /// <summary> PC_Button()
        /// Default constructor.
        /// </summary>
        public Button()
		{
		}

        /// <summary> PC_Button ( Texture2D lButtonSprite, Point lGraphicOrigin, Rectangle lLocation )
		/// Main constructor for the class.
		/// </summary>
		/// <param name="lButtonSprite"> Texture graphic for the button</param>
		/// <param name="lGraphicOrigin"> Origin point within the graphic of the buttons textures</param>
		/// <param name="location"> Location and size of the button</param>
		public Button(Texture2D lButtonSprite, Point graphicsOrigin, Rectangle location)
		{
			mButtonSprite = lButtonSprite;
			mDrawRectangle = new Rectangle
				(graphicsOrigin.X, graphicsOrigin.Y, location.Width, location.Height);
			mSize.X = location.Width;
			mSize.Y = location.Height;
			mLocation = location;
			mStartPoints = new Point[ sizeof(PCControlState) ];
			Label_Location = LabelLocation.Right;
			mLabel = "";
			mFontColor = Color.White;
			ControlState.NewControlState = State;
			mDrawRectangle = new Rectangle(graphicsOrigin.X, graphicsOrigin.Y, location.Width, location.Height);
			mStartPoints[(int)PCControlState.released] = graphicsOrigin;
			mStartPoints[(int)PCControlState.pressed] = new Point(graphicsOrigin.X + mDrawRectangle.Width, graphicsOrigin.Y);
			mStartPoints[(int)PCControlState.disabled] = new Point(graphicsOrigin.X + (mDrawRectangle.Width * 2), graphicsOrigin.Y);
			mStartPoints[(int)PCControlState.hover] = new Point(graphicsOrigin.X + (mDrawRectangle.Width * 3), graphicsOrigin.Y);
		}

        /// <summary> Draw(SpriteBatch spriteBatch) (REQUIRED) Implemented from the base class.
        /// Handles drawing the button.
        /// </summary>
        /// <param name="spriteBatch"> SpriteBatch Type. </param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mButtonSprite, mLocation, mDrawRectangle, Color.White);
        }

        /// <summary> DrawLabel(SpriteBatch spriteBatch, SpriteFont font, int fontSize)  
        /// (REQUIRED) Implemented from the base class. 
        /// If the label string is 0 length, then the method simply passes through and does nothing.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch for drawing</param>
        /// <param name="font">SpriteFont to use</param>
        /// <param name="fontSize">int type</param>
        public override void DrawLabel(SpriteBatch spriteBatch, SpriteFont font, int fontSize)
        {
            if (mLabel != "")
            {
                switch (Label_Location)
                {
                    case LabelLocation.Right:
                        #region
                        {
                            /// Right of control
                            Vector2 dPoint = new Vector2
                            (mLocation.X + mSize.X + (fontSize / 2), (mLocation.Y - 2) + (fontSize / 2));
                            spriteBatch.DrawString(font, mLabel, dPoint, mFontColor);
                        }
                        break;
                        #endregion
                    case LabelLocation.Left:
                        #region
                        {
                            /// Left of control
                            Vector2 dPoint = new Vector2
    (mLocation.X - ((mLabel.Length * fontSize) + (fontSize / 2)), (mLocation.Y - 2) + (fontSize / 2));
                            spriteBatch.DrawString(font, mLabel, dPoint, mFontColor);
                        }
                        break;
                        #endregion
                    case LabelLocation.Above:
                        #region
                        {
                            /// Above control
                            Vector2 dPoint = new Vector2
    (mLocation.X - ((mLabel.Length / 2) * fontSize), (mLocation.Y - (fontSize + (int)(fontSize / 3))));
                            spriteBatch.DrawString(font, mLabel, dPoint, mFontColor);
                        }
                        break;
                        #endregion
                    case LabelLocation.Below:
                        #region
                        {
                            /// Below control
                            Vector2 dPoint = new Vector2
                        (mLocation.X - (((mLabel.Length / 2) * fontSize)),
                        (mLocation.Y + mDrawRectangle.Height + (fontSize + (int)(fontSize / 3))));
                            spriteBatch.DrawString(font, mLabel, dPoint, mFontColor);
                        }
                        break;
                        #endregion
                }
            }
        }

        /// <summary> CheckForCollision(Point position) (REQUIRED) Implemented from the base class. 
        /// </summary>
        /// <param name="position"> Point Type. </param>
        /// <returns> True if the cursor is in contact with the control. </returns>
        public override bool CheckForCollision(Point position)
        {
            return mLocation.Contains(position);
        }

        /// <summary> Update ()  (REQUIRED) Implemented from the base class. 
        /// Not used in this control. Empty holder.
        /// </summary>
        public override void Update()
        {
        }
    }
}
