using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] Rigidbody playerigidbody;
    [SerializeField] FixedJoystick joystick;
    [SerializeField] float moveSpeed = 0f;
    private void FixedUpdate()
    {
        playerigidbody.velocity = new Vector3(joystick.Horizontal * moveSpeed, playerigidbody.velocity.y, joystick.Vertical * moveSpeed);
    }
}
