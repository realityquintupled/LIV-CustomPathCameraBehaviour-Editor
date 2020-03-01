using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathRenderer : MonoBehaviour {
    public GameObject controlPointPrefab;

    private LineRenderer lr;
    private List<Vector3> points;

    private void Start() {
        lr = GetComponent<LineRenderer>();
        points = GetComponentsInChildren<Transform>().Skip(1).ToList().ConvertAll<Vector3>(t => t.position);
        Recalculate();
    }

    public void AddPoint(Vector3 pos) {
        points.Add(pos);
        GameObject newPoint = Instantiate(controlPointPrefab);
        newPoint.transform.position = pos;
        newPoint.transform.parent = transform;
        UIManager.instance.SetMaxSpeed(transform.childCount);
    }

    public void UpdatePoint(int point, Vector3 pos) {
        points[point] = pos;
    }

    public void MovePoint(int point, int direction) {
        int newIndex = (point + direction + points.Count) % points.Count;
        Vector3 pointPos = points[point]; 
        points.RemoveAt(point);
        if (newIndex == points.Count)
            points.Add(pointPos);
        else
            points.Insert(newIndex, pointPos);
    }

    public void RemovePoint(int point) {
        points.RemoveAt(point);
    }

    public void ClearPoints() {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        points = new List<Vector3>();
        Recalculate();
    }

    public void Recalculate() {
        if(transform.childCount < 3) {
            lr.positionCount = 0;
            return;
        }
        SplineManager.instance.spline = new Spline(points);
        Vector3[] dp = new Vector3[points.Count * 30 + 1];
        for (int i = 0; i < dp.Length - 1; i++) {
            dp[i] = SplineManager.instance.PointAtTime(i * (float)points.Count / dp.Length);
        }
        dp[dp.Length - 1] = dp[0];
        lr.positionCount = dp.Length;
        lr.SetPositions(dp);
    }
}
