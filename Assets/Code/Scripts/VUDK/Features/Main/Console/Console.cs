namespace VUDK.Features.Main.Console 
{
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using UnityEngine.SceneManagement;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Console : MonoBehaviour
    {
        [SerializeField]
        private GameObject _consolePanel;
        [SerializeField]
        private TextMeshProUGUI _log;
        [SerializeField]
        private TMP_InputField _cmdInput;
        [SerializeField]
        private KeyCode _openConsoleKey;

        [SerializeField]
        private bool _disableCursorOnConsoleClose = false;
        [SerializeField]
        private bool _useCustomCommands = false;

        private static readonly List<string> commands = new List<string> // All possible default commands
        {
            "close",
            "clear",
            "gameobjects",
            "children",
            "find",
            "currentscene",
            "allscenes",
            "loadscene",
            "destroy",
            "move",
            "timescale",
            "fps",
            "vsync",
            "deleteprefs",
            "customcommands",
            "help"
        };

        private void Update()
        {
            if (Input.GetKeyDown(_openConsoleKey))
            {
                Enable();
                Cursor.visible = true;
            }
        }

        private void Enable()
        {
            _consolePanel.SetActive(true);
            _cmdInput.Select();
            _cmdInput.ActivateInputField();
        }

        public void OnSubmit(string value)
        {
            _cmdInput.text = "";
            _cmdInput.ActivateInputField();
            SendCommand(value);
        }

        private void SendCommand(string command)
        {
            command = command.ToLower();

            if (command.Contains("help"))
            {
                string[] cmd = command.Split(' ');

                if (cmd.Length > 1 && cmd[1] == "?")
                {
                    Log("Use this command to visualize the list of commands.");
                }
                else
                {
                    Log("Command List:\n" + " - " + string.Join("\t\n - ", commands) + "\nFor more information about a specific command, type: commandname ?");
                }

                return;
            }

            if (command.Contains("close"))
            {
                string[] cmd = command.Split(' ');

                if (cmd.Length > 1 && cmd[1] == "?")
                {
                    Log("Use this command to close the console.");
                }
                else
                {
                    if (_disableCursorOnConsoleClose)
                        Cursor.visible = false;
                    _consolePanel.SetActive(false);
                }

                return;
            }

            if (command.Contains("currentscene"))
            {
                string[] cmd = command.Split(' ');

                if (cmd.Length > 1 && cmd[1] == "?")
                {
                    Log("Use this command to get the index and name of the current scene.");
                }
                else
                {
                    Scene scene = SceneManager.GetActiveScene();
                    Log("(" + scene.buildIndex + ")" + " " + scene.name);
                }

                return;
            }

            if (command.Contains("allscenes"))
            {
                string[] cmd = command.Split(' ');

                if (cmd.Length > 1 && cmd[1] == "?")
                {
                    Log("Use this command to visualize all the scenes in the buildindex");
                }
                else
                {
                    string names = "";

                    for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
                    {
                        string scene = SceneUtility.GetScenePathByBuildIndex(i);
                        names += "(" + i + ")" + " " + scene + "\n";
                    }

                    Log(names);
                }

                return;
            }

            if (command.Contains("clear"))
            {
                string[] cmd = command.Split(' ');

                if (cmd.Length > 1 && cmd[1] == "?")
                {
                    Log("Use this command to clear the console.");
                }
                else
                {
                    ClearConsole();
                }

                return;
            }

            if (command.Contains("gameobjects"))
            {
                string[] cmd = command.Split(' ');

                if (cmd.Length > 1 && cmd[1] == "?")
                {
                    Log("Use this command to visualize all the scene in the build index.");
                }
                else
                {
                    GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

                    string goList = "List of the Main GameObjects in the scene: \n";

                    foreach (GameObject go in allObjects)
                    {
                        if (go.transform.parent == null)
                        {
                            goList += $"name: {go.name} --- tag: {go.tag} --- children: {go.transform.childCount}\n";
                        }
                    }

                    Log(goList);
                }
                return;
            }

            if (command.Contains("children "))
            {
                string[] cmd = command.Split(' ');
                string childs = "";

                if (cmd[1] == "?" || cmd.Length < 2)
                {
                    Log("Use this command to visualize all the children of a gameobject.\nSyntax: children GameObjectName.");
                }
                else
                {
                    GameObject go = FindIgnoreCase(cmd[1]);

                    if (go)
                    {
                        foreach (Transform child in go.transform.GetComponentsInChildren<Transform>())
                        {
                            string parentName = child.transform.parent is null ? "" : child.transform.parent.name;

                            childs += $"name: {child.name} --- tag: {child.tag} --- children: {child.transform.childCount} --- parent: {parentName}\n";
                        }

                        Log(childs);
                    }
                    else
                    {
                        Log($"GameObject {cmd[1]} was not found.");
                    }
                }

                return;
            }

            if (command.Contains("find "))
            {
                int firstSpaceIndex = command.IndexOf(' ');

                string cmd = command.Substring(0, firstSpaceIndex);
                string parameter = command.Substring(firstSpaceIndex + 1);

                if (parameter != "?")
                {
                    GameObject[] foundGameObjects = FindIgnoreCase(parameter, out int count);
                    string gosList = "";

                    if (foundGameObjects.Length > 0)
                    {
                        foreach (GameObject go in foundGameObjects)
                        {
                            gosList += $"{go.name} found at position {go.transform.position} --- children: {go.transform.childCount}\n";
                        }

                        Log(gosList);
                    }
                    else
                    {
                        Log($"No GameObject of name {parameter} was found.");
                    }
                }
                else
                {
                    Log("Use this command to find a gameobject and check its position in the scene.\nSyntax: find GameobjectName");
                }


                return;
            }

            if (command.Contains("loadscene "))
            {
                string[] sceneCmd = command.Split(' ');

                if (sceneCmd[1] != "?")
                {
                    if (int.TryParse(sceneCmd[1], out int sceneIndex))
                    {
                        if (sceneIndex > SceneManager.sceneCountInBuildSettings || sceneIndex < 0)
                        {
                            Log("Scene at index " + sceneIndex + " was not found.");
                        }
                        else
                        {
                            _consolePanel.SetActive(false);
                            SceneManager.LoadScene(sceneIndex);
                        }
                    }
                    else
                    {
                        Log(sceneCmd[1] + " is not a number." + "Please, insert the build index number of the scene you want to load.");
                    }
                }
                else
                {
                    Log("Use this command to load a scene.\nSyntax: loadscene indexOfTheScene.");
                }
                return;
            }

            if (command.Contains("destroy "))
            {
                int firstSpaceIndex = command.IndexOf(' ');

                string cmd = command.Substring(0, firstSpaceIndex);
                string goName = command.Substring(firstSpaceIndex + 1);

                if (goName != "?")
                {

                    GameObject go = FindIgnoreCase(goName);

                    if (go)
                    {
                        Log($"{go.name} found at position {go.transform.position} has been destroyed.");
                        Destroy(go);
                    }
                    else
                    {
                        Log($"{goName} was not found.");
                    }
                }
                else
                {
                    Log("Use this command to destroy an object in the scene by using its name.\nSyntax: destroy GameObjectName");
                }


                return;
            }

            if (command.Contains("move"))
            {
                try
                {
                    int firstSpaceIndex = command.IndexOf(' ');                     // Example: "move Main Camera (0,0,0)" | => break string point
                    string firstPart = command.Substring(0, firstSpaceIndex);       // "move | Main Camera (0,0,0)"
                    string secondPart = command.Substring(firstSpaceIndex + 1);

                    if (secondPart != "?")
                    {

                        int firstBracketIndex = secondPart.IndexOf('(');                    // Main Camera (0,0,0)

                        string goName = secondPart.Substring(0, firstBracketIndex - 1);         // Main Camera | (0,0,0)
                        string stringCoordinates = secondPart.Substring(firstBracketIndex + 1);

                        string[] coordinates = stringCoordinates.Replace(")", "").Split(',');
                        GameObject go = FindIgnoreCase(goName);


                        if (go)
                        {
                            if (float.TryParse(coordinates[0], out float x) && float.TryParse(coordinates[1], out float y) && float.TryParse(coordinates[2], out float z))
                            {
                                Vector3 destination = new Vector3(x, y, z);
                                Log($"{go.name} moved from position {go.transform.position} to position {destination}");
                                go.transform.position = destination;
                            }
                            else
                            {
                                Log("The position you want to move the object is not correct, please insert a position following this scheme: x,y,z");
                            }
                        }
                        else
                        {
                            Log($"GameObject of name {goName} was not found");
                        }
                    }
                    else
                    {
                        Log($"Use this command to an object in a specified position.\nSyntax: move NameOfGameObject (x,y,z)");
                    }
                }
                catch (System.Exception)
                {
                    Log($"Incorrect command syntax, use the following syntax: move NameOfGameObject (x,y,z)");
                }
                return;
            }

            if (command.Contains("timescale"))
            {
                string[] cmdTime = command.Split(' ');


                if (cmdTime.Length > 1)
                {
                    if (cmdTime[1] != "?")
                    {
                        if (cmdTime[1] != "reset")
                        {
                            if (float.TryParse(cmdTime[1], out float timeResult))
                            {
                                Log($"Timescale changed from {Time.timeScale} to {timeResult}");
                                Time.timeScale = timeResult;
                            }
                            else
                            {
                                Log("Timescale number not valid, please insert a number.");
                            }
                        }
                        else
                        {
                            Log("Timescale resetted to 1.0");
                            Time.timeScale = 1.0f;
                        }
                    }
                    else
                    {
                        Log($"Use this command to see or set the timescale of the game.\nSyntax:\n - timescale N => To set a timescale\n - timescale reset => To reset the timescale to 1\n - timescale => To see the current timescale");
                    }
                }
                else
                {
                    Log($"Current timescale {Time.timeScale}");
                }

                return;
            }

            if (command.Contains("fps"))
            {
                string[] cmdFps = command.Split(' ');

                if (cmdFps.Length > 1)
                {
                    if (cmdFps[1] != "?")
                    {
                        if (int.TryParse(cmdFps[1], out int fps))
                        {
                            Log($"FPS cap changed from {Application.targetFrameRate} to {fps}");
                            Application.targetFrameRate = fps;
                        }
                        else
                        {
                            Log("FPS number not valid, please insert an integer number.");
                        }
                    }
                    else
                    {
                        Log($"Use this command to see or set the max Frame Rate of the game.\nSyntax:\n - fps N => To set the fps\n - fps -1 => to uncap the framerate \n - fps => To see the current max framerate");
                    }
                }
                else
                {
                    Log($"Current max FPS {Application.targetFrameRate}");
                }

                return;
            }

            if (command.Contains("vsync"))
            {
                string[] cmdVsync = command.Split(' ');

                if (cmdVsync.Length > 1)
                {
                    if (cmdVsync[1] != "?")
                    {
                        if (int.TryParse(cmdVsync[1], out int vs))
                        {
                            if (vs >= 0 && vs <= 1)
                            {
                                Log($"VSync changed from {QualitySettings.vSyncCount} to {vs}");
                                QualitySettings.vSyncCount = vs;
                            }
                            else
                            {
                                Log($"VSync number {vs} is not valid. Please use 0 or 1.");
                            }
                        }
                        else
                        {
                            Log("VSync number not valid, please insert an integer number: 0 or 1.");
                        }
                    }
                    else
                    {
                        Log($"Use this command to see or set the VSync of the game.\nSyntax:\n - vsync 1 => To set the vsync on\n - vsync 0 => to set the vsync off \n - vsync => To see the current vsync mode.");
                    }
                }
                else
                {
                    Log($"Current vsync {QualitySettings.vSyncCount}");
                }

                return;
            }

            if (command.Contains("deleteprefs"))
            {
                string[] cmd = command.Split(' ');

                if (cmd.Length > 1)
                {
                    if (cmd[1] != "?")
                    {
                        Log($"Use this command to delete all playerprefs.\nSyntax:\n - playerprefs => To clear all playerprefs data.\n");
                    }
                    else
                    {
                        Log($"Parameter not recognized.");
                    }
                }
                else
                {
                    PlayerPrefs.DeleteAll();
                    Log("All playerprefs data has been deleted.");
                }

                return;
            }

            if (command.Contains("customcommands"))
            {
                string[] cmd = command.Split(' ');

                if (cmd.Length > 1 && cmd[1] == "?")
                {
                    Log("Use this command to see the custom commands for this console.");
                }
                else
                {
                    SendMessage("Commands", command);
                }

                return;
            }

            if (_useCustomCommands)
            {
                SendMessage("Commands", command, SendMessageOptions.DontRequireReceiver);
            }

            if (command == "")
            {
                Log("");
                return;
            }

            if (!_useCustomCommands)
                Log("\"" + command + "\"" + " is not recognized as an internal command.");
        }

        public void Log(string log)
        {
            this._log.text += "\n\n" + log;
        }

        private void ClearConsole()
        {
            this._log.text = "";
        }

        private GameObject FindIgnoreCase(string name)
        {
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            return allObjects.FirstOrDefault(go => Regex.IsMatch(go.name, "^" + name + "$", RegexOptions.IgnoreCase));
        }

        private GameObject[] FindIgnoreCase(string name, out int count)
        {
            GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            allObjects = allObjects.Where(go => Regex.IsMatch(go.name, "^" + name + "$", RegexOptions.IgnoreCase)).ToArray();
            count = allObjects.Length;
            return allObjects;
        }
    }
}
