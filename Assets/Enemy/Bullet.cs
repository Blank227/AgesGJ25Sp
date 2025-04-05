using System;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField]
    float _speed;
    Vector2 _direction;
    [SerializeField]
    CircleCollider2D _circleCollider;
    [SerializeField]
    LayerMask _layerMask;

    public void ShootBullet(Vector2 startPosition, Vector2 direction, float speed)
    {
        _speed = speed;
        transform.position = startPosition;
        _direction = direction;
    }



    private void FixedUpdate()
    {
        transform.position = transform.position +  new Vector3(_direction.x,_direction.y,0) * Time.fixedDeltaTime * _speed;
     
        
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _circleCollider.radius, _layerMask);
        if (hit != null) 
        {
            print("disable");
            IHurt hurtInterFace = hit.gameObject.GetComponent<IHurt>();
            if (hurtInterFace != null)
            {
                hurtInterFace.Damage(1);
                
            }
            this.gameObject.SetActive(false);
        }
    }
}
