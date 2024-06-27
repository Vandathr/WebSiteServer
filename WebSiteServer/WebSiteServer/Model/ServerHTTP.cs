using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteServer
{
    internal class ServerHTTP : IServer
    {
        private readonly BusinessLogic Logic = new BusinessLogic();
        private readonly DataLoader KeeperOfData = new DataLoader();
        private readonly IReceiver refToReceiver;
        private readonly HttpListener Server = new HttpListener();
        //private readonly string baseURL = "http://192.168.0.2:8888/"; 
        private readonly string baseURL = "http://localhost:8888/";
        private readonly string preprocessImageCommand = "PreprocessImage/";

        private HttpListenerContext Context;

        private byte[] answerByteN;

        private string queryName;


        public void LaunchServer()
        {
            Server.Start();
            refToReceiver.SendMessage("Сервер запущен");
            GetData();
        }

        public void CeaseCerver() 
        { 
            Server.Stop();
            refToReceiver.SendMessage("Сервер остановлен");
        }

        public void ReloadData()
        {
            KeeperOfData.ReloadData();
            refToReceiver.SendMessage("Файлы перезагружены");
        }

        private async Task GetData() 
        {
            Context = await Task.Run(() => Server.GetContextAsync());
            await Task.Run(() => SendAnswer());
            refToReceiver.SendMessage(Context.Request.Url.AbsolutePath);
        }

        private async void SendAnswer()
        {
            queryName = Context.Request.Url.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries).Last();

            MakeAnswer();
            Context.Response.ContentLength64 = answerByteN.Length;

            await Context.Response.OutputStream.WriteAsync(answerByteN);
            await Context.Response.OutputStream.FlushAsync();

            GetData();
        }

        private void MakeAnswer()
        {
            if (queryName.EndsWith(".html")) KeeperOfData.SetActiveContainer(DataLoader.htmlIndex);
            else if(queryName.EndsWith(".css")) KeeperOfData.SetActiveContainer(DataLoader.cssIndex);
            else if (queryName.EndsWith(".js")) KeeperOfData.SetActiveContainer(DataLoader.javaScriptIndex);
            else if(queryName == "PreprocessImage") 
            { 
                answerByteN = Encoding.UTF8.GetBytes(Logic.DoWork(Context));
                return; 
            }

            answerByteN = KeeperOfData[queryName];
        } 




        public ServerHTTP(IReceiver refToReceiver)
        {
            this.refToReceiver = refToReceiver;

            for (int i = 0; i < KeeperOfData.GetLength(); i++)
            {
                KeeperOfData.SetActiveContainer(i);
                foreach (var e in KeeperOfData)
                    Server.Prefixes.Add(baseURL + e + '/');               
            }

            Server.Prefixes.Add(baseURL + preprocessImageCommand);
        }
      
    }
}
