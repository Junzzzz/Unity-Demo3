using Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody player;
    [SerializeField] private CinemachineFreeLook playerCamera;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 1f;

    private bool _active;

    private static readonly int Running = Animator.StringToHash("running");

    private void Start()
    {
        _active = player != null && playerCamera != null;
    }

    private void FixedUpdate()
    {
        if (_active)
        {
            Movement();
        }
    }

    private void Movement()
    {
        var axisX = Input.GetAxis("Horizontal");
        var axisZ = Input.GetAxis("Vertical");
        player.velocity = new Vector3(axisX * moveSpeed * Time.deltaTime, 0, axisZ * moveSpeed * Time.deltaTime);
        animator.SetBool(Running, axisX != 0 || axisZ != 0);
    }
}