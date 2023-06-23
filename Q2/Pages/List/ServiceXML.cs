using Q2.Models;
using System.Xml.Serialization;

namespace Q2.Pages.List
{
    public class ServiceXML
    {
        [XmlElement("Service")]
        public List<Service> Services { get; set; }
    }
}
