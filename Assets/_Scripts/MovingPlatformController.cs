using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovingPlatformController : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public bool isActive;
    public float platformTimer;
    public float threshold;
    public float shapespeed = 0.00010f;
    Vector2 temp;
    public AudioSource noise;
    public AudioSource noise2;

    public PlayerBehaviour player;

    private Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();

        platformTimer = 0.1f;
        platformTimer = 0;
        isActive = false;
        distance = end.position - start.position;
        noise = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            platformTimer += Time.deltaTime;
            _Move();
           
            
                temp = transform.localScale;
                temp.x -= 0.1f * shapespeed * Time.deltaTime;
                temp.y -= 0.1f * shapespeed * Time.deltaTime;
                transform.localScale = temp;
                 noise.Play();
            
        
              

        }
        else
        {
            if (Vector3.Distance(player.transform.position, start.position) <
                Vector3.Distance(player.transform.position, end.position))
            {
                if (!(Vector3.Distance(transform.position, start.position) < threshold))
                {
                    platformTimer += Time.deltaTime;
                    _Move();
                }
            }
            else
            {
                if(!(Vector3.Distance(transform.position, end.position) < threshold))
                {
                    platformTimer += Time.deltaTime;
                    _Move();
                }
            }
        }
        if ((!isActive) && (temp.x < 1))
        {
            temp = transform.localScale;
            temp.x += 0.1f * shapespeed * Time.deltaTime;
            temp.y += 0.1f * shapespeed * Time.deltaTime;
            transform.localScale = temp;
             noise2.Play();
        }
    }

    private void _Move()
    {
        var distanceX = (distance.x > 0) ? start.position.x + Mathf.PingPong(platformTimer, distance.x) : start.position.x;
        var distanceY = (distance.y > 0) ? start.position.y + Mathf.PingPong(platformTimer, distance.y) : start.position.y;

        transform.position = new Vector3(distanceX, distanceY, 0.0f);
       
    }

    public void Reset()
    {
        transform.position = start.position;
        platformTimer = 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            temp = transform.localScale;
            temp.x -= 1f * shapespeed * Time.deltaTime;
            temp.y -= 1f * shapespeed * Time.deltaTime;
            transform.localScale = temp;
        }
    }
}
