using System.Globalization;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Models;

namespace WebApplicationOperaLublin.Services;

[Obsolete("Not used any more", true)]
public class EventDetailsService : IEventDetailsService
{
    private readonly HttpClient _httpClient = new();

    private const string BaseUrlDatesForPerformance = "https://operalubelska.bilety24.pl/wydarzenie/?id={0}";
    private const string BaseUrlEventDates = "https://operalubelska.bilety24.pl/?b24_month={0}";
    private const string BaseUrlMinMaxDate = "https://operalubelska.bilety24.pl";

    public async Task<IEnumerable<EventDateInfo>> GetDatesForPerformanceAsync(int eventId)
    {
        var url = string.Format(BaseUrlDatesForPerformance, eventId);
        var html = await _httpClient.GetStringAsync(url);

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var result = new List<EventDateInfo>();

        var rows = doc.DocumentNode.SelectNodes(
            "//div[contains(@class,'b24-column') and contains(@class,'b24-column-10')]//a");

        if (rows == null)
            return result;

        foreach (var btn in rows)
        {
            var visibleTime = HtmlEntity.DeEntitize(btn.InnerText).Trim();
            if (string.IsNullOrWhiteSpace(visibleTime))
                continue;

            var row = btn.ParentNode.ParentNode;

            var rawDateNode = row.SelectSingleNode(".//div[contains(@class,'b24-column-2')]/a");
            if (rawDateNode == null)
                continue;

            var rawDateParts = HtmlEntity.DeEntitize(rawDateNode.InnerHtml).Split("<br>");
            if (rawDateParts.Length != 2)
                continue;

            var dayOfWeek = rawDateParts[0].Trim();
            var date = rawDateParts[1].Trim();

            var href = btn.GetAttributeValue("href", "#");
            var isActive = !btn.GetAttributeValue("class", "").Contains("inactive");

            result.Add(new EventDateInfo
            {
                DayOfWeek = dayOfWeek,
                Date = date,
                Time = visibleTime,
                IsActive = isActive,
                BuyLink = href.StartsWith("http")
                    ? href
                    : "https://operalubelska.bilety24.pl" + href
            });
        }

        return result;
    }


    public async Task<IEnumerable<EventInstanceInfo>> GetEventDatesAsync(string yearMonth)
    {
        var url = string.Format(BaseUrlEventDates, yearMonth);
        var html = await _httpClient.GetStringAsync(url);

        var calendarDoc = new HtmlDocument();
        calendarDoc.LoadHtml(html);

        var results = new List<EventInstanceInfo>();
        var seen = new HashSet<string>();

        var eventItems = calendarDoc.DocumentNode.SelectNodes(
            "//div[contains(@class,'b24-day-events')]//div[contains(@class,'list-item')]");

        if (eventItems == null)
            return results;

        foreach (var item in eventItems)
        {
            var titleNode = item.SelectSingleNode(".//div[contains(@class,'list-item-title')]//a");
            var title = titleNode?.InnerText.Trim();
            if (string.IsNullOrWhiteSpace(title))
                continue;
            
            var statusNode = item.SelectSingleNode(".//a[contains(@class,'btn-buy')]");
            if (statusNode == null)
            {
                continue;
            }
            else
            {
                string statusText = statusNode.InnerText.Trim().ToUpper();
                string statusClass = statusNode.GetAttributeValue("class", "");

                if (statusText == "INFO" || statusClass.Contains("inactive"))
                    continue;
            }
            
            var dateText = item.SelectSingleNode(".//div[contains(@class,'date')]")?.InnerText;
            if (string.IsNullOrWhiteSpace(dateText))
                continue;

            var dm = Regex.Match(dateText, @"(\d{2}\.\d{2}\.\d{4}).*?(\d{2}:\d{2})");
            if (!dm.Success)
                continue;

            var dateStr = dm.Groups[1].Value;
            var timeStr = dm.Groups[2].Value;

            if (!DateTime.TryParseExact(
                    $"{dateStr} {timeStr}",
                    "dd.MM.yyyy HH:mm",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var dateTime))
            {
                continue;
            }
            
            var eventPageHref = titleNode.GetAttributeValue("href", null);
            if (string.IsNullOrWhiteSpace(eventPageHref))
                continue;

            if (!eventPageHref.StartsWith("http"))
                eventPageHref = "https://operalubelska.bilety24.pl" + eventPageHref;

            var eventPageHtml = await _httpClient.GetStringAsync(eventPageHref);

            var eventDoc = new HtmlDocument();
            eventDoc.LoadHtml(eventPageHtml);
            
            var timeButtons = eventDoc.DocumentNode.SelectNodes(
                "//div[contains(@class,'b24-column-10')]/a");

            if (timeButtons == null)
                continue;

            HtmlNode? matchingButton = null;

            foreach (var btn in timeButtons)
            {
                var visibleTime = btn.InnerText.Trim();
                
                if (visibleTime == timeStr)
                {
                    if (!btn.GetAttributeValue("class", "").Contains("inactive"))
                    {
                        matchingButton = btn;
                        break;
                    }
                }
            }
            
            if (matchingButton == null)
                continue;
            
            var buyHref = matchingButton.GetAttributeValue("href", null);
            if (string.IsNullOrWhiteSpace(buyHref))
                continue;

            var fullBuyLink = buyHref.StartsWith("http")
                ? buyHref
                : "https://operalubelska.bilety24.pl" + buyHref;
            
            var key = $"{title}_{dateTime:yyyy-MM-ddTHH:mm}";
            if (!seen.Add(key))
                continue;
            
            results.Add(new EventInstanceInfo
            {
                Title = title,
                DateTime = dateTime,
                BuyLink = fullBuyLink
            });
        }

        return results;
    }


    public async Task<MinMaxDate?> GetMinMaxDateAsync()
    {
        var html = await _httpClient.GetStringAsync(BaseUrlMinMaxDate);
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var monthLinks = doc.DocumentNode.SelectNodes("//a[contains(@href, '?b24_month=')]");
        if (monthLinks == null)
            return null;

        var monthValues = new HashSet<string>();

        foreach (var link in monthLinks)
        {
            var href = link.GetAttributeValue("href", "");
            var match = Regex.Match(href, @"\?b24_month=(\d{4}-\d{2})");
            if (match.Success)
            {
                monthValues.Add(match.Groups[1].Value);
            }
        }

        if (monthValues.Count == 0)
            return null;

        var ordered = monthValues
            .Select(s => DateTime.ParseExact(s, "yyyy-MM", CultureInfo.InvariantCulture))
            .OrderBy(d => d)
            .ToList();

        var min = ordered.First();
        var max = ordered.Last();
        var lastDay = DateTime.DaysInMonth(max.Year, max.Month);

        return new MinMaxDate
        {
            MinDate = new DateTime(min.Year, min.Month, 1),
            MaxDate = new DateTime(max.Year, max.Month, lastDay)
        };
    }
}