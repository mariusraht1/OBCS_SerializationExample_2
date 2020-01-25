using PurchaseOrderLib;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Purchaser purchaser = new Purchaser("Schuler", "Peter M.", "Universitätsstr. 9", "Essen", 45141);

            //Item[] items = new Item[2];
            //Item item1 = new Item("Braun WK210 Sahara", "Wasserkocher - kabellos", 30.00m, 5);
            //Item item2 = new Item("Philips HP 4841 Compact", "Haartrockner - kabelgebunden", 12.50m, 2);

            //items[0] = item1;
            //items[1] = item2;

            //PurchaseOrder purchaseOrder = new PurchaseOrder(purchaser, "2006-02-09", items, 6.95m);

            Serializer serializer = new Serializer();

            PurchaseOrder purchaseOrder = serializer.ImportFromXml("output.xml");
            serializer.ExportToXml("output.xml", purchaseOrder);

            Console.WriteLine(purchaseOrder.ToString());

            Console.WriteLine("Done.");
            Console.ReadLine();
        }
    }
}
