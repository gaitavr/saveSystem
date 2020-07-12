using System;
using System.Runtime.Serialization;
using UnityEngine;

public class Vector3Serializer : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, 
        StreamingContext context)
    {
        Vector3 target = (Vector3) obj;
        info.AddValue("x", target.x);
        info.AddValue("y", target.y);
        info.AddValue("z", target.z);
    }

    public object SetObjectData(object obj, SerializationInfo info, 
        StreamingContext context, ISurrogateSelector selector)
    {
        Vector3 target = (Vector3) obj;
        target.x = (float)info.GetValue("x", typeof(float));
        target.y = (float)info.GetValue("y", typeof(float));
        target.z = (float)info.GetValue("z", typeof(float));
        return target;
    }
}