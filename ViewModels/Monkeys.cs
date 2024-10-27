using ScheduleListUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleListUI.ViewModels
{
    public class Monkeys1
    {
        public ObservableCollection<Monkey> Monkeys { get; set; }

        public Monkeys1()
        {
            Monkeys = new ObservableCollection<Monkey>
            {
                new Monkey { Name = "IMG 1", ImageUrl = "logout.png", Location = "Brazil", Details = "Detail about monkey 1" },
                new Monkey { Name = "IMG 2", ImageUrl = "nossasenhora.png", Location = "India", Details = "Detail about monkey 2" }
            };
        }
    }
}
