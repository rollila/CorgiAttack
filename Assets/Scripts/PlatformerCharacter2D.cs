using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets._2D
{	[RequireComponent(typeof (UIHandler))]
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 15f;
		[SerializeField] private float m_DJumpForce = 10f;
		[SerializeField] private float m_DashForce = 100f;  
		[SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        private float moveSpeed;

        //Booleaneja ja speed animaatioita varten:
        private bool m_Grounded;            // Onko pelaaja maassa
        private bool m_Doublejump;          // Käsky tuplahypätä
		private bool m_Dashing;
		private float prevP;

        //Score
        public float playerScore;
		private UIHandler uiHandler;
		private Canvas canvas;
		private Menu menu;

		//Audio
		//Pitäis löytää parempia ääniä
		private AudioSource audioS;
		public AudioClip jumpSound;
		public AudioClip collisionSound;
		public AudioClip dashSound;


        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            moveSpeed = 3f;
            playerScore = 0;
			uiHandler = GetComponent<UIHandler> ();
			canvas = GameObject.Find ("Canvas").GetComponent<Canvas>();
			menu = canvas.GetComponent<Menu> ();
			prevP = -1f;
			audioS = GetComponent<AudioSource> ();
        }


        private void FixedUpdate()
        {
            moveSpeed += 0.01f;
            m_Grounded = false;
            playerScore += 0.1f * moveSpeed;
			uiHandler.UpdateScore((int) playerScore);

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
					m_Doublejump = false;
					m_Anim.SetBool("Falling", false);
            }

            //Oon nyt asettanut animaattoriin booleanin "Ground" joka on oltava TRUE, jotta corgi voi pompata
           	m_Anim.SetBool("Ground", m_Grounded);

            //corgin tippumisanimaatio
			if (!m_Grounded && m_Rigidbody2D.velocity.y < 0)
            {
				m_Anim.SetBool("Jump", false);
				m_Anim.SetBool ("Doublejump", false);
                m_Anim.SetBool("Falling", true);
            }

			//corgi on jumissa ja pelin pitää päättyä, nopeutta ei voi kattoa koska se pakotetaan corgille ni katon updatesyklien välissä et paikka muuttuu
			//tahmainen/huono
			if (transform.position.x == prevP) {
				Debug.Log ("Corgi is stuck");
				CorgiCollision ();
				menu.Death (GetPoints ());
			} else {
				prevP = transform.position.x;
			}
        }

		public void AddPoints(int points) {
			//tässä vois lisää kertoimen
			uiHandler.AddPoints (points);
			playerScore += (float)points;
		}

		public int GetPoints() {
			return (int)playerScore;
		}

		public bool IsDashing() {
			//Is Corgi dashing when it touches sign?
			return m_Dashing;
		}

        public void Move(float move, bool dash, bool jump)
        {
            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                //move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                //m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(moveSpeed, m_Rigidbody2D.velocity.y);
     
            }

            // If the player should jump...
			if (m_Grounded && jump && m_Anim.GetBool("Ground") || !m_Doublejump && jump && !m_Grounded)
            {
				//sound
				audioS.PlayOneShot (jumpSound);

				//Doublejump
				if (!m_Doublejump && !m_Grounded) {
					m_Doublejump = true;
					m_Anim.SetBool ("Doublejump", true);

					if (m_Anim.GetBool("Falling")) {
						//kumoaa gravitaation:
						m_Rigidbody2D.velocity = new Vector2(0f, m_JumpForce);
					} else {
						//tönäisee:
						m_Rigidbody2D.AddForce(new Vector2(0f, m_DJumpForce), ForceMode2D.Impulse);
					}

					m_Anim.SetBool("Falling", false);
				}

				//Jump
				else if (m_Grounded) {
	                m_Grounded = false;
	                m_Anim.SetBool("Ground", false);
					m_Anim.SetBool("Jump", true); //on mahdollista et tätä ei tartte mut animaatiot vastustaa mua
					m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce), ForceMode2D.Impulse);
					//m_Rigidbody2D.velocity = new Vector2(0f, m_JumpForce);
					//m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
				}
            }

			//Dash
			if (dash && !m_Anim.GetBool("Dash"))
            {
                m_Anim.SetBool("Dash", true);
				m_Rigidbody2D.AddForce(new Vector2(m_DashForce, 0f), ForceMode2D.Impulse);
				m_Dashing = true;
				StartCoroutine (WaitDash());
				audioS.PlayOneShot (dashSound);
            }
        }
			
		public void CorgiCollision() {
			audioS.PlayOneShot (collisionSound, 20f); //tää on tosi hiljanen
			m_Anim.SetTrigger ("Angel");
			audioS.PlayOneShot (collisionSound);
			//m_Anim.SetBool("Collision", true);
			//Debug.Log ("Corgi collision animation should be playing??");
		}

		public void CorgiFell() {
			m_Anim.SetBool ("FellOut", true);
		}

		IEnumerator WaitDash() {
			yield return new WaitForSeconds (0.3f); //pituus
			m_Anim.SetBool("Dash", false);
			m_Dashing = false;
		}

    }
}
