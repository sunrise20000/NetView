using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model
{
    public class ModuleNameModel
    {
        public int TotolIndex { get; set; } = 1;
        public string PureName { get; set; }
        public int LocalIndex { get; set; } = 1;

        public override string ToString()
        {
            return $"{TotolIndex}_{PureName}_{LocalIndex}";
            //return base.ToString();
        }

        public static ModuleNameModel FromFullName(string FullName)
        {
            var L = FullName.Split('_');
            if (L.Length == 3)
            {
                return new ModuleNameModel()
                {
                    LocalIndex = int.Parse(L[2]),
                    PureName = L[1],
                    TotolIndex = int.Parse(L[0]),

                };

            }
            return null;
        }
    }
}
