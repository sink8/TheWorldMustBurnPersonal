
using System;
using System.Collections.Generic;

[Serializable]
public class SerializedData
{
    public int number = 3;
    public float decimalNumber = 3.14f;
    public string name = "sini";
    public List<int> integer = new() { 1,2,3 };
    public Dictionary<string, int> CustomFields = new();

    public SerializedData()
    {
        CustomFields.Add("num", 111);
        CustomFields.Add("num2", 222);
    }
}
