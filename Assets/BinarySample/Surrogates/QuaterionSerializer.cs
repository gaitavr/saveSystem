using System;
using System.Runtime.Serialization;
using UnityEngine;

public class QuaternionSerializer : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info,
        StreamingContext context)
    {
        Quaternion target = (Quaternion)obj;
        info.AddValue("x", target.x);
        info.AddValue("y", target.y);
        info.AddValue("z", target.z);
        info.AddValue("w", target.w);
    }

    public object SetObjectData(object obj, SerializationInfo info,
        StreamingContext context, ISurrogateSelector selector)
    {
        Quaternion target = (Quaternion)obj;
        target.x = (float)info.GetValue("x", typeof(float));
        target.y = (float)info.GetValue("y", typeof(float));
        target.z = (float)info.GetValue("z", typeof(float));
        target.w = (float)info.GetValue("w", typeof(float));
        return target;
    }
}