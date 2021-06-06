using crna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;

namespace crna
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;
        InteractableUI interactableUI;
        PlayerAttacker playerAttacker;
        public GameObject interactableUIGameObject;
        public GameObject itemInteractableGameObject;

        public bool isInteracting;

        [Header("Player Flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;
        private void Awake()
        {
            cameraHandler = CameraHandler.singleton;
            playerAttacker = GetComponent<PlayerAttacker>();
        }

        void Start()
        {
            
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            interactableUI = FindObjectOfType<InteractableUI>();
        }

        

        // Update is called once per frame
        void Update()
        {
            float delta = Time.deltaTime;
            canDoCombo = anim.GetBool("canDoCombo");
            isInteracting = anim.GetBool("isInteracting");
            anim.SetBool("isInAir", isInAir);

            
            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollingAndSprinting(delta);
            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
            playerLocomotion.HandleJumping();

            CheckForInteractableObject();
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }


        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            
            inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;
            inputHandler.d_Pad_Up = false;
            inputHandler.d_Pad_Down = false;
            inputHandler.d_Pad_Left = false;
            inputHandler.d_Pad_Right = false;
            inputHandler.a_Input = false;
            inputHandler.jump_Input = false;
            inputHandler.inventory_input = false;
            if (isInAir)
            {
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
            }
        }
        public void CheckForInteractableObject()
        {
            
            RaycastHit hit;
            if(Physics.SphereCast(transform.position, 0.3f, transform.forward,out hit,1f,cameraHandler.ignoreLayers))
            {
                
                if (hit.collider.tag == "interactable")
                {
                    
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if (interactableObject != null)
                    {
                       
                        string interactableText = interactableObject.interactableText;
                        interactableUI.interactableText.text = interactableText;
                        interactableUIGameObject.SetActive(true);
                        if (inputHandler.a_Input)
                        {
                            
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }
            }
            else
            {
                if(interactableUIGameObject!=null)
                {
                    interactableUIGameObject.SetActive(false);
                }
                if(itemInteractableGameObject!=null && inputHandler.a_Input)
                {
                    itemInteractableGameObject.SetActive(false);
                }
            }
        }

    }

}