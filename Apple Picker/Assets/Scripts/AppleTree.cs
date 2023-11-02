using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Inscribed")]
    // Start is called before the first frame update
    public GameObject applePrefab; // apple prefab 
    private float speed = 10f; // speed of tree
    private float leftAndRightEdge = 20f;
    private float changeDirChance = 0.01f;
    private float minAppleDropDelay = 1f;
    private float maxAppleDropDelay = 5f;


    void Start()
    {
        Invoke("DropApple", Random.Range(minAppleDropDelay, maxAppleDropDelay));
    }

    // Update is called once per frame
    void Update()
    {
        // basic tree movement
        Vector3 pos = transform.position;
        pos.x += speed*Time.deltaTime;
        transform.position = pos;

        if (pos.x < -leftAndRightEdge){
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftAndRightEdge){
            speed = -Mathf.Abs(speed);
        }
    }
    private void FixedUpdate(){
        if (Random.value < changeDirChance)
        {
            speed *= -1;
        }
    }

    void DropApple(){
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        // Schedule the next apple drop at a random interval
        Invoke("DropApple", Random.Range(minAppleDropDelay, maxAppleDropDelay));
    }
}
