using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Xml.XPath;
using HtmlAgilityPack;
using TrackingFeature.Models;
using System.Globalization;
using TrackingFeature.Smpp;


namespace TrackingFeature
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient http = new HttpClient();
            string webSiteUrl = "http://www.posta.md/ro/tracking?id=RH263108004CN";
            var response =  http.GetByteArrayAsync(webSiteUrl);
            string source = Encoding.GetEncoding("utf-8").GetString(response.Result, 0, response.Result.Length - 1);
            source = WebUtility.HtmlDecode(source);
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(source);

            List<HtmlNode> nodes = htmlDoc.DocumentNode.Descendants().Where(w => (w.Name == "div" && w.Attributes["class"] != null) && w.Attributes["class"].Value.Contains("row clearfix")).ToList();

            OrderTracking orderTracking = new OrderTracking();

            foreach (HtmlNode node in nodes)
            {
                orderTracking.AddItem(node);
            } 

            SmppServer smppServer = new SmppServer();

            smppServer.ConnectToSmppServerSmsCarrier();
            smppServer.SendSms();

            Console.ReadLine();
        }
    }
}
