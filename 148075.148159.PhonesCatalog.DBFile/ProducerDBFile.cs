using _148075._148159.PhonesCatalog.Interfaces;

namespace _148075._148159.PhonesCatalog.DBFile
{
    [Serializable]
    internal class ProducerDBFile : IProducer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
