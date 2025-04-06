using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerControls : MonoBehaviour, IHurt
{

    GameHandler _gameHandler;


    [SerializeField]
    float InvincibleTime;
    float _invincibleTimer = 0f;
    [SerializeField]
    float DamageRemoveTime;




    InputAction moveAction;
    InputAction shootAction;
    InputAction stopAction;
    Vector2 direction;
    public float MovementSpeed = 5.0f;
    public string ShootActionName;
    public string StopActionName;
    public GameObject projectile;

    private float ShotCooldown = 0f;
    public bool CanShoot = true;
    public Camera Camera;
    public float widthThreshold;
    public float heightThreshold;
    BulletPool bulletPool;


    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction(ShootActionName);
        stopAction = InputSystem.actions.FindAction(StopActionName);
        var bulletPoolObject = GameObject.FindWithTag("PlayerBulletPool");
        bulletPool = bulletPoolObject.GetComponent<BulletPool>();

        GameObject gameHandlerObject = GameObject.FindWithTag("GameHandler");
        _gameHandler = gameHandlerObject.GetComponent<GameHandler>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_gameHandler.GameOver)
        {
            return;
        }


        _invincibleTimer -= Time.deltaTime;

        if (!stopAction.IsPressed()) 
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            if (moveValue != Vector2.zero) 
            {
                float length = MathF.Sqrt(moveValue.x * moveValue.x + moveValue.y * moveValue.y);
                direction = new Vector2(moveValue.x / length, moveValue.y / length);
                Vector2 oldPosition = gameObject.transform.position;
                gameObject.transform.Translate(MovementSpeed * Time.deltaTime * direction);
                if (gameObject.transform.position.x < Camera.transform.position.x - widthThreshold ||
                    gameObject.transform.position.x > Camera.transform.position.x + widthThreshold ||
                    gameObject.transform.position.y < Camera.transform.position.y - heightThreshold ||
                    gameObject.transform.position.y > Camera.transform.position.y + heightThreshold) gameObject.transform.position = oldPosition;
            }
        
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
        var bulletObject = bulletPool.GetNextBullet();
        var bullet = bulletObject.GetComponent<Bullet>();
        bullet.ShootBullet(transform.position, transform.up,10f);
    }

    public void Damage(int damage)
    {

        print("_invincibleTimer");
        if (_invincibleTimer <= 0)
        { 
        
            _invincibleTimer = InvincibleTime;
            _gameHandler.RemoveGameTime(DamageRemoveTime);
        }
    }
}
