using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private RectTransform crosshair;

    public GameObject player;

    public float aimSize = 25f;
    public float idleSize = 50f;
    public float walkSize = 75f;
    public float runJumpSize = 125f;
    public float currentSize = 50f;
    public float speed = 10f;
    public float gunMultiplier = 1;

    private bool Aiming()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            return true;
        }
        return false;
    }

    private bool JumpingorRunning()
    {
        if(Input.GetKey(KeyCode.Space) || (Input.GetKey(KeyCode.LeftShift) && (!Input.GetKey(KeyCode.Mouse1))))
        {
            return true;
        }
        return false;
    }

    private bool Walking()
    {
        if(!JumpingorRunning() && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            return true;
        }
        return false;
    }

    private void Update()
    {
        if(Aiming())
        {
            currentSize = Mathf.Lerp(currentSize, aimSize*gunMultiplier, Time.deltaTime * speed);
        }
        else if(Walking()){
            currentSize = Mathf.Lerp(currentSize, walkSize*gunMultiplier, Time.deltaTime * speed);
        }
        else if(JumpingorRunning()){
            currentSize = Mathf.Lerp(currentSize, runJumpSize*gunMultiplier, Time.deltaTime * speed);
        }
        else{
            currentSize = Mathf.Lerp(currentSize, idleSize*gunMultiplier, Time.deltaTime * speed);
        }
        crosshair.sizeDelta = new Vector2(currentSize, currentSize);
    }
}
