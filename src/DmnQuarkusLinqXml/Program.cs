// See https://aka.ms/new-console-template for more information
using DmnQuarkusLinqXml;
using System.Xml.Linq;

Console.WriteLine("Hello, World!");

// LINQ to XML
//               D:\Trials\Kogito\kiegroup\temp\kogito-quarkus-examples\dmn-quarkus-example\src\main\resources\Traffic Violation.dmn
// var filePath = @"D:\Trials\Kogito\kiegroup\temp\kogito-quarkus-examples\dmn-quarkus-example\src\main\resources\Traffic Violation.dmn";

var filePath = @".\DmnFiles\TrafficViolation.dmn";

if (!File.Exists(filePath))
{
    return;
}

var dmnDocument = XDocument.Load(filePath);
var dmnElement = XElement.Load(filePath);

var defaultNamespce = dmnElement.GetDefaultNamespace();

Console.WriteLine(defaultNamespce);

var dmnNamespacePrifix = dmnElement.GetNamespaceOfPrefix("dmn");
Console.WriteLine(dmnNamespacePrifix!.NamespaceName);

// ElementList(dmnFile1, dmnNamespacePrifix, "inputData");
// 
// ElementList(dmnFile1, dmnNamespacePrifix, "decision");
// 
// ElementList(dmnFile1, dmnNamespacePrifix, "inputEntry");

var dmnService = new DmnService();

//var ns = dmnService.FilterNodeByAttributeNameAndValue(dmnElement.DescendantNodesAndSelf(), "id", "_8CEBE4D5-DBEF-46EF-BD95-7B96148B6D8A");

var ns = dmnService.FilterNodeByDmnTextName(dmnElement.DescendantNodesAndSelf(), "\"speed\"");

dmnService.PrintNodes(ns);

if (!ns.Any())
{
    Console.WriteLine("Does not contain any elements");
    return;
}

if (ns.Count() == 1)
{
    var element = ns.FirstOrDefault() as XElement;
    element!.SetValue(">= 50");
    dmnElement.Save(filePath);
}

//if (ns.Count() == 1)
//{
//    var nodeWithGivenId = ns.FirstOrDefault();

//    var elements = ((XElement)nodeWithGivenId!).Elements();

//    Console.WriteLine(elements.Count());

//    var elementTemp = elements.FirstOrDefault();

//    Console.WriteLine(elementTemp!.Value);

//    elementTemp.SetValue("&gt;= 50");
//    dmnElement.Save(filePath);
//}
//else
//{
//    Console.WriteLine("Contains more elements then expected!!");
//    return;
//}



//static void ElementList(XElement dmnFile1, XNamespace dmnNamespacePrifix, string elementName)
//{
//    var elementList = dmnFile1.Descendants($"{{{dmnNamespacePrifix}}}{elementName}");

//    var count = elementList.Count();

//    Console.WriteLine(count);

//    foreach (var inputDataElement in elementList)
//    {
//        Console.WriteLine(inputDataElement.Name);
//    }

//    PrintNodes(elementList);
//}

//static IEnumerable<XNode> FilterNodeByAttributeNameAndValue(IEnumerable<XNode> nodes, string attributeName, string attributeValue)
//{
//    var nodesWithAttribute = new List<XNode>();
//    foreach (var node in nodes)
//    {
//        var element = node as XElement;

//        if (element == null)
//            continue;

//        if (element!.Attribute(attributeName) != null)
//        {
//            if (element!.Attribute(attributeName)!.Value.StartsWith(attributeValue))
//            {
//                nodesWithAttribute.Add(element);
//            }
//        }
//    }

//    return nodesWithAttribute;
//}

//static void PrintNodes(IEnumerable<XNode> allNodes)
//{
//    foreach (var node in allNodes)
//    {
//        Console.WriteLine("------------------Node------------------");
//        Console.WriteLine(node);
//        Console.WriteLine(node.NodeType);
//        Console.WriteLine(node.BaseUri);

//        if (node is XElement)
//        {
//            var element = node as XElement;
//            //Console.WriteLine($"Element name is {element!.Name}");
//            //Console.WriteLine($"Element value is {element.Value}");

//            var nameAttribute = element!.Attribute("name") == null ? null : element.Attribute("name")!.Value;
//            if (nameAttribute != null)
//                Console.WriteLine($"name attribute {nameAttribute}");

//            var idAttribute = element.Attribute("id") == null ? null : element.Attribute("id")!.Value;
//            if (idAttribute != null)
//                Console.WriteLine($"id attribute {idAttribute}");
//        }

//        // Console.WriteLine(node);
//        // Console.WriteLine();    
//    }
//}

// Console.WriteLine(dmnFile2);

// XmlElement root = xmlDoc.DocumentElement

