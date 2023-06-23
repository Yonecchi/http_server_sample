// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.Net;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // リスナーの作成
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/"); // 待ち受けるURLを指定

        try
        {
            listener.Start();
            Console.WriteLine("サーバーが起動しました。");

            while (true)
            {
                // リクエストの受信待ち
                HttpListenerContext context = listener.GetContext();

                // リクエストのデータを保存
                string requestData;
                using (StreamReader reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                {
                    requestData = reader.ReadToEnd();
                }

                // リクエストデータを保存
                string savePath = "C:\\data\\received_data.txt"; // データの保存先を指定
                //File.WriteAllText(savePath, requestData);
                //Console.WriteLine("データを保存しました: " + savePath);

                // レスポンスの作成
                string responseString = "データの受信が完了しました。";
                byte[] responseData = Encoding.UTF8.GetBytes(responseString);
                context.Response.ContentLength64 = responseData.Length;
                context.Response.OutputStream.Write(responseData, 0, responseData.Length);
                context.Response.OutputStream.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("エラーが発生しました: " + ex.Message);
        }
        finally
        {
            listener.Stop();
            listener.Close();
        }
    }
}