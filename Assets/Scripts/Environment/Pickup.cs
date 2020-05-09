﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pickup : MonoBehaviour
{
    public float turnDegrees;
    bool toDestroy;
    
    public enum pickupTypes
    {
        BasicGun,
    }
    public pickupTypes type;

    void Start()
    {
        if (type == pickupTypes.BasicGun)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/shittygun");
        }
    }

    void FixedUpdate()
    {
        gameObject.transform.Rotate(0, turnDegrees * Time.deltaTime, 0, Space.World);
        if (toDestroy) { Destroy(this.gameObject); }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.name != "Player") { return; }

        if (type == pickupTypes.BasicGun)
        {
            StartCoroutine(WhichGun("BasicGun"));
        }

        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<SphereCollider>().enabled = false;
    }

    IEnumerator WhichGun(string type)
    {
        yield return new WaitUntil(() => Input.GetMouseButton(0) || Input.GetMouseButton(1));

        if (Input.GetMouseButtonDown(0)) { EventMaster.Instance.PickupGun(type, "left"); }
        if (Input.GetMouseButtonDown(1)) { EventMaster.Instance.PickupGun(type, "right"); }

        toDestroy = true;

        yield break;
    }
}