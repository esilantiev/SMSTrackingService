using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingFeature.Models
{
    public class OrderTracking
    {
        private DateTime date;

        public List<TrackingItem> TrackingItems { get; set; }

        public OrderTracking()
        {
            TrackingItems = new List<TrackingItem>();
        }

        public void AddItem(HtmlNode node)
        {

            HtmlDocument innerHtml = new HtmlDocument();
            innerHtml.LoadHtml(node.InnerHtml);

            //var test = DateTime.TryParse(innerHtml.DocumentNode.SelectSingleNode("div[@class='cell tracking-result-header-date']").InnerText.Trim(), out date);

            var item = new TrackingItem
            {
                Date = DateTime.TryParseExact(innerHtml.DocumentNode.SelectSingleNode("div[@class='cell tracking-result-header-date']").InnerText.Trim(), "dd.MM.yyyy - HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out date) ? date : DateTime.MinValue,
                Country = innerHtml.DocumentNode.SelectSingleNode("div[@class='cell tracking-result-header-country']").InnerText,
                Location = innerHtml.DocumentNode.SelectSingleNode("div[@class='cell tracking-result-header-location']").InnerText,
                ItemEvent = innerHtml.DocumentNode.SelectSingleNode("div[@class='cell tracking-result-header-event']").InnerText,
                Description = innerHtml.DocumentNode.SelectSingleNode("div[@class='cell tracking-result-header-extra']").InnerText
            };
            
            TrackingItems.Add(item);
        }
    }
}
