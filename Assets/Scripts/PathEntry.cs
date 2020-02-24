using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PathEntry : MonoBehaviour {
    public Text pathNameLabel;

    private string pathName;

    public void SetPathName(string pathName) {
        this.pathName = pathName;
        pathNameLabel.text = pathName;
    }

    public void SelectPath() {
        UIManager.instance.ImportCameraPath(pathName);
    }

    public void DeletePath() {
        File.Delete(Path.Combine(UIManager.saveFolderPath, pathName + ".path"));
        UIManager.instance.RefreshImportPanel();
    }
}
