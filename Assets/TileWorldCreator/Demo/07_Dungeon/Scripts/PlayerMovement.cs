﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TWC.Demo
{
	public class PlayerMovement : MonoBehaviour
	{
		
		public float playerSpeed = 2.0f;
		public float jumpHeight = 1.0f;
		public float gravityValue = -9.81f;
		
		private TileWorldCreator tileWorldCreator;
		private CharacterController controller;
		private Vector3 playerVelocity;
		private bool groundedPlayer;
		
	
	
		private void Start()
		{
			controller = this.GetComponent<CharacterController>();
			
			controller.enabled = true;
		}
	
		void Update()
		{
			groundedPlayer = controller.isGrounded;
			if (groundedPlayer && playerVelocity.y < 0)
			{
				playerVelocity.y = 0f;
			}
	
			Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			controller.Move(move * Time.deltaTime * playerSpeed);
	
			if (move != Vector3.zero)
			{
				gameObject.transform.forward = move;
			}
	
			// Changes the height position of the player..
			//if (Input.GetButtonDown("Jump") && groundedPlayer)
			//{
			//	playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
			//}
	
			playerVelocity.y += gravityValue * Time.deltaTime;
			controller.Move(playerVelocity * Time.deltaTime);
		}
	}
}