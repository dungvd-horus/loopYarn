using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;

public class BuildReportLogGenerator
{

    [MenuItem("Horus/Generate Build APK Log")]
    public static void GenerateBuildAPKLog()
    {
#if UNITY_IOS
        string editorLogPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Library/Logs/Unity/Editor.log");
#else
        // Get the path to the Editor.log file
        string editorLogPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "Unity", "Editor", "Editor.log");
#endif

        // Check if the Editor.log file exists
        if (!File.Exists(editorLogPath))
        {
            Debug.LogError("Editor.log file not found. Make sure Unity has generated the log file.");
            return;
        }
        else
        {
            Debug.Log($"Found {editorLogPath}");
        }

        // Create a StreamReader to read the log file with file sharing enabled
        using (StreamReader reader = new StreamReader(new FileStream(editorLogPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
        {
            // Read the contents of the Editor.log file
            string editorLogContents = reader.ReadToEnd();

            // Find the indices of the "Build Report" section start and end
            int startIdx = editorLogContents.IndexOf("Build Report");
            int endIdx = editorLogContents.IndexOf("-------------------------------------------------------------------------------", startIdx + 1);

            // Check if the "Build Report" section exists
            if (startIdx == -1 || endIdx == -1)
            {
                Debug.LogError("Build Report section not found in the Editor.log file.");
                return;
            }

            // Extract the "Build Report" section
            string buildReport = editorLogContents.Substring(startIdx, endIdx - startIdx).Trim();


            if (string.IsNullOrEmpty(buildReport))
            {
                Debug.LogError("Editor.log buildReport not found");
                return;
            }

            // Set the output file name based on the build format
            string outputFileName = GetBuildLogFileName();

            // Set the output file path to the desktop
            string outputPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), outputFileName);

            // Write the build report to a new log file
            File.WriteAllText(outputPath, buildReport);

            // Refresh the Unity editor to show the file
            AssetDatabase.Refresh();

            // Log a message indicating the log has been generated
            Debug.Log("Build log generated at: " + outputPath);
        }

        // Helper method to generate the build log file name including version and version code
        string GetBuildLogFileName()
        {
            string productName = PlayerSettings.productName.Replace(" ", "");
            string version = PlayerSettings.bundleVersion;
            int versionCode = PlayerSettings.Android.bundleVersionCode;

            string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");

            return string.Format("BuildLog_{0}_{1}_VCode{2}_{3}_APK.txt", productName, version, versionCode, timestamp);
        }
    }


    [MenuItem("Horus/Generate Build Bundle Log")]
    public static void GenerateBuildBundleLog()
    {
        // Get the path to the Editor.log file
        string editorLogPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "Unity", "Editor", "Editor.log");
        // Check if the Editor.log file exists
        if (!File.Exists(editorLogPath))
        {
            Debug.LogError("Editor.log file not found. Make sure Unity has generated the log file.");
            return;
        }
        else
        {
            Debug.Log($"Found {editorLogPath}");
        }

        // Create a StreamReader to read the log file with file sharing enabled
        using (StreamReader reader = new StreamReader(new FileStream(editorLogPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
        {
            // Read the contents of the Editor.log file
            string editorLogContents = reader.ReadToEnd();

            // Find the indices of the "Build Report" section start and end
            int startIdx = editorLogContents.IndexOf("Bundle Name");

            int endIdx = editorLogContents.IndexOf("Bundle Name: Android", startIdx + 1);

            if (endIdx == -1)
            {
                endIdx = editorLogContents.IndexOf("Bundle Name: iOS", startIdx + 1);
            }

            // Check if the "Build Report" section exists
            if (startIdx == -1 || endIdx == -1)
            {
                Debug.LogError("Build Report section not found in the Editor.log file.");
                return;
            }

            // Extract the "Build Report" section
            string buildReport = editorLogContents.Substring(startIdx, endIdx - startIdx).Trim();


            if (string.IsNullOrEmpty(buildReport))
            {
                Debug.LogError("Editor.log buildReport not found");
                return;
            }

            // Set the output file name based on the build format
            string outputFileName = GetBuildLogFileName();

            // Set the output file path to the desktop
            string outputPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), outputFileName);

            // Write the build report to a new log file
            File.WriteAllText(outputPath, buildReport);

            // Refresh the Unity editor to show the file
            AssetDatabase.Refresh();

            // Log a message indicating the log has been generated
            Debug.Log("Build log generated at: " + outputPath);
        }

        // Helper method to generate the build log file name including version and version code
        string GetBuildLogFileName()
        {
            string productName = PlayerSettings.productName.Replace(" ", "");
            string version = PlayerSettings.bundleVersion;
            int versionCode = PlayerSettings.Android.bundleVersionCode;

            string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");

            return string.Format("BuildLog_{0}_{1}_VCode{2}_{3}_BUNDLE.txt", productName, version, versionCode, timestamp);
        }
    }
}
