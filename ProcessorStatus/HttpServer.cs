using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;

namespace ProcessorStatus
{
    class HttpServer
    {

        public async void serverThread()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            listener.Start();
            while (true)
            {
                HttpListenerContext ctx = listener.GetContext();
                string methodName = ctx.Request.Url.ToString();
                Console.WriteLine(methodName);
                Process[] processlist = Process.GetProcesses();
                byte[] bytes = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(processlist.ToList().Select(process => new { process.ProcessName, process.Id, process.PagedMemorySize64 })));
                ctx.Response.OutputStream.Write(bytes, 0, bytes.Length);
                ctx.Response.AddHeader("Content-Type", "application/json");
                ctx.Response.OutputStream.Close();
                int pid = 38260;
                var json = JsonConvert.SerializeObject(pid);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "https://localhost:44382/api/Process";
                using var client = new HttpClient();
                var response = await client.PostAsync(url, data);
            }
        }
    }
}
