using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPS : MonoBehaviour
{
    public Transform player;
    public Transform ship;

    public Text gpsText;

    void Start() {

    }

    void Update() {
        if(!player) {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            return;
        }

        if(!ship) {
            ship = GameObject.FindGameObjectWithTag("Ship").transform;
            return;
        }

        var dist = Vector3.Distance(player.position, ship.position);
        gpsText.text = "SIGNAL STRENGTH: " + (int) Mathf.Clamp(Mathf.Round((1.0f - dist / 500.0f) * 100.0f), 0.0f, 100.0f) + "%";
    }
}
