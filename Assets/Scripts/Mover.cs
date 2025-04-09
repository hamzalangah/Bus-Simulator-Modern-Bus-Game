using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mover : MonoBehaviour
{
    public bool PickMover = false;
    public bool DropMover = false;
    public Transform[] PickTarget, DropTarget;
    public float speed, moveSpeed;
    void Update()
    {
        if (PickMover)
        {
             transform.position = Vector3.MoveTowards(transform.position, PickTarget[levelSelection.levelNo].position, moveSpeed * Time.deltaTime);
             transform.rotation = Quaternion.RotateTowards(transform.rotation, PickTarget[levelSelection.levelNo].transform.rotation, speed * Time.deltaTime);
        }
        if (DropMover)
        {
             transform.position = Vector3.MoveTowards(transform.position, DropTarget[levelSelection.levelNo].position, moveSpeed * Time.deltaTime);
             transform.rotation = Quaternion.RotateTowards(transform.rotation, DropTarget[levelSelection.levelNo].transform.rotation, speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Pick")
        {
            this.gameObject.GetComponent<Rigidbody>().drag = 8f;
            PickMover = true;
            DropMover = false;
        } 
        if(other.gameObject.tag == "Drop")
        {
            this.gameObject.GetComponent<Rigidbody>().drag = 8f;
            DropMover = true;
            PickMover = false;
        }
    }
}