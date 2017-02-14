using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Reflection;
namespace PC_Controls
{
	public class PCControlManager
	{
		private List<PC_ControlGroup> mControlGroups;
		private PC_Control mCurrentActiveControl;
		private int mCurrentIndex = 0;
		private PC_ControlGroup mCurrentGroup;
		private bool mIsDoubleClicked;
		private bool mIsMouseHeld;
		private bool mIsRightPressed;
		private bool mIsRightDoubleClicked;
		private bool mIsRightHeld;

		/// <summary> CurrentActiveButton
		/// The button currently under the mouse (null if the mouse isn't over a button)
		/// </summary>
		public PC_Control CurrentActiveControl
		{
			get
			{
				return mCurrentActiveControl;
			}
		}
		/// <summary> IsDoubleClicked
		/// Returns true if the buttons was Left DoubleClicked.
		/// </summary>
		public bool IsDoubleClicked
		{
			get
			{
				return mIsDoubleClicked;
			}
		}
		/// <summary> IsHeld
		/// Returns true if the Left Mouse Button was held on the button.
		/// </summary>
		public bool IsHeld
		{
			get
			{
				return mIsMouseHeld;
			}
		}
		/// <summary> IsRightPressed
		/// Returns true if the button was clicked with the right mouse button.
		/// </summary>
		public bool IsRightPressed
		{
			get
			{
				return mIsRightPressed;
			}
		}
		/// <summary> IsRightDoubleClicked
		/// Returns true if the buttons was Right DoubleClicked.
		/// </summary>
		public bool IsRightDoubleClicked
		{
			get
			{
				return mIsRightDoubleClicked;
			}
		}
		/// <summary> IsRightHeld
		/// Returns true if the Right Mouse Button was Held on the button.
		/// </summary>
		public bool IsRightHeld
		{
			get
			{
				return mIsRightHeld;
			}
		}
		/// <summary> PCButtonManager
		/// Initalise the button manager
		/// </summary>
		public PCControlManager ( )
		{
			mCurrentActiveControl = null;
			PCMouseManager.MouseMove += new EventHandler ( MouseManager_MouseMove );
			PCMouseManager.LeftMousePress += new EventHandler ( MouseManager_LeftMousePress );
			PCMouseManager.LeftMouseRelease += new EventHandler ( MouseManager_LeftMouseRelease );
			PCMouseManager.LeftMouseDoubleClick += new EventHandler ( MouseManager_LeftMouseDoubleClick );
			PCMouseManager.LeftMouseHeld += new EventHandler ( MouseManager_LeftMouseHeld );
			PCMouseManager.RightMousePress += new EventHandler ( MouseManager_RightMousePress );
			PCMouseManager.RightMouseRelease += new EventHandler ( MouseManager_RightMouseRelease );
			PCMouseManager.RightMouseDoubleClick += new EventHandler ( MouseManager_RightMouseDoubleClick );
			PCMouseManager.RightMouseHeld += new EventHandler ( MouseManager_RightMouseHeld );
			PCMouseManager.Init ( );
			PCKeyboardManager.Init ( );
			mControlGroups = new List<PC_ControlGroup> ( );
		}
		/// <summary> AddControlGroup ( PC_ControlGroup buttonGroup )
		/// Add a set of buttons to the manager
		/// </summary>
		/// <param name="controlGroup">A PC_ControlGroup of buttons. </param>
		public void AddControlGroup ( PC_ControlGroup controlGroup )
		{
			PC_ControlGroup lControlGroup = controlGroup;
			mControlGroups.Add ( lControlGroup );
			mCurrentGroup = lControlGroup;
			mCurrentIndex = lControlGroup.Index;
		}
		/// <summary> AddControlGroup ( Button[ ] buttons, string name )
		/// Add an array of buttons to the manager, which will pit them into a new button group
		/// </summary>
		/// <param name="buttons">An array of PCButtons to add</param>
		/// <param name="name"> String name for the button group</param>
		public void AddControlGroup ( PC_Control[ ] controls, string name )
		{
			PC_ControlGroup controlGroup = new PC_ControlGroup ( controls );
			controlGroup.Name = name;
			controlGroup.Index = mControlGroups.Count;
			mControlGroups.Add ( controlGroup );
			mCurrentGroup = mControlGroups[ mCurrentIndex ];
			mCurrentIndex = controlGroup.Index;
		}
		/// <summary> 
		/// Add a List of buttons to the manager, which will put them into a new button group
		/// </summary>
		/// <param name="controls">A List of PCButtons to add</param>
		/// <param name="name"> String name for the button group</param>
		public void AddControlGroup ( List<PC_Control> controls, string name )
		{
			PC_ControlGroup controlGroup = new PC_ControlGroup ( controls );
			controlGroup.Name = name;
			mCurrentIndex = controlGroup.Index = mControlGroups.Count;
			mCurrentGroup = mControlGroups[ mCurrentIndex ];
			mControlGroups.Add ( controlGroup );
		}
		/// <summary> Draw
		/// Draw all the buttons of the Current Index Group.
		/// </summary>
		/// <param name="spriteBatch"> SpriteBatch pased from the game for drawing operations</param>
		public void Draw ( SpriteBatch spriteBatch )
		{
			mControlGroups[mCurrentIndex ].Draw ( spriteBatch );
		}
		/// <summary> ChangeToButtonGroup
		/// Changes the button manager index to a new group of buttons.
		/// Also releases all the buttons in the current group as well as the
		/// group that is being switched to. This DOES NOT unlock the buttons.
		/// </summary>
		/// <param name="bgIndex"> Index number of the PC_ControlGroup to switch to. </param>
		public void ChangeToControlGroup ( int bgIndex )
		{
			// Release all the buttons in the current group.
			int endpoint = mControlGroups[ mCurrentIndex ].Length;
			for ( int index = 0; index < endpoint; index++ )
			{
                if (mCurrentGroup.mControls[index]._Type == PC_Control.PCControlType.Button)
                {
                    mCurrentGroup.mControls[index].IsReleased();
                }
			}
			// Switch to the new index and group.
			mCurrentIndex = bgIndex;
			mCurrentGroup = mControlGroups[ mCurrentIndex ];
			// Release all the buttons in the new group.
			for ( int index = 0; index < endpoint; index++ )
			{
				mCurrentGroup.mControls[ index ].IsReleased ( );
			}
		}
		/// <summary> UpdateButtons
		/// 
		/// </summary>
		/// <param name="gameTime"></param>
		/// <returns></returns>
		public bool UpdateButtons ( GameTime gameTime )
		{
			Button mHotKeyButton = null;
			PCMouseManager.Update ( gameTime );
			PCKeyboardManager.Update ( gameTime );
            int endpoint = mCurrentGroup.Length;
			// check each buttons hotkey to see if it matches the current keyboard key.
            for (int index = 0; index < endpoint; index++)
            {
                if (mCurrentGroup.mControls[index]._Type == PC_Control.PCControlType.Button)
                {
                    if( mCurrentGroup.CheckForKeypress(PCKeyboardManager.CurrentKey) != null)
                    {
                        mHotKeyButton = (Button)mCurrentGroup.mControls[index];
                        mCurrentActiveControl = mCurrentGroup.mControls[index];
                        mHotKeyButton.IsReleased();
                        return true;
                    }
                }
            }
			// check to see if mouse is touching the control.
			Point lMousePoint = new Point ( PCMouseManager.CurrentMouseState.X, PCMouseManager.CurrentMouseState.Y );
			mCurrentActiveControl = mCurrentGroup.CheckForCollision ( lMousePoint );
			if ( mCurrentActiveControl != null )
			{
				// If it is, see if the button was clicked.
				if ( mCurrentActiveControl._State == PC_Control.PCControlState.pressed )
				{
					mCurrentActiveControl.IsReleased ( );
					return true;
				}
				return false;
			}
			return false;
		}

