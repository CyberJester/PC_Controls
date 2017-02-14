using System;
using System.Collections;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PC_Controls;
using MonoGame.Utilities.Png;
//using PC_ScreenManager;

namespace Test
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ControlTest : Microsoft.Xna.Framework.Game
	{
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private SpriteFont Courier_12, Courier_14, Courier_20;
		private StringBuilder stringBuilder;
		private PC_Control mCurrentActiveControl;

		private Checkbox CheckBox1, CheckBox2;
		private RadioButton RadioButton1, RadioButton2;
		private TextBox TextBox1;
		private Label TextBoxLabel;
		private Button Button1;
		private Checkbox Button1DisableCHKBX;
		private NumericUpDn NumericUpDn1;

		private Texture2D ControlTexture;
		private Texture2D Background;
		private Texture2D Cursors;
		private MouseState mouseState;
		private Keys mCurrentKey;
		private Point mousePosition;
		private bool mIsDoubleClicked;
		private bool mIsMouseHeld;
		private bool mIsRightPressed;
		private bool mIsRightDoubleClicked;
		private bool mIsRightHeld;
		private ControlStateArgs ControlState = new ControlStateArgs();

		private ArrayList ControlList;

		public ControlTest ( )
		{
			graphics = new GraphicsDeviceManager ( this );
			Content.RootDirectory = "Content";
			stringBuilder = new StringBuilder ( );
			graphics.PreferredBackBufferWidth = 1024;
			graphics.PreferredBackBufferHeight = 700;
			graphics.ApplyChanges ( );
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ( )
		{
			//this.IsMouseVisible = true;
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
			base.Initialize ( );
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ( )
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch ( GraphicsDevice );
			ControlTexture = Content.Load<Texture2D> ( "Textures\\ControlGraphics" );
			Background = Content.Load<Texture2D> ( "Textures\\Background" );
			Cursors = Content.Load<Texture2D> ( "Textures\\Cursors" );
			PCMouseManager.CreateCursor ( Cursors, new Point ( 168, 0 ), new Point ( 16, 16 ) );
			PCMouseManager.ShowCursor ( );
			Courier_12 = Content.Load<SpriteFont> ( "Fonts\\Courier_New_12_Reg" );
			Courier_14 = Content.Load<SpriteFont> ( "Fonts\\Courier_New_14_Normal" );
			Courier_20 = Content.Load<SpriteFont> ( "Fonts\\Courier_New_20_Bold" );
			ControlList = new ArrayList ( );
			CreateControls ( );
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent ( )
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update ( GameTime gameTime )
		{
			// Allows the game to exit
			if ( GamePad.GetState ( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed )
				this.Exit ( );
			PCMouseManager.Update ( gameTime );
			mouseState = PCMouseManager.CurrentMouseState;
			PCKeyboardManager.Update ( gameTime );
			mCurrentKey = PCKeyboardManager.CurrentKey;
			base.Update ( gameTime );
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw ( GameTime gameTime )
		{
			GraphicsDevice.Clear ( Color.CornflowerBlue );
			spriteBatch.Begin ( );
			{
				//spriteBatch.Draw ( Background, new Rectangle ( 0, 0, Background.Width, Background.Height ), Color.White );
				for ( int index = 0; index < ControlList.Count; index++ )
				{
					#region
					PC_Control control = (PC_Control)ControlList[index];
					switch ( control._Type )
					{
						case PC_Control.PCControlType.TextBox:
						#region
						{
							control.Draw ( spriteBatch );
						}
						break;
						#endregion
						case PC_Control.PCControlType.Button:
						#region
						{
							control.Draw ( spriteBatch );
							control.DrawLabel ( spriteBatch, Courier_12, 12 );
						}
						break;
						#endregion
						case PC_Control.PCControlType.CheckBox:
						#region
						{
							control.Draw(spriteBatch);
							control.DrawLabel ( spriteBatch, Courier_12, 12 );
						}
						break;
						#endregion
						case PC_Control.PCControlType.HScrollBar:
						#region
						{
							/// TODO: HScrollBar
						}
						break;
						#endregion
						case PC_Control.PCControlType.HSlider:
						#region
						{
							/// TODO: HSlider
						}
						break;
						#endregion
						case PC_Control.PCControlType.Label:
						#region
						{
							// This allows for the updating of labels on the screen.
							if ( control._Name == "TextBox1Label" )
							#region
							{
								Label label = ( Label ) ControlList[ index ];
								for ( int Lindex = 0; Lindex < ControlList.Count; Lindex++ )
								{
									PC_Control Control = ( PC_Control ) ControlList[ Lindex ];
									if ( Control._Type == PC_Control.PCControlType.TextBox )
									{
										if ( Control._Name == "TextBox 1" )
										{
											TextBox Tbox = ( TextBox ) ControlList[ Lindex ];
											string textboxtext = Tbox.Text;
											string labeltext = "TextBox Text is : ";
											labeltext = labeltext.Insert ( labeltext.Length, textboxtext );
											label.Text = labeltext;
										}
									}
								}
							}
							#endregion
							control.Draw ( spriteBatch );
						}
						break;
						#endregion
						case PC_Control.PCControlType.ListBox:
						#region
						{
							/// TODO: ListBox
						}
						break;
						#endregion
						case PC_Control.PCControlType.MenuStrip:
						#region
						{
							/// TODO: MenuStrip
						}
						break;
						#endregion
						case PC_Control.PCControlType.MessageBox:
						#region
						{
							/// TODO: MessageBox
						}
						break;
						#endregion
						case PC_Control.PCControlType.NumericUpDn:
						#region
						{
							control.Draw(spriteBatch);
							control.DrawLabel(spriteBatch, Courier_12, 12);
						}
						break;
						#endregion
						case PC_Control.PCControlType.OpenFileDialog:
						#region
						{
							/// TODO: OpenFileDialog
						}
						break;
						#endregion
						case PC_Control.PCControlType.ProgressBar:
						#region
						{
							/// TODO: ProgressBar
						}
						break;
						#endregion
						case PC_Control.PCControlType.RadioButton:
						#region
						{
							control.Draw ( spriteBatch );
							control.DrawLabel ( spriteBatch, Courier_12, 12 );
						}
						break;
						#endregion
						case PC_Control.PCControlType.SaveFileDialog:
						#region
						{
							/// TODO: SaveFileDialog
						}
						break;
						#endregion
						case PC_Control.PCControlType.StatusStrip:
						#region
						{
							/// TODO: StatusStrip
						}
						break;
						#endregion
						case PC_Control.PCControlType.ToolTip:
						#region
						{
							/// TODO: ToolTip
						}
						break;
						#endregion
						case PC_Control.PCControlType.VScrollBar:
						#region
						{
							/// TODO: VScrollBar
						}
						break;
						#endregion
						case PC_Control.PCControlType.VSlider:
						#region
						{
							/// TODO: VSlider
						}
						break;
						#endregion
					}
					#endregion
				}
			}
			PCMouseManager.DrawCursor ( spriteBatch );
			spriteBatch.End ( );
			base.Draw ( gameTime );
		}
		/// <summary>
		/// Create Controls: The section of the program where the controls are created. SEE SPECIAL REMARAKS.
		/// </summary>
		private void CreateControls ( )
		{
			Point GraphicsOrigin = new Point ( );
			Rectangle DrawRectangle = new Rectangle ( );
			#region CheckBox1
			{
				GraphicsOrigin = new Point ( 720, 408 );
				DrawRectangle = new Rectangle ( 20, 80, 24, 24 );
				CheckBox1 = new Checkbox ( ControlTexture, GraphicsOrigin, DrawRectangle );
				CheckBox1.Name = "CheckBox 1";
				CheckBox1.Label = "CheckBox 1 is : Unchecked.";
				CheckBox1.Index = ControlList.Count;
				ControlList.Add ( CheckBox1 );
			}
			#endregion
			#region CheckBox2
			{
				GraphicsOrigin = new Point ( 720, 408 );
				DrawRectangle = new Rectangle ( 20, 110, 24, 24 );
				CheckBox2 = new Checkbox ( ControlTexture, GraphicsOrigin, DrawRectangle );
				CheckBox2.Name = "CheckBox 2";
				CheckBox2.Label = "CheckBox 2 Disables CheckBox 1.";
				CheckBox2.Index = ControlList.Count;
				ControlList.Add ( CheckBox2 );
			}
			#endregion
			#region RadioButton1
			{
				GraphicsOrigin = new Point ( 720, 432 );
				DrawRectangle = new Rectangle ( 20, 140, 24, 24 );
				RadioButton1 = new RadioButton ( ControlTexture, GraphicsOrigin, DrawRectangle );
				RadioButton1.Name = "RadioButton 1";
				RadioButton1.Label = "RadioButton 1 is : Unchecked.";
				RadioButton1.Index = ControlList.Count;
				ControlList.Add ( RadioButton1 );
			}
			#endregion
			#region RadioButton2
			{
				GraphicsOrigin = new Point ( 720, 432 );
				DrawRectangle = new Rectangle ( 20, 170, 24, 24 );
				RadioButton2 = new RadioButton ( ControlTexture, GraphicsOrigin, DrawRectangle );
				RadioButton2.Name = "RadioButton 2";
				RadioButton2.Label = "RadioButton 2 Disables RadioButton 1.";
				RadioButton2.Index = ControlList.Count;
				ControlList.Add ( RadioButton2 );
			}
			#endregion
			/// <remarks> TextBox1
			#region
			/// This is an example of the usage of the PCTextBox Control. The control has the ability
			/// to show the internal cursor in several different "modes". It can be either a standard
			/// vertical cursor, a underline type horizontal cursor, or a "block" style replace cursor.
			/// The replace cursor is set/released with the insert key. The Horizontal/Vertical style
			/// must be set by the programmer, or defaults to a vertical style cursor. In this example
			/// I have set the cursor style to horizontal. Comment out the line 
			/// TextBox1.Cursor_Style = PCTextBox.CursorStyle.Horizontal; and the cursor will default
			/// to its vertical style.
			/// </remarks>
			#endregion
			#region TextBox1
			{
				GraphicsOrigin = new Point ( 0, 468 );
				DrawRectangle = new Rectangle ( 20, 200, 308, 28 );
				TextBox1 = new TextBox 
					( ControlTexture, GraphicsOrigin, DrawRectangle,Courier_20,new Point (16,20),Color.Black );
				TextBox1.Name = "TextBox 1";
				TextBox1.Index = ControlList.Count;
				TextBox1.Cursor_Style = TextBox.CursorStyle.Horizontal;
				ControlList.Add ( TextBox1 );
			}
			#endregion
			#region TextBoxLabel
			{
				Vector2 Location = new Vector2 ( 20, 234 );
				TextBoxLabel = new Label
	( "TextBox Text is : ", Location, Courier_12, new Point ( 12, 12 ), Color.White );
				TextBoxLabel._Name = "TextBox1Label";
				ControlList.Add ( TextBoxLabel );
			}
			#endregion
			#region Button1
			{
				GraphicsOrigin = new Point ( 0, 240 );
				DrawRectangle = new Rectangle ( 20, 256, 84, 48 );
				Button1 = new Button ( ControlTexture, GraphicsOrigin, DrawRectangle );
				Button1.Name = "Button 1";
				Button1.Label = "Button 1 : Has Not been clicked.";
				Button1.Index = ControlList.Count;
				ControlList.Add ( Button1 );
			}
			#endregion
			#region Button1DisableCHKBX
			{
				GraphicsOrigin = new Point ( 720, 408 );
				DrawRectangle = new Rectangle ( 20, 312, 24, 24 );
				Button1DisableCHKBX = new Checkbox ( ControlTexture, GraphicsOrigin, DrawRectangle );
				Button1DisableCHKBX.Name = "CheckBox 3";
				Button1DisableCHKBX.Label = "CheckBox 3 Disables and Resets Button 1.";
				Button1DisableCHKBX.Index = ControlList.Count;
				ControlList.Add ( Button1DisableCHKBX );
			}
			#endregion
			#region NumericUpDn1
			{
				GraphicsOrigin = new Point(816, 408);
				DrawRectangle = new Rectangle(20, 352, 74, 28);
				NumericUpDn1 = new NumericUpDn(ControlTexture, GraphicsOrigin, DrawRectangle, 16, Courier_12, new Point(12, 12), Color.White);
				NumericUpDn1.Name = "NumericUpDn 1";
				NumericUpDn1.Label = "NumericUpDn Control";
				NumericUpDn1.LabelLocation = PC_Control.LabelLocation.Right;
				NumericUpDn1.Index = ControlList.Count;
				ControlList.Add(NumericUpDn1);
			}
			#endregion
		}
		private void MouseManager_MouseMove ( object sender, EventArgs e )
		{
            mousePosition = PCMouseManager.MousePosition;
            foreach (PC_Control control in ControlList)
            {
				mCurrentActiveControl = null;
				if ((control._State == PC_Control.PCControlState.disabled) || (control._State == PC_Control.PCControlState.pressed))
					return;
				if (control.CheckForCollision(mousePosition))
				{
					control.MouseHover();
					mCurrentActiveControl = control;
				}
				else
				{
					control.MouseUnHover();
				}
            }
		}
		#region Left Mouse Button Event Handlers
		private void MouseManager_LeftMousePress ( object sender, EventArgs e )
		{
			mCurrentActiveControl = null; 
			for (int index = 0; index < ControlList.Count; index++)
			{
				PC_Control control = ( PC_Control ) ControlList[ index ];
				if ( control.CheckForCollision ( new Point ( mouseState.X, mouseState.Y ) ) )
				{
					control.LeftMousePress ( );
					mCurrentActiveControl = control; 
					switch (control._Name)
                    { 
                        case "CheckBox 1":
					#region
					{
						Checkbox ChkBox = (Checkbox)ControlList[index];
						if (ChkBox.State != PC_Control.PCControlState.disabled)
						{
							if (ChkBox.State == PC_Control.PCControlState.pressed)
							{
								ChkBox.Label = "CheckBox 1 is : Checked.";
								return;
							}
							if (ChkBox.State == PC_Control.PCControlState.released)
							{
								ChkBox.Label = "CheckBox 1 is : Unchecked.";
								return;
							}
							if (ChkBox.State == PC_Control.PCControlState.hover)
							{
								ChkBox.State = PC_Control.PCControlState.pressed;
								ChkBox.Label = "CheckBox 1 is : Checked.";
								return;
							}
						}
					}
                    break;
					#endregion
                        case "RadioButton 1":
					#region
					{
						RadioButton RdoBtn = (RadioButton)ControlList[index];
						if (RdoBtn.State != PC_Control.PCControlState.disabled)
						{
							if (RdoBtn.State == PC_Control.PCControlState.pressed)
							{
								RdoBtn.Label = "RadioButton 1 is : Checked.";
								return;
							}
							if (RdoBtn.State == PC_Control.PCControlState.released)
							{
								RdoBtn.Label = "RadioButton 1 is : Unchecked.";
								return;
							}
						}
					}
                    break;
					#endregion
					/// This area handles the Enabling/Disabling of CheckBox1, RadioButton1, and Button1
					/// by CheckBox2, RadioButton2, and CheckBox3.
                        case "CheckBox 2":
					#region
					{
						Checkbox Chkbox2 = (Checkbox)ControlList[index];
						for (int Sindex = 0; Sindex < ControlList.Count; Sindex++)
						{
							PC_Control Scontrol = (PC_Control)ControlList[Sindex];
							if ( Scontrol._Name == "CheckBox 1" )
							{
								Checkbox Chkbox1 = (Checkbox)Scontrol;
								if (Chkbox2.State == PC_Control.PCControlState.pressed)
								{
									Chkbox1.State = PC_Control.PCControlState.disabled;
									Chkbox1.Label = "CheckBox 1 is : Disabled.";
									return;
								}
								if (Chkbox2.State == PC_Control.PCControlState.released)
								{
									Chkbox1.State = PC_Control.PCControlState.released;
									Chkbox1.Label = "CheckBox 1 is : Unchecked.";
									return;
								}
							}
						}
					}
                    break;
					#endregion
                        case "RadioButton 2":
					#region
					{
						RadioButton RdoBtn2 = (RadioButton)ControlList[index];
						for (int Sindex = 0; Sindex < ControlList.Count; Sindex++)
						{
							PC_Control Scontrol = ( PC_Control ) ControlList[ Sindex ];
							if ( Scontrol._Name == "RadioButton 1" )
							{
								RadioButton RdoBtn1 = (RadioButton)Scontrol;
								if (RdoBtn2.State == PC_Control.PCControlState.pressed)
								{
									RdoBtn1.State = PC_Control.PCControlState.disabled;
									RdoBtn1.Label = "RadioButton 1 is : Disabled.";
									return;
								}
								if (RdoBtn2.State == PC_Control.PCControlState.released)
								{
									RdoBtn1.State = PC_Control.PCControlState.released;
									RdoBtn1.Label = "RadioButton 1 is : Unchecked.";
									return;
								}
							}
						}
					}
                    break;
					#endregion
                        case "Button 1":
					#region
					{
						Button btn = ( Button ) ControlList[ index ];
						if ( btn._State != PC_Control.PCControlState.disabled )
						{
							if ( btn.WasLeftClicked || btn.WasMiddleClicked || btn.WasRightClicked )
								btn.Label = "Button 1 : Has Been Clicked.";
							else
							    btn.Label = "Button 1 : Has Not Been Clicked.";
						}
					}
                    break;
					#endregion
                        case "CheckBox 3":
					#region
					{
						Checkbox Chkbox3 = (Checkbox)ControlList[index];
						for (int Sindex = 0; Sindex < ControlList.Count; Sindex++)
						{
							PC_Control Scontrol = ( PC_Control ) ControlList[ Sindex ];
							if ( Scontrol._Name == "Button 1" )
							{
								Button btn = (Button)Scontrol;
								if (Chkbox3.State == PC_Control.PCControlState.pressed)
								{
									btn.State = PC_Control.PCControlState.disabled;
									btn.Label = "Button 1 : Has Been Disabled.";
								}
								else
								{
									btn.IsReleased();
									btn.Label = "Button 1 : Has Not Been Clicked.";
								}
							}
						}
					}
                    break;
					#endregion
						case "TextBox 1":
					{
						TextBox tBox = (TextBox)control;
						tBox.Click(mouseState);
					}
					break;
                   }
				}
			}
		}

		private void MouseManager_LeftMouseRelease ( object sender, EventArgs e )
		{
			if ((int)mCurrentActiveControl._Type > 6)
			{
				mCurrentActiveControl.IsReleased();
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
		}

		private void MouseManager_RightMouseRelease ( object sender, EventArgs e )
		{
			//if ( CheckBox1.ControlState == PC_Control.PC_ControlState.pressed )
			//{
			//    CheckBox1.MouseReleased ( );  // Comment this statement to "Turn Off" Right-Clicks.
			//    mIsRightPressed = false;
			//}
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
