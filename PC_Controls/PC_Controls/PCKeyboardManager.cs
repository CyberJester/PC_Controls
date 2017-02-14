using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PC_Controls
{
	public class PCKeyboardManager
	{
		private static KeyboardState mCurrentKeyboardState, mPreviousKeyboardState;
		private static int[ ] keyDuration;
		private static Keys currentKey;
		private static Keys currentHeldKey;
		public enum KeyPressState
		{
			Pressed,
			Released
		}
		private static KeyPressState mCurrentKeyPressState;

		public static KeyPressState CurrentKeyPressState
		{
			get
			{
				return mCurrentKeyPressState;
			}
		}
		
		/// <summary>
		/// The current state of the keyboard as of the last time the update method was caled
		/// </summary>
		public static KeyboardState CurrentKeyboardState
		{
			get
			{
				return mCurrentKeyboardState;
			}
		}

		/// <summary>
		/// The state of the keyboard from the time before the last update call
		/// </summary>
		public static KeyboardState PreviousKeyboardState
		{
			get
			{
				return mPreviousKeyboardState;
			}
		}

		/// <summary>
		/// The Key that was pressed during the latest update call.
		/// </summary>
		public static Keys CurrentKey
		{
			get
			{
				return currentKey;
			}
		}
		public static Keys CurrentHeldKey
		{
			get
			{
				return currentHeldKey;
			}
		}
		/// <summary>
		/// Occurs once when any key on the keyboard is pressed
		/// </summary>
		public static event EventHandler KeyPressed = delegate
		{
		};
		/// <summary>
		/// Occurs when any key on the keyboard is held
		/// </summary>
		public static event EventHandler KeyHeld = delegate
		{
		};

		#region Individual Key Pressed Events
		/// <summary>
		/// BackKeyPressed Event.
		/// </summary>
		public static event EventHandler BackKeyPressed = delegate{};
		/// <summary>
		/// TabKeyPressed Event.
		/// </summary>
		public static event EventHandler TabKeyPressed = delegate{};
		/// <summary>
		/// EnterKeyPressed Event.
		/// </summary>
		public static event EventHandler EnterKeyPressed = delegate{};
		/// <summary>
		/// PauseKeyPressed Event.
		/// </summary>
		public static event EventHandler PauseKeyPressed = delegate{};
		/// <summary>
		/// CpsLockKeyPressed Event.
		/// </summary>
		public static event EventHandler CapsLockKeyPressed = delegate{};
		/// <summary>
		/// KanaKeyPressed Event.
		/// </summary>
		public static event EventHandler KanaKeyPressed = delegate{};
		/// <summary>
		/// KanjiKeyPressed Event.
		/// </summary>
		public static event EventHandler KanjiKeyPressed = delegate{};
		/// <summary>
		/// EscapeKeyPressed Event.
		/// </summary>
		public static event EventHandler EscapeKeyPressed = delegate{};
		/// <summary>
		/// ImeConvertKeyPressed Event.
		/// </summary>
		public static event EventHandler ImeConvertKeyPressed = delegate{};
		/// <summary>
		/// ImeNoConvertKeyPressed Event.
		/// </summary>
		public static event EventHandler ImeNoConvertKeyPressed = delegate{};
		/// <summary>
		/// SpaceKeyPressed Event.
		/// </summary>
		public static event EventHandler SpaceKeyPressed = delegate{};
		/// <summary>
		/// PageUpKeyPressed Event.
		/// </summary>
		public static event EventHandler PageUpKeyPressed = delegate{};
		/// <summary>
		/// PageDownKeyPressed Event.
		/// </summary>
		public static event EventHandler PageDownKeyPressed = delegate{};
		/// <summary>
		/// EndKeyPressed Event.
		/// </summary>
		public static event EventHandler EndKeyPressed = delegate{};
		/// <summary>
		/// HomeKeyPressed Event.
		/// </summary>
		public static event EventHandler HomeKeyPressed = delegate{};
		/// <summary>
		/// LeftKeyPressed Event.
		/// </summary>
		public static event EventHandler LeftKeyPressed = delegate{};
		/// <summary>
		/// UpKeyPressed Event.
		/// </summary>
		public static event EventHandler UpKeyPressed = delegate{};
		/// <summary>
		/// RightKeyPressed Event.
		/// </summary>
		public static event EventHandler RightKeyPressed = delegate{};
		/// <summary>
		/// DownKeyPressed Event.
		/// </summary>
		public static event EventHandler DownKeyPressed = delegate{};
		/// <summary>
		/// SelectKeyPressed Event.
		/// </summary>
		public static event EventHandler SelectKeyPressed = delegate{};
		/// <summary>
		/// PrintKeyPressed Event.
		/// </summary>
		public static event EventHandler PrintKeyPressed = delegate{};
		/// <summary>
		/// ExecuteKeyPressed Event.
		/// </summary>
		public static event EventHandler ExecuteKeyPressed = delegate{};
		/// <summary>
		/// PrintScreenKeyPressed Event.
		/// </summary>
		public static event EventHandler PrintScreenKeyPressed = delegate{};
		/// <summary>
		/// InsertKeyPressed Event.
		/// </summary>
		public static event EventHandler InsertKeyPressed = delegate{};
		/// <summary>
		/// DeleteKeyPressed Event.
		/// </summary>
		public static event EventHandler DeleteKeyPressed = delegate{};
		/// <summary>
		/// HelpKeyPressed Event.
		/// </summary>
		public static event EventHandler HelpKeyPressed = delegate{};
		/// <summary>
		/// D0KeyPressed Event.
		/// </summary>
		public static event EventHandler D0KeyPressed = delegate{};
		/// <summary>
		/// D1KeyPressed Event.
		/// </summary>
		public static event EventHandler D1KeyPressed = delegate{};
		/// <summary>
		/// D2KeyPressed Event.
		/// </summary>
		public static event EventHandler D2KeyPressed = delegate{};
		/// <summary>
		/// D3KeyPressed Event.
		/// </summary>
		public static event EventHandler D3KeyPressed = delegate{};
		/// <summary>
		/// D4KeyPressed Event.
		/// </summary>
		public static event EventHandler D4KeyPressed = delegate{};
		/// <summary>
		/// D5KeyPressed Event.
		/// </summary>
		public static event EventHandler D5KeyPressed = delegate{};
		/// <summary>
		/// D6KeyPressed Event.
		/// </summary>
		public static event EventHandler D6KeyPressed = delegate{};
		/// <summary>
		/// D7KeyPressed Event.
		/// </summary>
		public static event EventHandler D7KeyPressed = delegate{};
		/// <summary>
		/// D8KeyPressed Event.
		/// </summary>
		public static event EventHandler D8KeyPressed = delegate{};
		/// <summary>
		/// D9KeyPressed Event.
		/// </summary>
		public static event EventHandler D9KeyPressed = delegate{};
		/// <summary>
		/// AKeyPressed Event.
		/// </summary>
		public static event EventHandler AKeyPressed = delegate{};
		/// <summary>
		/// BKeyPressed Event.
		/// </summary>
		public static event EventHandler BKeyPressed = delegate{};
		/// <summary>
		/// CKeyPressed Event.
		/// </summary>
		public static event EventHandler CKeyPressed = delegate{};
		/// <summary>
		/// DKeyPressed Event.
		/// </summary>
		public static event EventHandler DKeyPressed = delegate{};
		/// <summary>
		/// EKeyPressed Event.
		/// </summary>
		public static event EventHandler EKeyPressed = delegate{};
		/// <summary>
		/// FKeyPressed Event.
		/// </summary>
		public static event EventHandler FKeyPressed = delegate{};
		/// <summary>
		/// GKeyPressed Event.
		/// </summary>
		public static event EventHandler GKeyPressed = delegate{};
		/// <summary>
		/// HKeyPressed Event.
		/// </summary>
		public static event EventHandler HKeyPressed = delegate{};
		/// <summary>
		/// IKeyPressed Event.
		/// </summary>
		public static event EventHandler IKeyPressed = delegate{};
		/// <summary>
		/// JKeyPressed Event.
		/// </summary>
		public static event EventHandler JKeyPressed = delegate{};
		/// <summary>
		/// KKeyPressed Event.
		/// </summary>
		public static event EventHandler KKeyPressed = delegate{};
		/// <summary>
		/// LKeyPressed Event.
		/// </summary>
		public static event EventHandler LKeyPressed = delegate{};
		/// <summary>
		/// MKeyPressed Event.
		/// </summary>
		public static event EventHandler MKeyPressed = delegate{};
		/// <summary>
		/// NKeyPressed Event.
		/// </summary>
		public static event EventHandler NKeyPressed = delegate{};
		/// <summary>
		/// OKeyPressed Event.
		/// </summary>
		public static event EventHandler OKeyPressed = delegate{};
		/// <summary>
		/// PKeyPressed Event.
		/// </summary>
		public static event EventHandler PKeyPressed = delegate{};
		/// <summary>
		/// QKeyPressed Event.
		/// </summary>
		public static event EventHandler QKeyPressed = delegate{};
		/// <summary>
		/// RKeyPressed Event.
		/// </summary>
		public static event EventHandler RKeyPressed = delegate{};
		/// <summary>
		/// SKeyPressed Event.
		/// </summary>
		public static event EventHandler SKeyPressed = delegate{};
		/// <summary>
		/// TKeyPressed Event.
		/// </summary>
		public static event EventHandler TKeyPressed = delegate{};
		/// <summary>
		/// UKeyPressed Event.
		/// </summary>
		public static event EventHandler UKeyPressed = delegate{};
		/// <summary>
		/// VKeyPressed Event.
		/// </summary>
		public static event EventHandler VKeyPressed = delegate{};
		/// <summary>
		/// WKeyPressed Event.
		/// </summary>
		public static event EventHandler WKeyPressed = delegate{};
		/// <summary>
		/// XKeyPressed Event.
		/// </summary>
		public static event EventHandler XKeyPressed = delegate{};
		/// <summary>
		/// YKeyPressed Event.
		/// </summary>
		public static event EventHandler YKeyPressed = delegate{};
		/// <summary>
		/// ZKeyPressed Event.
		/// </summary>
		public static event EventHandler ZKeyPressed = delegate{};
		/// <summary>
		/// LeftWindowsKeyPressed Event.
		/// </summary>
		public static event EventHandler LeftWindowsKeyPressed = delegate{};
		/// <summary>
		/// RightWindowsKeyPressed Event.
		/// </summary>
		public static event EventHandler RightWindowsKeyPressed = delegate{};
		/// <summary>
		/// AppsKeyPressed Event.
		/// </summary>
		public static event EventHandler AppsKeyPressed = delegate{};
		/// <summary>
		/// SleepKeyPressed Event.
		/// </summary>
		public static event EventHandler SleepKeyPressed = delegate{};
		/// <summary>
		/// NumPad0KeyPressed Event.
		/// </summary>
		public static event EventHandler NumPad0KeyPressed = delegate{};
		/// <summary>
		/// NumPad1KeyPressed Event.
		/// </summary>
		public static event EventHandler NumPad1KeyPressed = delegate{};
		/// <summary>
		/// NumPad2KeyPressed Event.
		/// </summary>
		public static event EventHandler NumPad2KeyPressed = delegate{};
		/// <summary>
		/// NumPad3KeyPressed Event.
		/// </summary>
		public static event EventHandler NumPad3KeyPressed = delegate{};
		/// <summary>
		/// NumPad4KeyPressed Event.
		/// </summary>
		public static event EventHandler NumPad4KeyPressed = delegate{};
		/// <summary>
		/// NumPad5KeyPressed Event.
		/// </summary>
		public static event EventHandler NumPad5KeyPressed = delegate{};
		/// <summary>
		/// NumPad6KeyPressed Event.
		/// </summary>
		public static event EventHandler NumPad6KeyPressed = delegate{};
		/// <summary>
		/// NumPad7KeyPressed Event.
		/// </summary>
		public static event EventHandler NumPad7KeyPressed = delegate{};
		/// <summary>
		/// NumPad8KeyPressed Event.
		/// </summary>
		public static event EventHandler NumPad8KeyPressed = delegate{};
		/// <summary>
		/// NumPad9KeyPressed Event.
		/// </summary>
		public static event EventHandler NumPad9KeyPressed = delegate{};
		/// <summary>
		/// MultiplyKeyPressed Event.
		/// </summary>
		public static event EventHandler MultiplyKeyPressed = delegate{};
		/// <summary>
		/// AddKeyPressed Event.
		/// </summary>
		public static event EventHandler AddKeyPressed = delegate{};
		/// <summary>
		/// SeparatorKeyPressed Event.
		/// </summary>
		public static event EventHandler SeparatorKeyPressed = delegate{};
		/// <summary>
		/// SubtractKeyPressed Event.
		/// </summary>
		public static event EventHandler SubtractKeyPressed = delegate{};
		/// <summary>
		/// DecimalKeyPressed Event.
		/// </summary>
		public static event EventHandler DecimalKeyPressed = delegate{};
		/// <summary>
		/// DivideKeyPressed Event.
		/// </summary>
		public static event EventHandler DivideKeyPressed = delegate{};
		/// <summary>
		/// F1KeyPressed Event.
		/// </summary>
		public static event EventHandler F1KeyPressed = delegate{};
		/// <summary>
		/// F2KeyPressed Event.
		/// </summary>
		public static event EventHandler F2KeyPressed = delegate{};
		/// <summary>
		/// F3KeyPressed Event.
		/// </summary>
		public static event EventHandler F3KeyPressed = delegate{};
		/// <summary>
		/// F4KeyPressed Event.
		/// </summary>
		public static event EventHandler F4KeyPressed = delegate{};
		/// <summary>
		/// F5KeyPressed Event.
		/// </summary>
		public static event EventHandler F5KeyPressed = delegate{};
		/// <summary>
		/// F6KeyPressed Event.
		/// </summary>
		public static event EventHandler F6KeyPressed = delegate{};
		/// <summary>
		/// F7KeyPressed Event.
		/// </summary>
		public static event EventHandler F7KeyPressed = delegate{};
		/// <summary>
		/// F8KeyPressed Event.
		/// </summary>
		public static event EventHandler F8KeyPressed = delegate{};
		/// <summary>
		/// F9KeyPressed Event.
		/// </summary>
		public static event EventHandler F9KeyPressed = delegate{};
		/// <summary>
		/// F10KeyPressed Event.
		/// </summary>
		public static event EventHandler F10KeyPressed = delegate{};
		/// <summary>
		/// F11KeyPressed Event.
		/// </summary>
		public static event EventHandler F11KeyPressed = delegate{};
		/// <summary>
		/// F12KeyPressed Event.
		/// </summary>
		public static event EventHandler F12KeyPressed = delegate{};
		/// <summary>
		/// F13KeyPressed Event.
		/// </summary>
		public static event EventHandler F13KeyPressed = delegate{};
		/// <summary>
		/// F14KeyPressed Event.
		/// </summary>
		public static event EventHandler F14KeyPressed = delegate{};
		/// <summary>
		/// F15KeyPressed Event.
		/// </summary>
		public static event EventHandler F15KeyPressed = delegate{};
		/// <summary>
		/// F16KeyPressed Event.
		/// </summary>
		public static event EventHandler F16KeyPressed = delegate{};
		/// <summary>
		/// F17KeyPressed Event.
		/// </summary>
		public static event EventHandler F17KeyPressed = delegate{};
		/// <summary>
		/// F18KeyPressed Event.
		/// </summary>
		public static event EventHandler F18KeyPressed = delegate{};
		/// <summary>
		/// F19KeyPressed Event.
		/// </summary>
		public static event EventHandler F19KeyPressed = delegate{};
		/// <summary>
		/// F20KeyPressed Event.
		/// </summary>
		public static event EventHandler F20KeyPressed = delegate{};
		/// <summary>
		/// F21KeyPressed Event.
		/// </summary>
		public static event EventHandler F21KeyPressed = delegate{};
		/// <summary>
		/// F22KeyPressed Event.
		/// </summary>
		public static event EventHandler F22KeyPressed = delegate{};
		/// <summary>
		/// F23KeyPressed Event.
		/// </summary>
		public static event EventHandler F23KeyPressed = delegate{};
		/// <summary>
		/// F24KeyPressed Event.
		/// </summary>
		public static event EventHandler F24KeyPressed = delegate{};
		/// <summary>
		/// NumLockKeyPressed Event.
		/// </summary>
		public static event EventHandler NumLockKeyPressed = delegate{};
		/// <summary>
		/// ScrollKeyKeyPressed Event.
		/// </summary>
		public static event EventHandler ScrollKeyPressed = delegate{};
		/// <summary>
		/// LeftShiftKeyPressed Event.
		/// </summary>
		public static event EventHandler LeftShiftKeyPressed = delegate{};
		/// <summary>
		/// RightShiftKeyPressed Event.
		/// </summary>
		public static event EventHandler RightShiftKeyPressed = delegate{};
		/// <summary>
		/// LeftControlKeyPressed Event.
		/// </summary>
		public static event EventHandler LeftControlKeyPressed = delegate{};
		/// <summary>
		/// RightControlKeyPressed Event.
		/// </summary>
		public static event EventHandler RightControlKeyPressed = delegate{};
		/// <summary>
		/// LeftAltKeyPressed Event.
		/// </summary>
		public static event EventHandler LeftAltKeyPressed = delegate{};
		/// <summary>
		/// RightAltKeyPressed Event.
		/// </summary>
		public static event EventHandler RightAltKeyPressed = delegate{};
		/// <summary>
		/// BrowserBackKeyPressed Event.
		/// </summary>
		public static event EventHandler BrowserBackKeyPressed = delegate{};
		/// <summary>
		/// BrowserForwardKeyPressed Event.
		/// </summary>
		public static event EventHandler BrowserForwardKeyPressed = delegate{};
		/// <summary>
		/// BrowserRefreshKeyPressed Event.
		/// </summary>
		public static event EventHandler BrowserRefreshKeyPressed = delegate{};
		/// <summary>
		/// BrowserStopKeyPressed Event.
		/// </summary>
		public static event EventHandler BrowserStopKeyPressed = delegate{};
		/// <summary>
		/// BrowserSearchKeyPressed Event.
		/// </summary>
		public static event EventHandler BrowserSearchKeyPressed = delegate{};
		/// <summary>
		/// BrowserFavoritesKeyPressed Event.
		/// </summary>
		public static event EventHandler BrowserFavoritesKeyPressed = delegate{};
		/// <summary>
		/// BrowserHomeKeyPressed Event.
		/// </summary>
		public static event EventHandler BrowserHomeKeyPressed = delegate{};
		/// <summary>
		/// VolumeMuteKeyPressed Event.
		/// </summary>
		public static event EventHandler VolumeMuteKeyPressed = delegate{};
		/// <summary>
		/// VolumeDownKeyPressed Event.
		/// </summary>
		public static event EventHandler VolumeDownKeyPressed = delegate{};
		/// <summary>
		/// VolumeUpKeyPressed Event.
		/// </summary>
		public static event EventHandler VolumeUpKeyPressed = delegate{};
		/// <summary>
		/// MediaNextTrackKeyPressed Event.
		/// </summary>
		public static event EventHandler MediaNextTrackKeyPressed = delegate{};
		/// <summary>
		/// MediaPreviousTrackKeyPressed Event.
		/// </summary>
		public static event EventHandler MediaPreviousTrackKeyPressed = delegate{};
		/// <summary>
		/// MediaStopKeyPressed Event.
		/// </summary>
		public static event EventHandler MediaStopKeyPressed = delegate{};
		/// <summary>
		/// MediaPlayPauseKeyPressed Event.
		/// </summary>
		public static event EventHandler MediaPlayPauseKeyPressed = delegate{};
		/// <summary>
		/// LaunchMailKeyPressed Event.
		/// </summary>
		public static event EventHandler LaunchMailKeyPressed = delegate{};
		/// <summary>
		/// SelectMediaKeyPressed Event.
		/// </summary>
		public static event EventHandler SelectMediaKeyPressed = delegate{};
		/// <summary>
		/// LaunchApplication1KeyPressed Event.
		/// </summary>
		public static event EventHandler LaunchApplication1KeyPressed = delegate{};
		/// <summary>
		/// LaunchApplication2KeyPressed Event.
		/// </summary>
		public static event EventHandler LaunchApplication2KeyPressed = delegate{};
		/// <summary>
		/// OemSemicolonKeyPressed Event.
		/// </summary>
		public static event EventHandler OemSemicolonKeyPressed = delegate{};
		/// <summary>
		/// OemPlusKeyPressed Event.
		/// </summary>
		public static event EventHandler OemPlusKeyPressed = delegate{};
		/// <summary>
		/// OemCommaKeyPressed Event.
		/// </summary>
		public static event EventHandler OemCommaKeyPressed = delegate{};
		/// <summary>
		/// OemMinusKeyPressed Event.
		/// </summary>
		public static event EventHandler OemMinusKeyPressed = delegate{};
		/// <summary>
		/// OemPeriodKeyPressed Event.
		/// </summary>
		public static event EventHandler OemPeriodKeyPressed = delegate{};
		/// <summary>
		/// OemQuestionKeyPressed Event.
		/// </summary>
		public static event EventHandler OemQuestionKeyPressed = delegate{};
		/// <summary>
		/// OemTildeKeyPressed Event.
		/// </summary>
		public static event EventHandler OemTildeKeyPressed = delegate{};
		/// <summary>
		/// ChatPadGreenKeyPressed Event.
		/// </summary>
		public static event EventHandler ChatPadGreenKeyPressed = delegate{};
		/// <summary>
		/// ChatPadOrangeKeyPressed Event.
		/// </summary>
		public static event EventHandler ChatPadOrangeKeyPressed = delegate{};
		/// <summary>
		/// OemOpenBracketsKeyPressed Event.
		/// </summary>
		public static event EventHandler OemOpenBracketsKeyPressed = delegate{};
		/// <summary>
		/// OemPipeKeyKeyPressed Event.
		/// </summary>
		public static event EventHandler OemPipeKeyPressed = delegate{};
		/// <summary>
		/// OemCloseBracketsKeyPressed Event.
		/// </summary>
		public static event EventHandler OemCloseBracketsKeyPressed = delegate{};
		/// <summary>
		/// OemQuotesKeyPressed Event.
		/// </summary>
		public static event EventHandler OemQuotesKeyPressed = delegate{};
		/// <summary>
		/// Oem8KeyPressed Event.
		/// </summary>
		public static event EventHandler Oem8KeyPressed = delegate{};
		/// <summary>
		/// OemBackslashKeyPressed Event.
		/// </summary>
		public static event EventHandler OemBackslashKeyPressed = delegate{};
		/// <summary>
		/// ProcessKeyKeyPressed Event.
		/// </summary>
		public static event EventHandler ProcessKeyKeyPressed = delegate{};
		/// <summary>
		/// OemCopyKeyPressed Event.
		/// </summary>
		public static event EventHandler OemCopyKeyPressed = delegate{};
		/// <summary>
		/// OemAutoKeyPressed Event.
		/// </summary>
		public static event EventHandler OemAutoKeyPressed = delegate{};
		/// <summary>
		/// OemEnlWKeyPressed Event.
		/// </summary>
		public static event EventHandler OemEnlWKeyPressed = delegate{};
		/// <summary>
		/// AttnKeyPressed Event.
		/// </summary>
		public static event EventHandler AttnKeyPressed = delegate{};
		/// <summary>
		/// CrselKeyPressed Event.
		/// </summary>
		public static event EventHandler CrselKeyPressed = delegate{};
		/// <summary>
		/// ExselKeyPressed Event.
		/// </summary>
		public static event EventHandler ExselKeyPressed = delegate{};
		/// <summary>
		/// EraseEofKeyPressed Event.
		/// </summary>
		public static event EventHandler EraseEofKeyPressed = delegate{};
		/// <summary>
		/// PlayKeyPressed Event.
		/// </summary>
		public static event EventHandler PlayKeyPressed = delegate{};
		/// <summary>
		/// ZoomKeyPressed Event.
		/// </summary>
		public static event EventHandler ZoomKeyPressed = delegate{};
		#endregion

		#region  Individual Key held events
		/// <summary>
		/// NoneKeyHeld event
		/// </summary>
		public static event EventHandler NoneKeyHeld = delegate{};
		/// <summary>
		/// BackKeyHeld event
		/// </summary>
		public static event EventHandler BackKeyHeld = delegate{};
		/// <summary>
		/// TabKeyHeld event
		/// </summary>
		public static event EventHandler TabKeyHeld = delegate{};
		/// <summary>
		/// EnterKeyHeld event
		/// </summary>
		public static event EventHandler EnterKeyHeld = delegate{};
		/// <summary>
		/// PauseKeyHeld event
		/// </summary>
		public static event EventHandler PauseKeyHeld = delegate{};
		/// <summary>
		/// CapsLockKeyHeld event
		/// </summary>
		public static event EventHandler CapsLockKeyHeld = delegate{};
		/// <summary>
		/// KanaKeyHeld event
		/// </summary>
		public static event EventHandler KanaKeyHeld = delegate{};
		/// <summary>
		/// KanjiKeyHeld event
		/// </summary>
		public static event EventHandler KanjiKeyHeld = delegate{};
		/// <summary>
		/// EscapeKeyHeld event
		/// </summary>
		public static event EventHandler EscapeKeyHeld = delegate{};
		/// <summary>
		/// ImeConvertKeyHeld event
		/// </summary>
		public static event EventHandler ImeConvertKeyHeld = delegate{};
		/// <summary>
		/// ImeNoConvertKeyHeld event
		/// </summary>
		public static event EventHandler ImeNoConvertKeyHeld = delegate{};
		/// <summary>
		/// SpaceKeyHeld event
		/// </summary>
		public static event EventHandler SpaceKeyHeld = delegate{};
		/// <summary>
		/// PageUpKeyHeld event
		/// </summary>
		public static event EventHandler PageUpKeyHeld = delegate{};
		/// <summary>
		/// PageDownKeyHeld event
		/// </summary>
		public static event EventHandler PageDownKeyHeld = delegate{};
		/// <summary>
		/// EndKeyHeld event
		/// </summary>
		public static event EventHandler EndKeyHeld = delegate{};
		/// <summary>
		/// HomeKeyHeld event
		/// </summary>
		public static event EventHandler HomeKeyHeld = delegate{};
		/// <summary>
		/// LeftKeyHeld event
		/// </summary>
		public static event EventHandler LeftKeyHeld = delegate{};
		/// <summary>
		/// UpKeyHeld event
		/// </summary>
		public static event EventHandler UpKeyHeld = delegate{};
		/// <summary>
		/// RightKeyHeld event
		/// </summary>
		public static event EventHandler RightKeyHeld = delegate{};
		/// <summary>
		/// DownKeyHeld event
		/// </summary>
		public static event EventHandler DownKeyHeld = delegate{};
		/// <summary>
		/// SelectKeyHeld event
		/// </summary>
		public static event EventHandler SelectKeyHeld = delegate{};
		/// <summary>
		/// PrintKeyHeld event
		/// </summary>
		public static event EventHandler PrintKeyHeld = delegate{};
		/// <summary>
		/// ExecuteKeyHeld event
		/// </summary>
		public static event EventHandler ExecuteKeyHeld = delegate{};
		/// <summary>
		/// PrintScreenKeyHeld event
		/// </summary>
		public static event EventHandler PrintScreenKeyHeld = delegate{};
		/// <summary>
		/// InsertKeyHeld event
		/// </summary>
		public static event EventHandler InsertKeyHeld = delegate{};
		/// <summary>
		/// DeleteKeyHeld event
		/// </summary>
		public static event EventHandler DeleteKeyHeld = delegate{};
		/// <summary>
		/// HelpKeyHeld event
		/// </summary>
		public static event EventHandler HelpKeyHeld = delegate{};
		/// <summary>
		/// D0KeyHeld event
		/// </summary>
		public static event EventHandler D0KeyHeld = delegate{};
		/// <summary>
		/// D1KeyHeld event
		/// </summary>
		public static event EventHandler D1KeyHeld = delegate{};
		/// <summary>
		/// D2KeyHeld event
		/// </summary>
		public static event EventHandler D2KeyHeld = delegate{};
		/// <summary>
		/// D3KeyHeld event
		/// </summary>
		public static event EventHandler D3KeyHeld = delegate{};
		/// <summary>
		/// D4KeyHeld event
		/// </summary>
		public static event EventHandler D4KeyHeld = delegate{};
		/// <summary>
		/// D5KeyHeld event
		/// </summary>
		public static event EventHandler D5KeyHeld = delegate{};
		/// <summary>
		/// D6KeyHeld event
		/// </summary>
		public static event EventHandler D6KeyHeld = delegate{};
		/// <summary>
		/// D7KeyHeld event
		/// </summary>
		public static event EventHandler D7KeyHeld = delegate{};
		/// <summary>
		/// D8KeyHeld event
		/// </summary>
		public static event EventHandler D8KeyHeld = delegate{};
		/// <summary>
		/// D9KeyHeld event
		/// </summary>
		public static event EventHandler D9KeyHeld = delegate{};
		/// <summary>
		/// AKeyHeld event
		/// </summary>
		public static event EventHandler AKeyHeld = delegate{};
		/// <summary>
		/// BKeyHeld event
		/// </summary>
		public static event EventHandler BKeyHeld = delegate{};
		/// <summary>
		/// CKeyHeld event
		/// </summary>
		public static event EventHandler CKeyHeld = delegate{};
		/// <summary>
		/// DKeyHeld event
		/// </summary>
		public static event EventHandler DKeyHeld = delegate{};
		/// <summary>
		/// EKeyHeld event
		/// </summary>
		public static event EventHandler EKeyHeld = delegate{};
		/// <summary>
		/// FKeyHeld event
		/// </summary>
		public static event EventHandler FKeyHeld = delegate{};
		/// <summary>
		/// GKeyHeld event
		/// </summary>
		public static event EventHandler GKeyHeld = delegate{};
		/// <summary>
		/// HKeyHeld event
		/// </summary>
		public static event EventHandler HKeyHeld = delegate{};
		/// <summary>
		/// IKeyHeld event
		/// </summary>
		public static event EventHandler IKeyHeld = delegate{};
		/// <summary>
		/// JKeyHeld event
		/// </summary>
		public static event EventHandler JKeyHeld = delegate{};
		/// <summary>
		/// KKeyHeld event
		/// </summary>
		public static event EventHandler KKeyHeld = delegate{};
		/// <summary>
		/// LKeyHeld event
		/// </summary>
		public static event EventHandler LKeyHeld = delegate{};
		/// <summary>
		/// MKeyHeld event
		/// </summary>
		public static event EventHandler MKeyHeld = delegate{};
		/// <summary>
		/// NKeyHeld event
		/// </summary>
		public static event EventHandler NKeyHeld = delegate{};
		/// <summary>
		/// OKeyHeld event
		/// </summary>
		public static event EventHandler OKeyHeld = delegate{};
		/// <summary>
		/// PKeyHeld event
		/// </summary>
		public static event EventHandler PKeyHeld = delegate{};
		/// <summary>
		/// QKeyHeld event
		/// </summary>
		public static event EventHandler QKeyHeld = delegate{};
		/// <summary>
		/// RKeyHeld event
		/// </summary>
		public static event EventHandler RKeyHeld = delegate{};
		/// <summary>
		/// SKeyHeld event
		/// </summary>
		public static event EventHandler SKeyHeld = delegate{};
		/// <summary>
		/// TKeyHeld event
		/// </summary>
		public static event EventHandler TKeyHeld = delegate{};
		/// <summary>
		/// UKeyHeld event
		/// </summary>
		public static event EventHandler UKeyHeld = delegate{};
		/// <summary>
		/// VKeyHeld event
		/// </summary>
		public static event EventHandler VKeyHeld = delegate{};
		/// <summary>
		/// WKeyHeld event
		/// </summary>
		public static event EventHandler WKeyHeld = delegate{};
		/// <summary>
		/// XKeyHeld event
		/// </summary>
		public static event EventHandler XKeyHeld = delegate{};
		/// <summary>
		/// YKeyHeld event
		/// </summary>
		public static event EventHandler YKeyHeld = delegate{};
		/// <summary>
		/// ZKeyHeld event
		/// </summary>
		public static event EventHandler ZKeyHeld = delegate{};
		/// <summary>
		/// LeftWindowsKeyHeld event
		/// </summary>
		public static event EventHandler LeftWindowsKeyHeld = delegate{};
		/// <summary>
		/// RightWindowsKeyHeld event
		/// </summary>
		public static event EventHandler RightWindowsKeyHeld = delegate{};
		/// <summary>
		/// AppsKeyHeld event
		/// </summary>
		public static event EventHandler AppsKeyHeld = delegate{};
		/// <summary>
		/// SleepKeyHeld event
		/// </summary>
		public static event EventHandler SleepKeyHeld = delegate{};
		/// <summary>
		/// NumPad0KeyHeld event
		/// </summary>
		public static event EventHandler NumPad0KeyHeld = delegate{};
		/// <summary>
		/// NumPad1KeyHeld event
		/// </summary>
		public static event EventHandler NumPad1KeyHeld = delegate{};
		/// <summary>
		/// NumPad2KeyHeld event
		/// </summary>
		public static event EventHandler NumPad2KeyHeld = delegate{};
		/// <summary>
		/// NumPad3KeyHeld event
		/// </summary>
		public static event EventHandler NumPad3KeyHeld = delegate{};
		/// <summary>
		/// NumPad4KeyHeld event
		/// </summary>
		public static event EventHandler NumPad4KeyHeld = delegate{};
		/// <summary>
		/// NumPad5KeyHeld event
		/// </summary>
		public static event EventHandler NumPad5KeyHeld = delegate{};
		/// <summary>
		/// NumPad6KeyHeld event
		/// </summary>
		public static event EventHandler NumPad6KeyHeld = delegate{};
		/// <summary>
		/// NumPad7KeyHeld event
		/// </summary>
		public static event EventHandler NumPad7KeyHeld = delegate{};
		/// <summary>
		/// NumPad8KeyHeld event
		/// </summary>
		public static event EventHandler NumPad8KeyHeld = delegate{};
		/// <summary>
		/// NumPad9KeyHeld event
		/// </summary>
		public static event EventHandler NumPad9KeyHeld = delegate{};
		/// <summary>
		/// MultiplyKeyHeld event
		/// </summary>
		public static event EventHandler MultiplyKeyHeld = delegate{};
		/// <summary>
		/// AddKeyHeld event
		/// </summary>
		public static event EventHandler AddKeyHeld = delegate{};
		/// <summary>
		/// SeparatorKeyHeld event
		/// </summary>
		public static event EventHandler SeparatorKeyHeld = delegate{};
		/// <summary>
		/// SubtractKeyHeld event
		/// </summary>
		public static event EventHandler SubtractKeyHeld = delegate{};
		/// <summary>
		/// DecimalKeyHeld event
		/// </summary>
		public static event EventHandler DecimalKeyHeld = delegate{};
		/// <summary>
		/// DivideKeyHeld event
		/// </summary>
		public static event EventHandler DivideKeyHeld = delegate{};
		/// <summary>
		/// F1KeyHeld event
		/// </summary>
		public static event EventHandler F1KeyHeld = delegate{};
		/// <summary>
		/// F2KeyHeld event
		/// </summary>
		public static event EventHandler F2KeyHeld = delegate{};
		/// <summary>
		/// F3KeyHeld event
		/// </summary>
		public static event EventHandler F3KeyHeld = delegate{};
		/// <summary>
		/// F4KeyHeld event
		/// </summary>
		public static event EventHandler F4KeyHeld = delegate{};
		/// <summary>
		/// F5KeyHeld event
		/// </summary>
		public static event EventHandler F5KeyHeld = delegate{};
		/// <summary>
		/// F6KeyHeld event
		/// </summary>
		public static event EventHandler F6KeyHeld = delegate{};
		/// <summary>
		/// F7KeyHeld event
		/// </summary>
		public static event EventHandler F7KeyHeld = delegate{};
		/// <summary>
		/// F8KeyHeld event
		/// </summary>
		public static event EventHandler F8KeyHeld = delegate{};
		/// <summary>
		/// F9KeyHeld event
		/// </summary>
		public static event EventHandler F9KeyHeld = delegate{};
		/// <summary>
		/// F10KeyHeld event
		/// </summary>
		public static event EventHandler F10KeyHeld = delegate{};
		/// <summary>
		/// F11KeyHeld event
		/// </summary>
		public static event EventHandler F11KeyHeld = delegate{};
		/// <summary>
		/// F12KeyHeld event
		/// </summary>
		public static event EventHandler F12KeyHeld = delegate{};
		/// <summary>
		/// F13KeyHeld event
		/// </summary>
		public static event EventHandler F13KeyHeld = delegate{};
		/// <summary>
		/// F14KeyHeld event
		/// </summary>
		public static event EventHandler F14KeyHeld = delegate{};
		/// <summary>
		/// F15KeyHeld event
		/// </summary>
		public static event EventHandler F15KeyHeld = delegate{};
		/// <summary>
		/// F16KeyHeld event
		/// </summary>
		public static event EventHandler F16KeyHeld = delegate{};
		/// <summary>
		/// F17KeyHeld event
		/// </summary>
		public static event EventHandler F17KeyHeld = delegate{};
		/// <summary>
		/// F18KeyHeld event
		/// </summary>
		public static event EventHandler F18KeyHeld = delegate{};
		/// <summary>
		/// F19KeyHeld event
		/// </summary>
		public static event EventHandler F19KeyHeld = delegate{};
		/// <summary>
		/// F20KeyHeld event
		/// </summary>
		public static event EventHandler F20KeyHeld = delegate{};
		/// <summary>
		/// F21KeyHeld event
		/// </summary>
		public static event EventHandler F21KeyHeld = delegate{};
		/// <summary>
		/// F22KeyHeld event
		/// </summary>
		public static event EventHandler F22KeyHeld = delegate{};
		/// <summary>
		/// F23KeyHeld event
		/// </summary>
		public static event EventHandler F23KeyHeld = delegate{};
		/// <summary>
		/// F24KeyHeld event
		/// </summary>
		public static event EventHandler F24KeyHeld = delegate{};
		/// <summary>
		/// NumLockKeyHeld event
		/// </summary>
		public static event EventHandler NumLockKeyHeld = delegate{};
		/// <summary>
		/// ScrollKeyHeld event
		/// </summary>
		public static event EventHandler ScrollKeyHeld = delegate{};
		/// <summary>
		/// LeftShiftKeyHeld event
		/// </summary>
		public static event EventHandler LeftShiftKeyHeld = delegate{};
		/// <summary>
		/// RightShiftKeyHeld event
		/// </summary>
		public static event EventHandler RightShiftKeyHeld = delegate{};
		/// <summary>
		/// LeftControlKeyHeld event
		/// </summary>
		public static event EventHandler LeftControlKeyHeld = delegate{};
		/// <summary>
		/// RightControlKeyHeld event
		/// </summary>
		public static event EventHandler RightControlKeyHeld = delegate{};
		/// <summary>
		/// LeftAltKeyHeld event
		/// </summary>
		public static event EventHandler LeftAltKeyHeld = delegate{};
		/// <summary>
		/// RightAltKeyHeld event
		/// </summary>
		public static event EventHandler RightAltKeyHeld = delegate{};
		/// <summary>
		/// BrowserBackKeyHeld event
		/// </summary>
		public static event EventHandler BrowserBackKeyHeld = delegate{};
		/// <summary>
		/// BrowserForwardKeyHeld event
		/// </summary>
		public static event EventHandler BrowserForwardKeyHeld = delegate{};
		/// <summary>
		/// BrowserRefreshKeyHeld event
		/// </summary>
		public static event EventHandler BrowserRefreshKeyHeld = delegate{};
		/// <summary>
		/// BrowserStopKeyHeld event
		/// </summary>
		public static event EventHandler BrowserStopKeyHeld = delegate{};
		/// <summary>
		/// BrowserSearchKeyHeld event
		/// </summary>
		public static event EventHandler BrowserSearchKeyHeld = delegate{};
		/// <summary>
		/// BrowserFavoritesKeyHeld event
		/// </summary>
		public static event EventHandler BrowserFavoritesKeyHeld = delegate{};
		/// <summary>
		/// BrowserHomeKeyKeyHeld event
		/// </summary>
		public static event EventHandler BrowserHomeKeyHeld = delegate{};
		/// <summary>
		/// VolumeMuteKeyHeld event
		/// </summary>
		public static event EventHandler VolumeMuteKeyHeld = delegate{};
		/// <summary>
		/// VolumeDownKeyHeld event
		/// </summary>
		public static event EventHandler VolumeDownKeyHeld = delegate{};
		/// <summary>
		/// VolumeUpKeyHeld event
		/// </summary>
		public static event EventHandler VolumeUpKeyHeld = delegate{};
		/// <summary>
		/// MediaNextTrackKeyHeld event
		/// </summary>
		public static event EventHandler MediaNextTrackKeyHeld = delegate{};
		/// <summary>
		/// MediaPreviousTrackKeyHeld event
		/// </summary>
		public static event EventHandler MediaPreviousTrackKeyHeld = delegate{};
		/// <summary>
		/// MediaStopKeyHeld event
		/// </summary>
		public static event EventHandler MediaStopKeyHeld = delegate{};
		/// <summary>
		/// MediaPlayPauseKeyHeld event
		/// </summary>
		public static event EventHandler MediaPlayPauseKeyHeld = delegate{};
		/// <summary>
		/// LaunchMailKeyHeld event
		/// </summary>
		public static event EventHandler LaunchMailKeyHeld = delegate{};
		/// <summary>
		/// SelectMediaKeyHeld event
		/// </summary>
		public static event EventHandler SelectMediaKeyHeld = delegate{};
		/// <summary>
		/// LaunchApplication1KeyHeld event
		/// </summary>
		public static event EventHandler LaunchApplication1KeyHeld = delegate{};
		/// <summary>
		/// LaunchApplication2KeyHeld event
		/// </summary>
		public static event EventHandler LaunchApplication2KeyHeld = delegate{};
		/// <summary>
		/// OemSemicolonKeyHeld event
		/// </summary>
		public static event EventHandler OemSemicolonKeyHeld = delegate{};
		/// <summary>
		/// OemPlusKeyHeld event
		/// </summary>
		public static event EventHandler OemPlusKeyHeld = delegate{};
		/// <summary>
		/// OemCommaKeyHeld event
		/// </summary>
		public static event EventHandler OemCommaKeyHeld = delegate{};
		/// <summary>
		/// OemMinusKeyHeld event
		/// </summary>
		public static event EventHandler OemMinusKeyHeld = delegate{};
		/// <summary>
		/// OemPeriodKeyHeld event
		/// </summary>
		public static event EventHandler OemPeriodKeyHeld = delegate{};
		/// <summary>
		/// OemQuestionKeyHeld event
		/// </summary>
		public static event EventHandler OemQuestionKeyHeld = delegate{};
		/// <summary>
		/// OemTildeKeyHeld event
		/// </summary>
		public static event EventHandler OemTildeKeyHeld = delegate{};
		/// <summary>
		/// ChatPadGreenKeyHeld event
		/// </summary>
		public static event EventHandler ChatPadGreenKeyHeld = delegate{};
		/// <summary>
		/// ChatPadOrangeKeyHeld event
		/// </summary>
		public static event EventHandler ChatPadOrangeKeyHeld = delegate{};
		/// <summary>
		/// OemOpenBracketsKeyHeld event
		/// </summary>
		public static event EventHandler OemOpenBracketsKeyHeld = delegate{};
		/// <summary>
		/// OemPipeKeyHeld event
		/// </summary>
		public static event EventHandler OemPipeKeyHeld = delegate{};
		/// <summary>
		/// OemCloseBracketsKeyHeld event
		/// </summary>
		public static event EventHandler OemCloseBracketsKeyHeld = delegate{};
		/// <summary>
		/// OemQuotesKeyHeld event
		/// </summary>
		public static event EventHandler OemQuotesKeyHeld = delegate{};
		/// <summary>
		/// Oem8KeyHeld event
		/// </summary>
		public static event EventHandler Oem8KeyHeld = delegate{};
		/// <summary>
		/// OemBackslashKeyHeld event
		/// </summary>
		public static event EventHandler OemBackslashKeyHeld = delegate{};
		/// <summary>
		///  ProcessKeyKeyHeld event
		/// </summary>
		public static event EventHandler ProcessKeyKeyHeld = delegate{};
		/// <summary>
		/// OemCopyKeyHeld event
		/// </summary>
		public static event EventHandler OemCopyKeyHeld = delegate{};
		/// <summary>
		/// OemAutoKeyHeld event
		/// </summary>
		public static event EventHandler OemAutoKeyHeld = delegate{};
		/// <summary>
		/// OemEnlWKeyHeld event
		/// </summary>
		public static event EventHandler OemEnlWKeyHeld = delegate{};
		/// <summary>
		/// AttnKeyHeld event
		/// </summary>
		public static event EventHandler AttnKeyHeld = delegate{};
		/// <summary>
		/// CrselKeyHeld event
		/// </summary>
		public static event EventHandler CrselKeyHeld = delegate{};
		/// <summary>
		/// ExselKeyHeld event
		/// </summary>
		public static event EventHandler ExselKeyHeld = delegate{};
		/// <summary>
		/// EraseEofKeyHeld event
		/// </summary>
		public static event EventHandler EraseEofKeyHeld = delegate{};
		/// <summary>
		/// PlayKeyHeld event
		/// </summary>
		public static event EventHandler PlayKeyHeld = delegate{};
		/// <summary>
		/// ZoomKeyHeld event
		/// </summary>
		public static event EventHandler ZoomKeyHeld = delegate{};
		#endregion

		/// <summary>
		/// Initalse the KeyboardManager - must be called before the update function
		/// </summary>
		public static void Init ( )
		{
			mCurrentKeyboardState = Keyboard.GetState ( );
			mPreviousKeyboardState = Keyboard.GetState ( );

			keyDuration = new int[ ( int ) Keys.Zoom + 1 ];
		}

		/// <summary>
		/// Update the Keyboard set and fires any events
		/// </summary>
		/// <param name="gameTime">XNA reference to the current gameTime instance, used for maintaining
		/// how long keys have been pressed.
		/// </param>
		public static void Update ( GameTime gameTime )
		{
			mPreviousKeyboardState = mCurrentKeyboardState;
			mCurrentKeyboardState = Keyboard.GetState ( );
			for ( int keyId = 0; keyId < ( int ) Keys.Zoom; keyId++ )
			{
				if ( mCurrentKeyboardState.IsKeyDown ( ( Keys ) keyId ) )
				{
					if ( keyDuration[ keyId ] == 0 )
					{
						KeyPressed ( null, new KeyPressedEventArgs ( ( Keys ) keyId ) );
						handleKeyPress ( ( Keys ) keyId );
						currentKey = ( Keys ) keyId;
						mCurrentKeyPressState = KeyPressState.Pressed;
					}
					KeyHeld ( null, new KeyPressedEventArgs ( ( Keys ) keyId ) );
					handleKeyHeld ( ( Keys ) keyId );
					keyDuration[ keyId ] += gameTime.ElapsedGameTime.Milliseconds;
					currentHeldKey = ( Keys ) keyId;
					currentKey = ( Keys ) keyId;
				}
				else
				{
					keyDuration[ keyId ] = 0;
					mCurrentKeyPressState = KeyPressState.Released;
				}
			}
		}

		/// <summary>
		/// Gets the amount of time a key has been pressed in miliseconds
		/// </summary>
		/// <param name="lKey">The key to check</param>
		/// <returns>The time in miliseconds the key has been pressed for (0 means not pressed)</returns>
		public static int GetTimePressed ( Keys lKey )
		{
			return keyDuration[ ( int ) lKey ];
		}

		private static void handleKeyPress ( Keys keyPressed )
		{
			switch ( keyPressed.ToString ( ) )
			{
				case "Back":
				BackKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Tab":
				TabKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Enter":
				EnterKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Pause":
				PauseKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "CapsLock":
				CapsLockKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Kana":
				KanaKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Kanji":
				KanjiKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Escape":
				EscapeKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "ImeConvert":
				ImeConvertKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "ImeNoConvert":
				ImeNoConvertKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Space":
				SpaceKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "PageUp":
				PageUpKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "PageDown":
				PageDownKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "End":
				EndKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Home":
				HomeKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Left":
				LeftKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Up":
				UpKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Right":
				RightKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Down":
				DownKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Select":
				SelectKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Print":
				PrintKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Execute":
				ExecuteKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "PrintScreen":
				PrintScreenKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Insert":
				InsertKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Delete":
				DeleteKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Help":
				HelpKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "D0":
				D0KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "D1":
				D1KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "D2":
				D2KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "D3":
				D3KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "D4":
				D4KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "D5":
				D5KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "D6":
				D6KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "D7":
				D7KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "D8":
				D8KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "D9":
				D9KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "A":
				AKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "B":
				BKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "C":
				CKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "D":
				DKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "E":
				EKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F":
				FKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "G":
				GKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "H":
				HKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "I":
				IKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "J":
				JKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "K":
				KKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "L":
				LKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "M":
				MKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "N":
				NKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "O":
				OKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "P":
				PKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Q":
				QKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "R":
				RKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "S":
				SKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "T":
				TKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "U":
				UKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "V":
				VKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "W":
				WKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "X":
				XKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Y":
				YKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Z":
				ZKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "LeftWindows":
				LeftWindowsKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "RightWindows":
				RightWindowsKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Apps":
				AppsKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Sleep":
				SleepKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "NumPad0":
				NumPad0KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "NumPad1":
				NumPad1KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "NumPad2":
				NumPad2KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "NumPad3":
				NumPad3KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "NumPad4":
				NumPad4KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "NumPad5":
				NumPad5KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "NumPad6":
				NumPad6KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "NumPad7":
				NumPad7KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "NumPad8":
				NumPad8KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "NumPad9":
				NumPad9KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Multiply":
				MultiplyKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Add":
				AddKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Separator":
				SeparatorKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Subtract":
				SubtractKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Decimal":
				DecimalKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Divide":
				DivideKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F1":
				F1KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F2":
				F2KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F3":
				F3KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F4":
				F4KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F5":
				F5KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F6":
				F6KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F7":
				F7KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F8":
				F8KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F9":
				F9KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F10":
				F10KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F11":
				F11KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F12":
				F12KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F13":
				F13KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F14":
				F14KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F15":
				F15KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F16":
				F16KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F17":
				F17KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F18":
				F18KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F19":
				F19KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F20":
				F20KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F21":
				F21KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F22":
				F22KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F23":
				F23KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "F24":
				F24KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "NumLock":
				NumLockKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Scroll":
				ScrollKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "LeftShift":
				LeftShiftKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "RightShift":
				RightShiftKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "LeftControl":
				LeftControlKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "RightControl":
				RightControlKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "LeftAlt":
				LeftAltKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "RightAlt":
				RightAltKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "BrowserBack":
				BrowserBackKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "BrowserForward":
				BrowserForwardKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "BrowserRefresh":
				BrowserRefreshKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "BrowserStop":
				BrowserStopKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "BrowserSearch":
				BrowserSearchKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "BrowserFavorites":
				BrowserFavoritesKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "BrowserHome":
				BrowserHomeKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "VolumeMute":
				VolumeMuteKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "VolumeDown":
				VolumeDownKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "VolumeUp":
				VolumeUpKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "MediaNextTrack":
				MediaNextTrackKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "MediaPreviousTrack":
				MediaPreviousTrackKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "MediaStop":
				MediaStopKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "MediaPlayPause":
				MediaPlayPauseKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "LaunchMail":
				LaunchMailKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "SelectMedia":
				SelectMediaKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "LaunchApplication1":
				LaunchApplication1KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "LaunchApplication2":
				LaunchApplication2KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "OemSemicolon":
				OemSemicolonKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "OemPlus":
				OemPlusKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "OemComma":
				OemCommaKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "OemMinus":
				OemMinusKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "OemPeriod":
				OemPeriodKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "OemQuestion":
				OemQuestionKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "OemTilde":
				OemTildeKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "ChatPadGreen":
				ChatPadGreenKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "ChatPadOrange":
				ChatPadOrangeKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "OemOpenBrackets":
				OemOpenBracketsKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "OemPipe":
				OemPipeKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "OemCloseBrackets":
				OemCloseBracketsKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "OemQuotes":
				OemQuotesKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Oem8":
				Oem8KeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "OemBackslash":
				OemBackslashKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "ProcessKey":
				ProcessKeyKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "OemCopy":
				OemCopyKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "OemAuto":
				OemAutoKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "OemEnlW":
				OemEnlWKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Attn":
				AttnKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Crsel":
				CrselKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Exsel":
				ExselKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "EraseEof":
				EraseEofKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Play":
				PlayKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
				case "Zoom":
				ZoomKeyPressed ( null, new KeyPressedEventArgs ( keyPressed ) );
				break;
			}
		}

		private static void handleKeyHeld ( Keys keyHeld )
		{
			currentHeldKey = ( Keys ) keyHeld;
			switch ( keyHeld.ToString ( ) )
			{
				case "Back":
				BackKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Tab":
				TabKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Enter":
				EnterKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Pause":
				PauseKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "CapsLock":
				CapsLockKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Kana":
				KanaKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Kanji":
				KanjiKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Escape":
				EscapeKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "ImeConvert":
				ImeConvertKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "ImeNoConvert":
				ImeNoConvertKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Space":
				SpaceKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "PageUp":
				PageUpKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "PageDown":
				PageDownKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "End":
				EndKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Home":
				HomeKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Left":
				LeftKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Up":
				UpKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Right":
				RightKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Down":
				DownKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Select":
				SelectKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Print":
				PrintKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Execute":
				ExecuteKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "PrintScreen":
				PrintScreenKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Insert":
				InsertKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Delete":
				DeleteKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Help":
				HelpKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "D0":
				D0KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "D1":
				D1KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "D2":
				D2KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "D3":
				D3KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "D4":
				D4KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "D5":
				D5KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "D6":
				D6KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "D7":
				D7KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "D8":
				D8KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "D9":
				D9KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "A":
				AKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "B":
				BKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "C":
				CKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "D":
				DKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "E":
				EKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F":
				FKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "G":
				GKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "H":
				HKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "I":
				IKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "J":
				JKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "K":
				KKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "L":
				LKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "M":
				MKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "N":
				NKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "O":
				OKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "P":
				PKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Q":
				QKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "R":
				RKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "S":
				SKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "T":
				TKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "U":
				UKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "V":
				VKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "W":
				WKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "X":
				XKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Y":
				YKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Z":
				ZKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "LeftWindows":
				LeftWindowsKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "RightWindows":
				RightWindowsKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Apps":
				AppsKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Sleep":
				SleepKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "NumPad0":
				NumPad0KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "NumPad1":
				NumPad1KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "NumPad2":
				NumPad2KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "NumPad3":
				NumPad3KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "NumPad4":
				NumPad4KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "NumPad5":
				NumPad5KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "NumPad6":
				NumPad6KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "NumPad7":
				NumPad7KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "NumPad8":
				NumPad8KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "NumPad9":
				NumPad9KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Multiply":
				MultiplyKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Add":
				AddKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Separator":
				SeparatorKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Subtract":
				SubtractKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Decimal":
				DecimalKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Divide":
				DivideKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F1":
				F1KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F2":
				F2KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F3":
				F3KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F4":
				F4KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F5":
				F5KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F6":
				F6KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F7":
				F7KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F8":
				F8KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F9":
				F9KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F10":
				F10KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F11":
				F11KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F12":
				F12KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F13":
				F13KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F14":
				F14KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F15":
				F15KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F16":
				F16KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F17":
				F17KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F18":
				F18KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F19":
				F19KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F20":
				F20KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F21":
				F21KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F22":
				F22KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F23":
				F23KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "F24":
				F24KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "NumLock":
				NumLockKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Scroll":
				ScrollKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "LeftShift":
				LeftShiftKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "RightShift":
				RightShiftKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "LeftControl":
				LeftControlKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "RightControl":
				RightControlKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "LeftAlt":
				LeftAltKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "RightAlt":
				RightAltKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "BrowserBack":
				BrowserBackKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "BrowserForward":
				BrowserForwardKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "BrowserRefresh":
				BrowserRefreshKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "BrowserStop":
				BrowserStopKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "BrowserSearch":
				BrowserSearchKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "BrowserFavorites":
				BrowserFavoritesKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "BrowserHome":
				BrowserHomeKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "VolumeMute":
				VolumeMuteKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "VolumeDown":
				VolumeDownKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "VolumeUp":
				VolumeUpKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "MediaNextTrack":
				MediaNextTrackKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "MediaPreviousTrack":
				MediaPreviousTrackKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "MediaStop":
				MediaStopKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "MediaPlayPause":
				MediaPlayPauseKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "LaunchMail":
				LaunchMailKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "SelectMedia":
				SelectMediaKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "LaunchApplication1":
				LaunchApplication1KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "LaunchApplication2":
				LaunchApplication2KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "OemSemicolon":
				OemSemicolonKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "OemPlus":
				OemPlusKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "OemComma":
				OemCommaKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "OemMinus":
				OemMinusKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "OemPeriod":
				OemPeriodKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "OemQuestion":
				OemQuestionKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "OemTilde":
				OemTildeKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "ChatPadGreen":
				ChatPadGreenKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "ChatPadOrange":
				ChatPadOrangeKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "OemOpenBrackets":
				OemOpenBracketsKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "OemPipe":
				OemPipeKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "OemCloseBrackets":
				OemCloseBracketsKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "OemQuotes":
				OemQuotesKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Oem8":
				Oem8KeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "OemBackslash":
				OemBackslashKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "ProcessKey":
				ProcessKeyKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "OemCopy":
				OemCopyKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "OemAuto":
				OemAutoKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "OemEnlW":
				OemEnlWKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Attn":
				AttnKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Crsel":
				CrselKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Exsel":
				ExselKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "EraseEof":
				EraseEofKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Play":
				PlayKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
				case "Zoom":
				ZoomKeyHeld ( null, new KeyHeldEventArgs ( keyHeld, keyDuration[ ( int ) keyHeld ] ) );
				break;
			}
		}
	}
}
