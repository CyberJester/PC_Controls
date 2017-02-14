using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace PC_Controls
{
   public class ControlStateArgs : EventArgs
    {
       public PC_Control.PCControlState NewControlState;
    }
   /// <summary> PC_Control Base Class.
    /// Base class from which the other PCInput classes are derived. 
    /// Provides a generated Guid? UniqueID, an int Index, and TabIndex,
    /// a string Name, a Rectangle location for a control label and Enumerations for
    /// the controls state, type, and label location. It also contains the TextCursor
    /// structure that can be used by controls that inherit the base class.
    /// 
    /// The base class also contains abstract holders for the major methods needed by all the controls:
    /// Draw, DrawLabel, CheckForCollision, Click, and Update.
    /// </summary>
    public abstract class PC_Control
    {
        /// <summary> PCControlState
        /// Enumeration of the four possible states that a control can have:
        /// released, pressed, disabled, and hover
        /// </summary>
		public enum PCControlState
		{
			released,
			pressed,
			disabled,
			hover
		}

        /// <summary> LabelLocation
        /// Enumeration of the four possible locations of the controls label:
        /// Above, Below, Left, and Right.
        /// </summary>
		public enum LabelLocation
		{
			Above,
			Below,
			Left,
			Right
		}

        /// <summary> PCControlType
        /// Public Enumeration of the 18 different types of controls.
        /// </summary>
		public enum PCControlType
		{
			CheckBox,
			RadioButton,
			Label,
			TextBox,
			ProgressBar,
			StatusStrip,
			ToolTip,
			// All the controls below this line handle a IsReleased method. The ones above do not.
			Button,
			MessageBox,	// OK, OK - CANCEL, YES/NO, YES/NO - CANCEL
			ListBox,
			HScrollBar,
			VScrollBar,
			HSlider,
			VSlider,
			OpenFileDialog,
			SaveFileDialog,
			NumericUpDn,
			MenuStrip
		}

        /// <summary> Public Struct TextCursor
        /// Public structure that is used by several controls to hold and draw either a vertical,
        /// horizontal, or "Replace" type of cursor. (The replace type is the width of the character one.)
        /// </summary>
		public struct TextCursor
		{
			public static Texture2D Texture;
			public static Point Location;
			public static Rectangle SourceRect;
			public static bool IsVisable;
			public static Color FontColor;

			public void Draw ( SpriteBatch spriteBatch )
			{
				if ( IsVisable )
				{
					spriteBatch.Draw ( Texture,
						new Rectangle ( Location.X, Location.Y, SourceRect.Width, SourceRect.Height ),
						SourceRect, FontColor );
				}
			}
		}

		/// <summary> Private Internal Variables
		/// </summary>
		protected Guid? mUniqueId;
		private Keys mHotKey;
		private int mIndex;
		private int mTabIndex;
		private string mName;
		private Rectangle mLocation;
		private string mLabel;
		private LabelLocation mLabelLocation;
		private PCControlType mType;
		private PCControlState mState, mPreviousState, mNewState;
		private ControlStateArgs ControlState = new ControlStateArgs();

        #region Event Handlers
        /// <summary> Event Handlers
        /// 
        /// </summary>
        #region HotKeyPress
        public event EventHandler _HotKeyPress = delegate { };
        private delegate void HotKeyPressHandler(object sender, EventArgs e);
        /// <summary> HotKeyPressMethod 
        /// </summary>
        public void HotKeyPressMethod(object sender, EventArgs e)
        {
            HotKeyPressed();
        }
        /// <summary> HotKeyPressed Event Handler
        /// </summary>
        public abstract void HotKeyPressed();
        #endregion
        #region HotKeyRelease
        /// <summary> KotKeyRelease Event
        /// </summary>
        public event EventHandler _HotKeyRelease = delegate { };
        private delegate void HotKeyReleaseHandler(object sender, EventArgs e);
        /// <summary> HotKeyReleaseMethod 
        /// </summary>
        public void HotKeyReleaseMethod(object sender, EventArgs e)
        {
            HotKeyReleased();
        }
        /// <summary> HotKeyReleased Event Handler
        /// </summary>
        public abstract void HotKeyReleased();
        #endregion
        #region MouseHover
        public event EventHandler _MouseHover = delegate { };
        /// <summary> MouseHover Event
        /// </summary>
        private delegate void MouseHoverHandler(object sender, EventArgs e);
        /// <summary> MouseHover Method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseHoverMethod(object sender, EventArgs e)
        {
            MouseHover();
        }
        public abstract void MouseHover();
        #endregion
        #region MouseUnHover
        public event EventHandler _MouseUnHover = delegate { };
        /// <summary> MouseUnHover Event
        /// </summary>
        private delegate void MouseUnHoverHandler(object sender, EventArgs e);
        /// <summary>  MouseUnHover Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseUnHoveredMethod(object sender, EventArgs e)
        {
            MouseUnHover();
        }
        public abstract void MouseUnHover();
        #endregion
        #region LeftMousePress
        public virtual event EventHandler _LeftMousePress = delegate { };
        /// <summary> LeftMousePress Event
        /// </summary>
        private delegate void LeftMousePressHandler(object sender, EventArgs e);
        /// <summary> LeftMousePress Method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LeftMousePressMethod(object sender, EventArgs e)
        {
            LeftMousePress();
        }
        /// <summary> LeftMousePress Event Handler.
        /// </summary>
        public abstract void LeftMousePress();
        #endregion
        #region LeftMouseRelease
        public event EventHandler _LeftMouseRelease = delegate { };
        /// <summary> LeftMouseRelease Event
        /// </summary>
        private delegate void LeftMouseReleaseHandler(object sender, EventArgs e);
        /// <summary> LeftMouseRelease Method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LeftMouseReleaseMethod(object sender, EventArgs e)
        {
            LeftMouseRelease();
        }
        public abstract void LeftMouseRelease();
        #endregion
        #region RightMousePress
        public event EventHandler _RightMousePress = delegate { };
        /// <summary> RightMousePress Event
        /// </summary>
        private delegate void RightMousePressHandler(object sender, EventArgs e);
        /// <summary> RightMousePress Method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RightMousePressMethod(object sender, EventArgs e)
        {
            RightMousePress();
        }
        public abstract void RightMousePress();
        #endregion
        #region RightMouseRelease
        public event EventHandler _RightMouseRelease = delegate { };
        /// <summary> RightMouseRelease Event
        /// </summary>
        private delegate void RightMouseReleaseHandler(object sender, EventArgs e);
        /// <summary> RightMouseRelease Method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RightMouseReleaseMethod(object sender, EventArgs e)
        {
            RightMouseRelease();
        }
        public abstract void RightMouseRelease();
        #endregion
        #region MiddleMousePress
        public event EventHandler _MiddleMousePress = delegate { };
        /// <summary> MiddleMousePress Event
        /// </summary>
        private delegate void MiddleMousePressHandler(object sender, EventArgs e);
        /// <summary> MiddleMousePress Method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MiddleMousePressMethod(object sender, EventArgs e)
        {
            MiddleMousePress();
        }
        public abstract void MiddleMousePress();
        #endregion
        #region MiddleMouseRelease
        public event EventHandler _MiddleMouseRelease = delegate { };
        /// <summary> MiddleMouseRelease Event
        /// </summary>
        private delegate void MiddleMouseReleaseHandler(object sender, EventArgs e);
        /// <summary> MiddleMouseRelease Method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MiddleMouseReleaseMethod(object sender, EventArgs e)
        {
            MiddleMouseRelease();
        }
        public abstract void MiddleMouseRelease();
        #endregion
        #region IsReleased
        public event EventHandler _IsReleased = delegate { };
        /// <summary> IsReleased Event
        /// </summary>
        private delegate void IsReleasedHandler(object sender, EventArgs e);
        /// <summary> IsReleased Method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void IsReleasedMethod(object sender, EventArgs e)
        {
            IsReleased();
        }
        public abstract void IsReleased();
        #endregion
        #region ScrollWheelScroll
        public event EventHandler _ScrollWheelScroll = delegate { };
        /// <summary> ScrollWheelScroll Event
        /// </summary>
        private delegate void ScrollWheelScrollHandler(object sender, EventArgs e);
        /// <summary> ScrollWheelScroll Method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ScrollWheelScrollMethod(object sender, EventArgs e)
        {
            ScrollWheelScroll();
        }
        public abstract void ScrollWheelScroll();
        #endregion
        #region Click
        public event EventHandler _Click = delegate { };
        /// <summary> Click Event
        /// </summary>
        private delegate void ClickHandler(object sender, EventArgs e);
        /// <summary> Click Method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClickMethod(object sender, EventArgs e)
        {
            Click();
        }
        public abstract void Click();
        #endregion
        #region ChangeState
        public event EventHandler _ChangeState = delegate { };
        /// <summary> ChangeState Event
        /// </summary>
        private delegate void ChangeStateHandler(object sender, ControlStateArgs state);
        /// <summary> ChangeState Method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChangeStateMethod(object sender, ControlStateArgs state)
        {
            ChangeState(state);
        }
        public abstract void ChangeState( ControlStateArgs NewState);
        #endregion
        #endregion

		#region Properties

		/// <summary> UniqueId {get;} Guid? Type
		/// UniqueId property for every Control.
		/// </summary>
		public Guid? _UniqueId
		{
			get
			{
				return mUniqueId;
			}
		}
		/// <summary> _Index {get;set;} int Type
		/// Object Index number.
		/// </summary>
		public int _Index
		{
			get
			{
				return mIndex;
			}
			set
			{
				mIndex = value;
			}
		}
		/// <summary> _TabIndex {get;set;} int Type
		/// Object Tab Index number.
		/// </summary>
		public int _TabIndex
		{
			get
			{
				return mTabIndex;
			}
			set
			{
				mTabIndex = value;
			}
		}
		/// <summary> _Name {get;set;} String Type
		/// Object Name
		/// </summary>
		public string _Name
		{
			get
			{
				return mName;
			}
			set
			{
				mName = value;
			}
		}
		/// <summary> _Type {get;set;} PCControlType Type
		/// Object type
		/// </summary>
		public PCControlType _Type
		{
			get
			{
				return mType;
			}
			set
			{
				mType = value;
			}
		}
		/// <summary> Location {get;set;} Rectangle Type
		/// Object location
		/// </summary>
		public Rectangle _Location
		{
			get
			{
				return mLocation;
			}
			set
			{
				mLocation = value;
			}
		}
		public string _Label
		{
			get { return mLabel; }
			set { mLabel = value; }
		}
		/// <summary> Label_Location {get;set;} LabelLocation Type
		/// Object Label location
		/// </summary>
		public LabelLocation _Label_Location
		{
			get
			{
				return mLabelLocation;
			}
			set
			{
				mLabelLocation = value;
			}
		}
		/// <summary> _State {get;set} PCControlState Type
		/// Object current state
		/// </summary>
		public PCControlState _State
		{
			get
			{
				return mState;
			}
			set
			{
				mState = value;
			}
		}
		/// <summary> _HotKey {get;set} Keys Type
		/// Controls HotKey.
		/// </summary>
		public Keys _HotKey
		{
			get { return mHotKey; }
			set { mHotKey = value; }
		}
		#endregion

		/// Constructor
		public PC_Control ( )
		{
			//create a new unique id for this business object
			mUniqueId = Guid.NewGuid ( );
			mIndex = 0;
			mTabIndex = 0;
			mName = "";
			mState = PCControlState.released;
		}

		#region Abstract Methods
		/// <summary> Abstract for the controls Draw method
		/// </summary>
		/// <param name="spriteBatch"> SpriteBatch Type</param>
		public abstract void Draw ( SpriteBatch spriteBatch );

		/// <summary> Abstract for the controls DrawLabel method
		/// </summary>
		/// <param name="spriteBatch">SpriteBatch Type</param>
		/// <param name="font">SpriteFont Type</param>
		/// <param name="fontSize">int Type</param>
		public abstract void DrawLabel ( SpriteBatch spriteBatch );

		/// <summary> Abstract for the controls CheckForCollision method
		/// </summary>
		/// <param name="position">Point Type</param>
		/// <returns>bool Type. True if mouse is in contact</returns>
		public abstract bool CheckForCollision ( Point position );

		/// <summary> Abstract for the controls Update method
		/// </summary>
        public abstract void Update();
		#endregion
	}
}
