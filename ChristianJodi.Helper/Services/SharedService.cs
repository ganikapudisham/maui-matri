﻿using Matri.Abstract;

namespace Matri.Services;

public class SharedService : ISharedService
{
    private Dictionary<string, object> DTODict { get; set; } = new Dictionary<string, object>();
    public void Add<T>(string key, T value) where T : class
    {
        if (DTODict.ContainsKey(key))
        {
            DTODict[key] = value;
        }
        else
        {
            DTODict.Add(key, value);
        }
    }
    public T GetValue<T>(string key) where T : class
    {
        if (DTODict.ContainsKey(key))
        {
            return DTODict[key] as T;
        }
        return null;
    }

    public void AddBool(string key, bool value) 
    {
        if (DTODict.ContainsKey(key))
        {
            DTODict[key] = value;
        }
        else
        {
            DTODict.Add(key, value);
        }
    }

    public bool? GetBool(string key)
    {
        if (DTODict.ContainsKey(key))
        {
            return DTODict[key] as bool?;
        }
        return null;
    }
}
