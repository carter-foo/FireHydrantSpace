using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HUDMarkers : MonoBehaviour {
    public Vector2 lowresScreenSize = new Vector2(540, 360);
    public float scannerRange = 200f;
    public Texture marker;

    public List<TargetIcon> targets;

    void Start() {
        StartCoroutine(ScanLoop());
    }

    public void RemoveTarget(TargetIcon t) {
        targets.Remove(t);
    }

    IEnumerator ScanLoop() {
        while (true) { 
            targets.Clear();
            var icons = GameObject.FindObjectsOfType<TargetIcon>();
            foreach (var icon in icons)
            {
                if(Vector3.Distance(transform.position, icon.transform.position) < Mathf.Min(icon.hardLimit, scannerRange * icon.rangeMult)) {
                    targets.Add(icon);
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private Vector2 CalculateScreenSizeCoeff() {
        return new Vector2(lowresScreenSize.x / Screen.width, lowresScreenSize.y / Screen.height);
    }

    private Vector2 SnapUICoords(Vector2 pos) {
        var coeff = CalculateScreenSizeCoeff();
        return new Vector2(Mathf.Floor(pos.x * coeff.x) / coeff.x, Mathf.Floor(pos.y * coeff.y) / coeff.y);
    }

    void OnGUI() {
        var PADDING = 20;

        var screenSizeCoeff = CalculateScreenSizeCoeff();
        var width = marker.width / screenSizeCoeff.x;
        var height = marker.height / screenSizeCoeff.y;
        var phi = Mathf.Atan(Screen.height / Screen.width);

        foreach (var target in targets)
        {
            if(!target.show) continue;
            
            var viewPortPos = Camera.main.WorldToViewportPoint(target.transform.position);
            var screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
            if(viewPortPos.z > 0 && viewPortPos.x > 0 && viewPortPos.y > 0 && viewPortPos.x < 1f && viewPortPos.y < 1f ) {
                screenPos = screenPos / screenSizeCoeff;
                GUI.DrawTexture(new Rect(screenPos.x - width / 2f, Screen.height - screenPos.y - height / 2f, width, height), marker);
            }
            else {
                var dir = (target.transform.position - Camera.main.transform.position).normalized;
                var proj = Vector3.ProjectOnPlane(dir, -Camera.main.transform.forward);
                var localProj = Camera.main.transform.InverseTransformDirection(proj).normalized;

                var pos = new Vector3(0.5f, 0.5f) + localProj;
                pos = new Vector3(pos.x * Screen.width, pos.y * Screen.height, 0f);
                pos.x = Mathf.Clamp(pos.x, PADDING, Screen.width - PADDING);
                pos.y = Mathf.Clamp(pos.y, PADDING, Screen.height - PADDING);
                // normalizedDirPos = new Vector3(normalizedDirPos.x / Screen.width, normalizedDirPos.y / Screen.height);
                // pos = RetroCameraEffect.SnapUICoords(pos) / RetroCameraEffect.Coefficient;
                GUI.DrawTexture(new Rect(pos.x - width / 2f, Screen.height - pos.y - height / 2f, width, height), marker);
            }
        }
    }
}