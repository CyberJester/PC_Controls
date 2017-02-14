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
	/// <summary> PCLabel
	/// The PCLabel class does not change states when the mouse is passed over it or clicks on it,
	/// so the is no access to the base class ControlState.
	/// </summary>
	public class Label : PC_Control
	{
		private string mLabelText;
		private SpriteFont mFont;
		private Vector2 mLocation;
		private Point mSize;
		private Color mColor;
		private ControlStateArgs ControlState = new ControlStateArgs ();

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
		/// <summary>
		/// 
		/// </summary>
		public string Text
		{
			get
			{
				return mLabelText;
			}
			set
			{
				mLabelText = value;
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
		#endregion
		/// <summary>
		/// Empty constructor.
		/// </summary>
		public Label ( )
		{
		}
		/// <summary>
		/// Main constructor.
		/// </summary>
		/// <param name="Text">Text for  the Label to display.</param>
		/// <param name="Location">Vector2 position of the label.</param>
		/// <param name="Font">SpriteFont to use.</param>
		/// <param name="FontSize">Point width and height of the font.</param>
		/// <param name="FontColor">Color of the font.</param>
		public Label ( string Text, Vector2 Location, SpriteFont Font, Point FontSize, Color FontColor )
		{
			base._Type = PCControlType.Label;
			ControlState.NewControlState = PCControlState.released;
			mLabelText = Text;
			mLocation = Location;
			mFont = Font;
			mSize = FontSize;
			mColor = FontColor;

		}
		/// <summary>
		/// Override of the base class abstract method.
		/// </summary>
		/// <param name="spriteBactch"> SpriteBatch used for the draw operation.</param>
		public override void Draw ( SpriteBatch spriteBactch )
		{
			spriteBactch.DrawString ( mFont, mLabelText, mLocation, mColor );
		}
		/// <summary>
		/// Empty Method holder. Required for Implementation of the base class abstract method.
		/// </summary>
		/// <param name="spriteBatch"></param>
		/// <param name="font"></param>
		/// <param name="fontHeight"></param>
		public override void DrawLabel ( SpriteBatch spriteBatch, SpriteFont font, int fontHeight )
		{
		}
		/// <summary>
		/// Empty Method holder. Required for Implementation of the base class abstract method.
		/// Always returns false;
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public override bool CheckForCollision ( Point position )
		{
			return false;
		}
		public override void Update ( )
		{
		}
	}
}
