using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientApp.Helpers
{
    public class ComboBoxHelper
    {
        public ComboBoxHelper(int id, string title)
        {
            Id = id;
            Title = title;
        }
        public ComboBoxHelper() { }
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
