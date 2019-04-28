using NetView.Definations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NetView.Model
{
    public class MessageModel : INotifyPropertyChanged
    {
        DateTime msgTime { get; set; }
        EnumMsgType msgType { get; set; }
        string msgContent { get; set; }

        public DateTime MsgTime
        {
            get { return msgTime; }
            set {
                if (msgTime != value)
                {
                    msgTime = value;
                    RaisePropertyChanged();
                }
            }
        }
        public EnumMsgType MsgType
        {
            get { return msgType; }
            set
            {
                if (msgType != value)
                {
                    msgType = value;
                    RaisePropertyChanged();
                }
            }
        }
        public string MsgContent
        {
            get { return msgContent; }
            set
            {
                if (msgContent != value)
                {
                    msgContent = value;
                    RaisePropertyChanged();
                }
            }
        }

        public MessageModel()
        {
            MsgTime = DateTime.Now;
            MsgType = EnumMsgType.Info;
            MsgContent = "";
        }
        public MessageModel(EnumMsgType type, string content)
        {
            MsgTime = DateTime.Now;
            MsgType = type;
            MsgContent = content;
        }
        public MessageModel(DateTime time, EnumMsgType type, string content)
        {
            MsgTime = time;
            MsgType = type;
            MsgContent = content;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName]string PropertyName = "")
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(PropertyName));
        }
    }
}
