using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class TableManager
{
    public ConstInfo constInfo;
    public Dictionary<int, CharacterInfo> Characters = new Dictionary<int, CharacterInfo>();
    public Dictionary<int, BuildInfo> Builds = new Dictionary<int, BuildInfo>();

    public void LoadTextAssets()
    {
        Load(Characters, "character");
        //Load(Builds, "builds");

        Debug.Log("Loading json completed");
    }

    private void Load<T>(Dictionary<int, T> dic, string path) where T : ITableInfo
    {
        var textAsset = Resources.Load<TextAsset>("Json/" + path);
        string json = textAsset.text;

        T[] array = JsonConvert.DeserializeObject<T[]>(json);
        foreach (var item in array)
        {
            dic.Add(item.Key, item);
        }
    }
}
