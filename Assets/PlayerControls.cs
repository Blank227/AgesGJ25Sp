using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerControls : MonoBehaviour
{
    InputAction moveAction;
    InputAction shootAction;
    InputAction stopAction;
    Vector2 direction;
    public float MovementSpeed = 5.0f;
    public string ShootActionName;
    public string StopActionName;
    public Rigidbody2D projectile;

    private float ShotCooldown = 0f;
    public bool CanShoot = true;
    public Camera Camera;
    public float widthThreshold;
    public float heightThreshold;


    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction(ShootActionName);
        stopAction = InputSystem.actions.FindAction(StopActionName);
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopAction.IsPressed()) 
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            float length = MathF.Sqrt(moveValue.x * moveValue.x + moveValue.y * moveValue.y);
            direction = new Vector2(moveValue.x / length, moveValue.y / length);
            Vector2 oldPosition = gameObject.transform.position;
            gameObject.transform.Translate(MovementSpeed * Time.deltaTime * direction);
            if (gameObject.transform.position.x < Camera.transform.position.x - widthThreshold ||
                gameObject.transform.position.x > Camera.transform.position.x + widthThreshold ||
                gameObject.transform.position.y < Camera.transform.position.y - heightThreshold ||
                gameObject.transform.position.y > Camera.transform.position.y + heightThreshold) gameObject.transform.position = oldPosition;
        }       

        if (!CanShoot)
        {
            ShotCooldown -= 10 * Time.deltaTime;

            if (ShotCooldown <= 0f)
            {
                ShotCooldown = 0f;
                CanShoot = true;
            }
        }

        if (shootAction.IsPressed() && CanShoot == true)
        {
            FireBullet(transform.position + new Vector3((float)0.2, 0, 0), Vector2.up * 10);
            FireBullet(transform.position - new Vector3((float)0.2, 0, 0), Vector2.up * 10);
            CanShoot = false;
            ShotCooldown = 1;
        }
    }

    private void FireBullet(Vector3 position, Vector3 direction)
    {
        Rigidbody2D clone;
        clone = Instantiate(projectile, position, transform.rotation);

        clone.linearVelocity = transform.TransformDirection(direction);
    }
}
