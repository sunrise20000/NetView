﻿using NetView.Definations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model
{
    public class MonitorVarModel : INotifyPropertyChanged
    {
        private string curValue= "";
        private string modifyValue = "";

        public MonitorVarModel()
        {
            IoType = EnumModuleIOType.IN;
            SubModelName = "HL1001";
            DataType = EnumType.UINT;
        }
        public EnumModuleIOType IoType { get; set; }
           
        public string SubModelName { get; set; }

        public EnumType DataType { get; set; }


        public string CurValue
        {
            get { return curValue; }
            set {
                if (value != curValue)
                {
                    curValue = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string ModifyValue
        {
            get { return modifyValue; }
            set {
                if (value != modifyValue)
                {
                    modifyValue = value;
                    RaisePropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged([CallerMemberName]string PropertyName = "")
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(PropertyName));
        }

        void UpdateDisplayFormat(EnumDisplayFormat oldFormat, EnumDisplayFormat newFormat)
        {
            return;
        }
    }
}
