using UnityEngine;

public class FollowSpline : MonoBehaviour {
    public float speed = 1;
    public bool isFollowing = false;

    private float t;

    void Update() {
        if (isFollowing && SplineManager.instance.spline != null) {
            transform.position = SplineManager.instance.PointAtTime(t);
            t += Time.deltaTime * speed;
        }
    }
}
