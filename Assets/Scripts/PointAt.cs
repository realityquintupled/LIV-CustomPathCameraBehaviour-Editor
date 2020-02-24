using UnityEngine;

[ExecuteAlways]
public class PointAt : MonoBehaviour {
    public Transform target;
    public bool isPointing;

    void LateUpdate() {
        if(isPointing)
            transform.LookAt(target);
    }
}
