﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float CameraMoveSpeed =120.0f;
    // Start is called before the first frame update
    public GameObject CameraFollowObject;
    Vector3 FollowPos;
    public float clampAngle = 80.0f;
    public float inputSensivity = 150.0f;
    public GameObject CameraObj;
    public GameObject PlayerObj;
    public float camDistanceXtoPlayer;
    public float camDistanceYtoPlayer;
    public float camDistanceZtoPlayer;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
    public float smoothX;
    public float smoothY;
    private float rotX = 0.0f;
    private float rotY = 0.0f;
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;        
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("RightStickHorizontal");
        //float inputZ = Input.GetAxis("RightStickVertical");
        float inputY = Input.GetAxis("RightStickVertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = inputX + mouseX;
        //finalInputZ = inputZ + mouseY;
        finalInputZ = inputY + mouseY;

        rotY += finalInputX * inputSensivity * Time.deltaTime;
        rotX += finalInputZ * inputSensivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, - clampAngle, clampAngle);
        // Linea agregada
        rotY = Mathf.Clamp(rotY, -clampAngle, clampAngle);
        Quaternion localRotation = Quaternion.Euler(rotX, rotY,0.0f);
        transform.rotation = localRotation;

    }
    void LateUpdate() {
        CameraUpdater();
    }

    void CameraUpdater(){
        Transform target = CameraFollowObject.transform;
        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position,step);
    }
}