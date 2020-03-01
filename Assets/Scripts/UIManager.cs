using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class UIManager : MonoBehaviour {
    public static UIManager instance;
    public static string saveFolderPath;

    public GameObject spectatorCamera;
    public GameObject fixedPoint;
    public PathRenderer pathRenderer;
    public Text pathName;
    public Slider speedSlider;
    public Slider cSlider;

    public GameObject point;

    public GameObject pathEntryPrefab;
    public GameObject pathEntriesContainer;
    public GameObject importPanel;

    public GameObject coordinatesPanel;
    public InputField xCoord;
    public InputField yCoord;
    public InputField zCoord;

    public CameraTarget target;

    public Text targetText;

    private FollowSpline follower;
    private PointAt pointer;
    private RectTransform importPanelTransform;
    private int direction;

    private const int targetTypesCount = 2; // # of values in the CameraTarget enum

    private void Awake() {
        if (instance == null)
            instance = this;
        saveFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LIV", "CustomCameraPaths");
    }

    private void Start() {
        follower = spectatorCamera.GetComponent<FollowSpline>();
        pointer = spectatorCamera.GetComponent<PointAt>();
        importPanelTransform = importPanel.GetComponent<RectTransform>();
        UpdateTarget(0);
    }

    public void ToggleCameraFollow(bool follow) {
        follower.isFollowing = follow;
        pointer.isPointing = follow;
    }

    public void UpdateCurvature(float c) {
        SplineManager.instance.c = c;
        pathRenderer.Recalculate();
    }

    public void UpdateSpeed(float speed) {
        follower.speed = speed;
    }

    public void SetMaxSpeed(float speed) {
        speedSlider.minValue = -speed;
        speedSlider.maxValue = speed;
    }

    // direction is either -1 or 1 (back and forwards buttons respectively)
    // cycles through the camera target types and updates the camera
    public void UpdateTarget(int direction) {
        target = (CameraTarget)(((int)target + direction + targetTypesCount) % targetTypesCount);
        targetText.text = "Target " + Enum.GetName(typeof(CameraTarget), target).Replace('_', ' ');
        switch(target) {
            case CameraTarget.Head:
                pointer.target = Camera.main.transform;
                break;
            case CameraTarget.Fixed_Point:
                pointer.target = fixedPoint.transform;
                break; 
        }
    }

    public void UpdateXCoordinate(string x) {
        if (point != null && float.TryParse(x, out float xCoord)) {
            point.transform.position = new Vector3(xCoord, point.transform.position.y, point.transform.position.z);
            pathRenderer.UpdatePoint(point.transform.GetSiblingIndex(), point.transform.position);
            pathRenderer.Recalculate();
        }
    }

    public void UpdateYCoordinate(string y) {
        if (point != null && float.TryParse(y, out float yCoord)) {
            point.transform.position = new Vector3(point.transform.position.x, yCoord, point.transform.position.z);
            pathRenderer.UpdatePoint(point.transform.GetSiblingIndex(), point.transform.position);
            pathRenderer.Recalculate();
        }
}

    public void UpdateZCoordinate(string z) {
        if (point != null && float.TryParse(z, out float zCoord)) {
            point.transform.position = new Vector3(point.transform.position.x, point.transform.position.y, zCoord);
            pathRenderer.UpdatePoint(point.transform.GetSiblingIndex(), point.transform.position);
            pathRenderer.Recalculate();
        }
    }

    public void RefreshImportPanel() {
        string[] paths = Directory.GetFiles(saveFolderPath, "*.path");
        importPanelTransform.sizeDelta = new Vector2(importPanelTransform.sizeDelta.x, 55 + paths.Length * 35);
        foreach (Transform entry in pathEntriesContainer.transform)
            Destroy(entry.gameObject);
        for(int i = 0; i < paths.Length; i++) {
            string pathName = new FileInfo(paths[i]).Name.Split('.')[0];
            GameObject entry = Instantiate(pathEntryPrefab);
            RectTransform entryTransform = entry.GetComponent<RectTransform>();
            entry.GetComponent<PathEntry>().SetPathName(pathName);
            entryTransform.SetParent(pathEntriesContainer.transform, false);
            entryTransform.localPosition = new Vector3(0, -35 * i, 0);
        }
    }

    public void ImportCameraPath(string pathName) {
        importPanel.SetActive(false);
        coordinatesPanel.SetActive(false);
        pathRenderer.ClearPoints();
        using (StreamReader reader = new StreamReader(Path.Combine(saveFolderPath, pathName + ".path"))) {
            while(!reader.EndOfStream) {
                string line = reader.ReadLine();
                if (line.StartsWith(";")) {
                    string[] pair = line.TrimStart(';').Split(':');
                    switch(pair[0]) {
                        case "c":
                            float c = float.Parse(pair[1]);
                            SplineManager.instance.c = c;
                            cSlider.value = c;
                            break;
                        case "speed":
                            float speed = float.Parse(pair[1]);
                            follower.speed = speed;
                            speedSlider.value = speed;
                            break;
                        case "mode":
                            target = (CameraTarget)Enum.Parse(typeof(CameraTarget), pair[1]);
                            UpdateTarget(0);
                            break;
                        case "point":
                            float[] coords = pair[1].Split(',').ToList().ConvertAll<float>(coord => float.Parse(coord)).ToArray(); // I'm not proud of this
                            fixedPoint.transform.position = new Vector3(coords[0], coords[1], coords[2]);
                            break;
                    }
                } else {
                    float[] coords = line.Split(',').ToList().ConvertAll<float>(c => float.Parse(c)).ToArray(); // It's just terrible
                    pathRenderer.AddPoint(new Vector3(coords[0], coords[1], coords[2]));
                }
            }
        }
        pathRenderer.Recalculate();
    }

    public void ExportCameraPath() {
        if (!Directory.Exists(saveFolderPath))
            Directory.CreateDirectory(saveFolderPath);
        using (StreamWriter writer = new StreamWriter(Path.Combine(saveFolderPath, pathName.text + ".path"))) {
            writer.WriteLine(";c:" + SplineManager.instance.c);
            writer.WriteLine(";speed:" + follower.speed);
            writer.WriteLine(";mode:" + Enum.GetName(typeof(CameraTarget), target));
            if (target == CameraTarget.Fixed_Point)
                writer.WriteLine(";point:" + fixedPoint.transform.position.x + "," + fixedPoint.transform.position.y + "," + fixedPoint.transform.position.z);
            foreach (Vector3 point in SplineManager.instance.spline.points)
                writer.WriteLine(point.x + "," + point.y + "," + point.z);
        }
    }

    public void ShowCoordinatesPanel() {
        coordinatesPanel.SetActive(true);
        xCoord.text = point.transform.position.x.ToString();
        yCoord.text = point.transform.position.y.ToString();
        zCoord.text = point.transform.position.z.ToString();
    }

    public void OnTouchpadChanged(SteamVR_Behaviour_Vector2 behaviour, SteamVR_Input_Sources source, Vector2 newAxis, Vector2 newDelta) {
        if (point == null)
            return;
        int newDirection = (int)(newAxis.x * 4 / 3);
        if (newDirection != direction && newDirection != 0) {
            pathRenderer.MovePoint(point.transform.GetSiblingIndex(), newDirection);
            point.transform.SetSiblingIndex((point.transform.GetSiblingIndex() + newDirection + point.transform.parent.childCount) % point.transform.parent.childCount);
            pathRenderer.Recalculate();
        }
        direction = newDirection;
    }

    public enum CameraTarget {
        Head = 0,
        Fixed_Point = 1
    }
}
