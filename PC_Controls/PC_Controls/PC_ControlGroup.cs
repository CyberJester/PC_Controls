using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace PC_Controls
{
	public class PC_ControlGroup
	{
		public List<PC_Control> mControls;
		private int mLength;
		private int mIndex, mTabIndex;
		private string mName;
		private Guid mUniqueId;

        /// <summary> public int Length {get;}
        /// Returns the number of controls in the group.
        /// </summary>
		public int Length
		{
			get
			{
				return mLength;
			}
		}
		public Guid UniqueId
		{
			get
			{
				return mUniqueId;
			}
			set
			{
				mUniqueId = value;
			}
		}

        /// <summary> public int Index {get;set;}
        /// The groups Index value. Can remain at zero.
        /// </summary>
		public int Index
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
		public int TabIndex
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
		public string Name
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

		private void Init( )
		{
			//create a new unique id for this business object
			mUniqueId = Guid.NewGuid ( );
			mIndex = 0;
			mTabIndex = 0;
			mName = "";
		}

		/// <summary> Default constructor
		/// </summary>
		public PC_ControlGroup ( )
		{
			Init ( );
			mControls = new List<PC_Control> ( );
		}

        /// <summary> PC_ControlGroup ( PC_Control control )
		/// Constructor which allows a single button and a name string to be create a group.
		/// </summary>
		/// <param name="control">The button to put in the group</param>
		public PC_ControlGroup ( PC_Control control )
		{
			Init ( );
			mControls = new List<PC_Control> ( );
			control._Index = 0;
			AddControl ( control );
			mLength = mControls.Count;
		}

        /// <summary> PC_ControlGroup ( List<PC_Control> lControls )
		/// Constructor to create a new control group from a List of controls.
		/// </summary>
		/// <param name="lControls">The buttons to put in the group</param>
		public PC_ControlGroup ( List<PC_Control> lControls )
		{
			Init ( );
			mControls = new List<PC_Control> ( );
			int controlId = 0;
			foreach ( PC_Control control in lControls )
			{
				control._Index = controlId;
				AddControl ( control );
				controlId++;
			}
			mLength = mControls.Count;
		}

        /// <summary> PC_ControlGroup ( PC_Control[ ] lControls )
		/// Constructor to create a new control group from an array of controls.
		/// </summary>
		/// <param name="lControls">The buttons to put in the group</param>
		public PC_ControlGroup ( PC_Control[ ] lControls )
		{
			Init ( );
			mControls = new List<PC_Control> ( );
			for ( int buttonId = 0; buttonId < lControls.Length; buttonId++ )
			{
				lControls[ buttonId ]._Index = buttonId;
				AddControl ( lControls[ buttonId ] );
			}
			mLength = mControls.Count;
		}

        /// <summary> AddControl ( PC_Control control )
		/// Adds a control to the existing control group.
        /// Used when the group is instanciated with a List or Array.
		/// </summary>
		/// <param name="control"> PC_Control Type: control to be added to the group. </param>
		private void AddControl ( PC_Control control )
		{
			mControls.Add ( control );
		}

        /// <summary> PC_Control CheckForCollision ( Point lMousePoint )
		/// Checks for a collision between the mouse and all the buttons in this group
		/// </summary>
		/// <param name="lMousePoint">The position of the mouse</param>
		/// <returns> PC_Control that the mouse is touching or null. </returns>
		public PC_Control CheckForCollision ( Point lMousePoint )
		{
			PC_Control hitControl = null;
			foreach ( PC_Control control in mControls )
			{
                if (control.CheckForCollision(lMousePoint))
                {
                    hitControl = control;
                    break;
                }
			}
			return hitControl;
		}

        /// <summary> PC_Control CheckForKeypress ( Keys currentKey )
        /// This method scans the group for any PC_Button types and then checks if the
        /// currentKey equals their hotKey (if assigned one).
        /// WARNING: This is ONLY USED ON BUTTON TYPE CONTROLS.
        /// </summary>
        /// <param name="currentKey"> Keys Type.</param>
        /// <returns> PC_Control Type:</returns>
		public PC_Control CheckForKeypress ( Keys currentKey )
		{
			PC_Control hitControl = null;
			foreach ( PC_Control control in mControls )
			{
                if(control._Type == PC_Control.PCControlType.Button)
                {
                    Button button = (Button)control;
                    if (button.HotKey == currentKey)
                    {
                        hitControl = (PC_Control)button;
                        break;
                    }
                }
			}
			return hitControl;
		}

		/// <summary>
		/// Draw all the buttons in this group
		/// </summary>
		/// <param name="spriteBatch">The sprite batch to draw the buttons with</param>
		public void Draw ( SpriteBatch spriteBatch )
		{
			foreach ( PC_Control control in mControls )
			{
				control.Draw ( spriteBatch );
			}
		}
	}
}

