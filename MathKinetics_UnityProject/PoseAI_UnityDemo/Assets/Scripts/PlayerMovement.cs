using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoseAI;

public class PlayerMovement : MonoBehaviour
{
    //Script to make the objects move towards the player and simulate as if the player is moving although he is staying on the same spot
    public PoseAICharacterController poseai_controller_script;
	public GameObject Player;

    public void Start()
    {
		Player = GameObject.Find("PlayerArmature Variant");
		poseai_controller_script = Player.GetComponent<PoseAICharacterController>();
	}

    void Update()
    {
        var movementSpeed = poseai_controller_script.GetMovementSpeed();
		transform.Translate(-Vector3.forward * Time.deltaTime * movementSpeed, Space.World);
    }
}
