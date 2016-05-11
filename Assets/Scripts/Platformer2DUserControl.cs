using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private bool m_Dash;
		private bool m_Pause;
		private Canvas canvas;
		private Menu menu;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
			canvas = GameObject.Find ("Canvas").GetComponent<Canvas>();
			menu = canvas.GetComponent<Menu> ();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_Dash)
            {
                //Fire1 for Dash? Pitää asettaa oikee Edit -> Project Settings -> Input Settings...
                m_Dash = CrossPlatformInputManager.GetButtonDown("Dash"); //F
            }

			if (!m_Pause) {
				m_Pause = CrossPlatformInputManager.GetButtonDown("Pause"); //P

			}
			//pause button?
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(h, m_Dash, m_Jump);
            m_Jump = false;
            m_Dash = false;
			if (m_Pause) {
				menu.PauseGame();
				m_Pause = false;
			}
        }
    }
}
