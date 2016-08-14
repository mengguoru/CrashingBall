using UnityEngine;
using System.Collections;
using System.IO;

public class DataStore : MonoBehaviour {
    static bool hadcreated;
	
    // Use this for initialization
	void Start ()
    {
        Debug.Log(hadcreated);
        int i;
        if (!hadcreated)
        {
            for (i = 0; i < 27; i++)
            {
                CreateFile(Application.persistentDataPath, "data.txt", "1");
            }
            hadcreated = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void CreateFile(string path, string name, string info)
    {
        //文件流信息
        StreamWriter sw;
        FileInfo t = new FileInfo(path + "//" + name);
        if (!t.Exists)
        {
            //如果此文件不存在则创建
            sw = t.CreateText();
        }
        else
        {
            //如果此文件存在则打开
            sw = t.AppendText();
        }
        //以行的形式写入信息
        sw.WriteLine(info);
        //关闭流
        sw.Close();
        //销毁流
        sw.Dispose();
    }
}
