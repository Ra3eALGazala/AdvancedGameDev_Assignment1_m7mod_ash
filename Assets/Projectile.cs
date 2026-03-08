using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 direction = Vector2.right; 
    void Update()
    {
       
        transform.Translate(direction * speed * Time.deltaTime);
    }
   

    
    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("sword")) 
    {
      
        if (GameManager.instance != null)
        {
            GameManager.instance.AddPoint();
        }

        Destroy(gameObject); 
    }
}
}