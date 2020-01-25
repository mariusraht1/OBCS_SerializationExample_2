using System;
using System.IO;
using System.Xml.Serialization;

namespace PurchaseOrderLib
{
    public class PurchaseOrder
    {
        public Purchaser Purchaser { get; set; }
        public string OrderDate { get; set; }
        [XmlArray("Items")]
        [XmlArrayItem(ElementName = "OrderedItem", Type = typeof(Item))]
        public Item[] OrderedItems { get; set; }
        public decimal ShipCost { get; set; }

        private decimal subTotal;
        public decimal SubTotal
        {
            get
            {
                subTotal = 0;

                for (int i = 0; i < OrderedItems.Length; i++)
                {
                    subTotal += OrderedItems[i].LineTotal;
                }

                return subTotal;
            }
            set
            {
                subTotal = value;
            }
        }

        private decimal totalCost;
        public decimal TotalCost
        {
            get
            {
                totalCost = SubTotal + ShipCost;
                return totalCost;
            }
            set
            {
                totalCost = value;
            }
        }

        public PurchaseOrder() { }

        public PurchaseOrder(Purchaser purchaser, string orderDate, Item[] orderedItems, decimal shipCost)
        {
            Purchaser = purchaser;
            OrderDate = orderDate;
            OrderedItems = orderedItems;
            ShipCost = shipCost;
        }

        public override string ToString()
        {
            return $"Sub Total: {SubTotal}\nShip Cost: {ShipCost}\nTotal Cost: {TotalCost}";
        }
    }

    [XmlRoot("ShipTo")]
    public class Purchaser
    {
        [XmlAttribute]
        public string Surname { get; set; }
        [XmlAttribute]
        public string Forename { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }

        public Purchaser() { }

        public Purchaser(string surname, string forename, string street, string city, int zip)
        {
            Surname = surname;
            Forename = forename;
            Street = street;
            City = city;
            Zip = zip;
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        private decimal lineTotal;
        public decimal LineTotal
        {
            get
            {
                lineTotal = UnitPrice * Quantity;
                return lineTotal;
            }
            set
            {
                lineTotal = value;
            }
        }

        public Item() { }

        public Item(string name, string description, decimal unitPrice, int quantity)
        {
            Name = name;
            Description = description;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }

    public class Serializer
    {
        public void ExportToXml(string fileName, PurchaseOrder purchaseOrder)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PurchaseOrder));

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, purchaseOrder);
            }
        }

        public PurchaseOrder ImportFromXml(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PurchaseOrder));

            using (StreamReader reader = new StreamReader(fileName))
            {
                return (PurchaseOrder)serializer.Deserialize(reader);
            }
        }
    }
}
