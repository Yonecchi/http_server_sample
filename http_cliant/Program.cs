// See https://aka.ms/new-console-template for more information
using System;
using System.Net;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // 送信先のURLを指定
        string url = "http://localhost:8080/";

        // 送信するデータを指定
        string requestData = "Hello, Server! This is the data I'm sending.";

        // HTTPリクエストの作成
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";

        byte[] requestDataBytes = Encoding.UTF8.GetBytes(requestData);
        request.ContentLength = requestDataBytes.Length;

        using (Stream requestStream = request.GetRequestStream())
        {
            requestStream.Write(requestDataBytes, 0, requestDataBytes.Length);
        }

        // レスポンスの受信
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string responseText = reader.ReadToEnd();
                    Console.WriteLine("サーバーからのレスポンス: " + responseText);
                }
            }
        }
    }
}