using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongPlayer : MonoBehaviour
{
    private GameObject Player;
    private Transform Position;

    private float player_xPos;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("PlayerArmature Variant/Skeleton/Hips/Spine/Chest");
        Position = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        player_xPos = Player.transform.position.x;
        Position.position = new Vector3(player_xPos, Position.position.y, Position.position.z);
    }
}
