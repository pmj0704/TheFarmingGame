using System.Collections.Generic;
using System;
[System.Serializable]
public class User
{
    public string nickname;
    public long money;

    public List<Plot> plotList = new List<Plot>();
}
