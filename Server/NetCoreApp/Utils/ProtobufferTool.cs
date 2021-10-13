using System;
using System.IO;
using System.Collections.Generic;
using Google.Protobuf;

public class ProtobufferTool
{
    // 序列化protobuf
    public static byte[] Serialize(IMessage msg)
    {
        using (MemoryStream rawOutput = new MemoryStream())
        {
            CodedOutputStream output = new CodedOutputStream(rawOutput);
            output.WriteMessage(msg);
            output.Flush();
            byte[] result = rawOutput.ToArray();
            return result;
        }
    }

    // 反序列化protobuf
    public static T Deserialize<T>(byte[] dataBytes) where T : IMessage, new()
    {
        CodedInputStream stream = new CodedInputStream(dataBytes);
        T msg = new T();
        try
        {
            stream.ReadMessage(msg);
        }
        catch (System.Exception e)
        {
            Console.WriteLine("接收错误：" + e.ToString());

            //发过来的是utf8-string
            string str = System.Text.Encoding.UTF8.GetString(dataBytes);
            Console.WriteLine(str);
        }
        return msg;
    }

    public static byte[] PackMessage(CSID messageID, IMessage msg)
    {
        byte header = (byte)messageID; //消息id (1个字节)
        byte[] body = ProtobufferTool.Serialize(msg);

        List<byte> packList = new List<byte>();
        packList.Add(header); //包头
        packList.AddRange(body); //包体
        return packList.ToArray();
    }

    public static T UnpackMessage<T>(byte[] data) where T : IMessage, new()
    {
        byte header = data[0];
        byte[] body = new byte[data.Length - 1];
        Array.Copy(data, 1, body, 0, data.Length - 1);

        T msg = Deserialize<T>(body);
        return msg;
    }
}