		private void MouseManager_MouseMove ( object sender, EventArgs e )
		{
			Point mousePosition = PCMouseManager.MousePosition;
			if ( mCurrentActiveControl != null && mCurrentActiveControl.CheckForCollision ( mousePosition ) )
			{
				return;
			}
			else
			{
				if ( mCurrentActiveControl != null )
				{
					//mCurrentActiveControl. ( );
				}
				mCurrentActiveControl = null;
				foreach ( PC_ControlGroup buttonGroup in mControlGroups )
				{
					mCurrentActiveControl = buttonGroup.CheckForCollision ( mousePosition );
					if ( mCurrentActiveControl != null )
					{
						mCurrentActiveControl.MouseHover ( );
					}

				}
			}
		}
		#region Left Mouse Button Event Handlers
		private void MouseManager_LeftMousePress ( object sender, EventArgs e )
		{
			if ( mCurrentActiveControl != null )
			{
				mCurrentActiveControl.LeftMousePress ( );
			}
		}

		private void MouseManager_LeftMouseRelease ( object sender, EventArgs e )
		{
			if ( mCurrentActiveControl != null && mCurrentActiveControl._State == PC_Control.PCControlState.pressed )
			{
				mCurrentActiveControl.LeftMouseRelease ( );
			}
		}

		private void MouseManager_LeftMouseDoubleClick ( object sender, EventArgs e )
		{
			mIsDoubleClicked = true;
		}

		private void MouseManager_LeftMouseHeld ( object sender, EventArgs e )
		{
			mIsMouseHeld = true;
		}

		#endregion
		#region Right Mouse Button Event Handlers
		private void MouseManager_RightMousePress ( object sender, EventArgs e )
		{
            mCurrentActiveControl.RightMousePress();
			mIsRightPressed = true;
		}

		private void MouseManager_RightMouseRelease ( object sender, EventArgs e )
		{
			mCurrentActiveControl.RightMouseRelease ( );
			mIsRightPressed = false;
		}

		private void MouseManager_RightMouseDoubleClick ( object sender, EventArgs e )
		{
			mIsRightDoubleClicked = true;
		}

		private void MouseManager_RightMouseHeld ( object sender, EventArgs e )
		{
			mIsRightHeld = true;
		}
		#endregion

	}
}
