
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

// Determine XML file path (try app base then working directory)
string xmlPath = Path.Combine(AppContext.BaseDirectory, "people.xml");
if (!File.Exists(xmlPath))
{
    xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "people.xml");
}

if (!File.Exists(xmlPath))
{
    Console.WriteLine($"File not found: {xmlPath}");
    return;
}

XDocument doc;
try
{
    doc = XDocument.Load(xmlPath);
}
catch (Exception ex)
{
    Console.WriteLine($"Error reading XML: {ex.Message}");
    return;
}

var people = doc.Descendants("person")
    .Select(p => new
    {
        First = (string?)p.Element("firstname") ?? string.Empty,
        Last = (string?)p.Element("lastname") ?? string.Empty
    })
    .ToList();

foreach (var p in people)
{
    var parts = new[] { p.First?.Trim(), p.Last?.Trim() };
    var full = string.Join(" ", parts.Where(s => !string.IsNullOrWhiteSpace(s)));
    Console.WriteLine(full);
}

Console.WriteLine($"Total people: {people.Count}");
