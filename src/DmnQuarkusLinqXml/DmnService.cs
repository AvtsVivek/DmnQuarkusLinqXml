using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DmnQuarkusLinqXml
{
    public class DmnService
    {
        public void ElementList(XElement dmnFile1, XNamespace dmnNamespacePrifix, string elementName)
        {
            var elementList = dmnFile1.Descendants($"{{{dmnNamespacePrifix}}}{elementName}");

            var count = elementList.Count();

            Console.WriteLine(count);

            foreach (var inputDataElement in elementList)
            {
                Console.WriteLine(inputDataElement.Name);
            }

            PrintNodes(elementList);
        }

        public IEnumerable<XNode> FilterNodeByDmnTextName(IEnumerable<XNode> nodes, string dmnTextName)
        {
            var speedNodesToBeReturned = new List<XNode>();

            foreach (var node in nodes)
            {
                var element = node as XElement;

                if (element == null)
                    continue;

                if (element.Name.LocalName == "inputEntry")
                {
                    // && element.FirstNode

                    if (element.FirstNode == null)
                        continue;

                    var firstElement = element.FirstNode as XElement;

                    if (firstElement == null)
                        continue;

                    var firstElementText = firstElement.FirstNode as XText;

                    if(firstElementText == null)
                        continue;

                    if (firstElementText.Value == dmnTextName)
                    {
                        foreach (var speedNodes in node.ElementsAfterSelf())
                        {
                            var firstSpeedNodeElement = speedNodes.FirstNode as XElement;

                            if (firstSpeedNodeElement == null)
                                continue;

                            if(firstSpeedNodeElement.Value.Contains(">="))
                                speedNodesToBeReturned.Add(firstSpeedNodeElement);
                        }
                    }

                    
                    // Debugger.Break();
                }

                //if (element!.Attribute(dmnTextName) != null)
                //{
                //    if (element!.Attribute(dmnTextName)!.Value.StartsWith(dmnTextName))
                //    {
                //        nodesWithAttribute.Add(element);
                //    }
                //}
            }

            return speedNodesToBeReturned;
        }
        public IEnumerable<XNode> FilterNodeByAttributeNameAndValue(IEnumerable<XNode> nodes, string attributeName, string attributeValue)
        {
            var nodesWithAttribute = new List<XNode>();
            foreach (var node in nodes)
            {
                var element = node as XElement;

                if (element == null)
                    continue;

                if (element!.Attribute(attributeName) != null)
                {
                    if (element!.Attribute(attributeName)!.Value.StartsWith(attributeValue))
                    {
                        nodesWithAttribute.Add(element);
                    }
                }
            }

            return nodesWithAttribute;
        }

        public void PrintNodes(IEnumerable<XNode> allNodes)
        {
            foreach (var node in allNodes)
            {
                Console.WriteLine("------------------Node------------------");
                Console.WriteLine(node);
                Console.WriteLine(node.NodeType);
                Console.WriteLine(node.BaseUri);

                if (node is XElement)
                {
                    var element = node as XElement;
                    //Console.WriteLine($"Element name is {element!.Name}");
                    //Console.WriteLine($"Element value is {element.Value}");

                    var nameAttribute = element!.Attribute("name") == null ? null : element.Attribute("name")!.Value;
                    if (nameAttribute != null)
                        Console.WriteLine($"name attribute {nameAttribute}");

                    var idAttribute = element.Attribute("id") == null ? null : element.Attribute("id")!.Value;
                    if (idAttribute != null)
                        Console.WriteLine($"id attribute {idAttribute}");
                }

                // Console.WriteLine(node);
                // Console.WriteLine();    
            }
        }

    }
}
