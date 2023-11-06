using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CameraController : MonoBehaviour
{
    public float rotateSpeed = 20.0f, speed = 10.0f, zoomSpeed = 500f;
    private float _mult = 1f;

    private void Update()
    {
        float _move_cam_horizontal = Input.GetAxis("Horizontal");
        float _move_cam_vertical = Input.GetAxis("Vertical");

        float rotate = 0f;
        if (Input.GetKey(KeyCode.Q))
            rotate = -1f;
        else if (Input.GetKey(KeyCode.E))
            rotate = 1f;
        
        if (Input.GetKey(KeyCode.LeftShift))
            _mult = 2f;
        else 
            _mult = 1f;

        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * rotate * _mult, Space.World);
        transform.Translate(new Vector3(_move_cam_horizontal, 0, _move_cam_vertical) * Time.deltaTime * _mult * speed, Space.Self);

        transform.position += transform.up * zoomSpeed * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel");

        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, -20f, 10f),
            transform.position.z);

        //todo
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        //    GetComponent<NavMeshAgent>().SetDestination(this.GameObject);
        //}
    }
} 
    