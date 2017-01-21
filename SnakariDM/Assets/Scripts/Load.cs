using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System;


/// <summary>
/// Utility class written by Tero Paavolainen to work with Unity file systems.
/// Contains basic file loading
/// </summary>
public class Load
{
    public const string MetaExtension = ".meta";

    public static List<DataFile> LoadFilesFromExeLevel(string folderPath)
    {
        string[] fileNames = GetFileNamesAtExeLevel(folderPath);
        List<DataFile> fileContents = new List<DataFile>();
        for (int i = 0; i < fileNames.Length; i++)
        {
            string fileName = fileNames[i];
            if (fileName.Contains(MetaExtension)) continue;
            fileContents.Add(LoadFile(fileName));
        }

        return fileContents;

    }

    public static List<DataFile> LoadFiles(string path)
    {
        string[] fileNames = Directory.GetFiles(path);
        List<DataFile> fileContents = new List<DataFile>();
        for (int i = 0; i < fileNames.Length; i++)
        {
            string fileName = fileNames[i];
            if (fileName.Contains(MetaExtension)) continue;
            fileContents.Add(LoadFile(fileName));
        }

        return fileContents;
    }



    private static string[] GetFileNamesAtExeLevel(string folderPath)
    {

        string fullPath = System.IO.Path.Combine(ExeLevelPath(), folderPath);
        string[] fileNames = Directory.GetFiles(fullPath);
        return fileNames;
    }




    public static void LoadObjModel(string filename)
    {
        throw new NotImplementedException();
        //Mesh mesh = new Mesh();
    }


    public static bool IsDirectory(string path)
    {
        return Directory.Exists(path);
    }

    public static bool IsDirectoryAtExeLevel(string path)
    {
        string fullPath = Path.Combine(ExeLevelPath(), path);
        return IsDirectory(fullPath);
    }


    public static bool IsFile(string path)
    {
        return File.Exists(path);
    }


    public static string ExeLevelPath()
    {
        return OneDirectoryUp(Application.dataPath);
    }


    public static bool IsFileAtExeLevel(string path)
    {
        string fullPath = System.IO.Path.Combine(ExeLevelPath(), path);
        return IsFile(fullPath);
    }


    public static string OneDirectoryUp(string folderPath)
    {
        return System.IO.Path.Combine(folderPath, "..");
    }



    public static byte[] LoadFileAsBytes(string filePath)
    {
        string fullPath = System.IO.Path.Combine(OneDirectoryUp(Application.dataPath), filePath);
        byte[] bytes = File.ReadAllBytes(fullPath);
        return bytes;
    }


    public static DataFile LoadFile(string filePath)
    {
        string fullPath = System.IO.Path.Combine(OneDirectoryUp(Application.dataPath), filePath); //If given with full path, Path.Combine will use that. Otherwise we use the level which exe is found from
        string fullFile = null;
        StreamReader reader = new StreamReader(fullPath);
        using (reader)
        {
            fullFile = reader.ReadToEnd();
        }

        DataFile file = new DataFile();
        file.content = fullFile;
        string withExtension = ParseJustFileName(fullPath, true);
        int splitIndex = withExtension.IndexOf('.');
        file.name = withExtension.Substring(0, splitIndex);
        file.extension = withExtension.Substring(splitIndex);
        return file;
    }

    public static string ParseJustFileName(string fullPath, bool isFileExtensionReturned = false)
    {
        int index = fullPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar);
        string fileName = fullPath.Substring(index + 1);
        if (!isFileExtensionReturned)
        {
            int indexOfDot = fileName.LastIndexOf('.');
            fileName = fileName.Substring(0, indexOfDot);
        }
        return fileName;
    }
}