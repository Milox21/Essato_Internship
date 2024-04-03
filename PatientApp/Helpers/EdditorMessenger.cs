using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientApp.Helpers
{
    public class EdditorMessenger<T> where T : class, new()
    {
        public EdditorMessenger(int id, T item)
        {
            Id = id;
            Item = item;
        }

        public int Id { get; set; }
        public T Item { get; set; }
    }
}
