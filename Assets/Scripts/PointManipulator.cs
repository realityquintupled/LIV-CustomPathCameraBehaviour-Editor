using UnityEngine;
using Valve.VR;

public class PointManipulator : MonoBehaviour {
    public PathRenderer pathRenderer;
    public float speed = 1;
    public SteamVR_Input_Sources hand;
    public Material activeMat;
    public Material inactiveMat;

    private GameObject point;
    private GameObject relative;
    private float direction;

    public void Start() {
        relative = new GameObject("Pointer-Relative");
        relative.transform.parent = transform;
    }

    public void PointGrabbed(SteamVR_Behaviour_Boolean behaviour, SteamVR_Input_Sources source, bool state) {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 20)) {
            if(hit.transform.tag == "ControlPoint" || hit.transform.tag == "FixedPoint") {
                point = hit.transform.gameObject;
                relative.transform.position = point.transform.position;
            }
        }
    }
    
    public void Update() {
        if (point != null) {
            relative.transform.position += transform.forward.normalized * speed * Time.deltaTime * direction;
            point.transform.position = relative.transform.position;
            pathRenderer.Recalculate();
        } else {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 20)) {
                if (hit.transform.tag == "ControlPoint") {
                    if(UIManager.instance.point != hit.transform.gameObject) {
                        if (UIManager.instance.point != null)
                            UIManager.instance.point.GetComponent<Renderer>().material = inactiveMat;
                        SteamVR_Actions.Controls.Haptics.Execute(0, .1f, 50, 1, hand);
                        UIManager.instance.point = hit.transform.gameObject;
                        UIManager.instance.ShowCoordinatesPanel();
                        hit.transform.gameObject.GetComponent<Renderer>().material = activeMat;
                    }
                }
            }
        }
    }

    public void OnTouchpadChanged(SteamVR_Behaviour_Vector2 behaviour, SteamVR_Input_Sources source, Vector2 newAxis, Vector2 newDelta) {
        direction = newAxis.y;
    }

    public void PointReleased(SteamVR_Behaviour_Boolean behaviour, SteamVR_Input_Sources source, bool state) {
        point = null;
    }

    public void CreatePoint(SteamVR_Behaviour_Boolean behaviour, SteamVR_Input_Sources source, bool state) {
        pathRenderer.AddPoint(transform.position);
        pathRenderer.Recalculate();
    }

    public void RemovePoint(SteamVR_Behaviour_Boolean behaviour, SteamVR_Input_Sources source, bool state) {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 20)) {
            if (hit.transform.tag == "ControlPoint") {
                DestroyImmediate(hit.transform.gameObject);
                pathRenderer.Recalculate();
                UIManager.instance.coordinatesPanel.SetActive(false);
            }
        }
    }
}
