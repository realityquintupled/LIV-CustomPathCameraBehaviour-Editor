using UnityEngine;

public class SplineManager : MonoBehaviour {
    public static SplineManager instance;

    public float c;
    public Spline spline;

    private void Awake() {
        if (instance == null)
            instance = this;
    }

    public Vector3 PointAtTime(float t) {
        return spline.PointAtTime(t, c);
    }
}
