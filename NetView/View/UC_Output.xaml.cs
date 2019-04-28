using NetView.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetView.View
{
    /// <summary>
    /// Interaction logic for UC_Output.xaml
    /// </summary>
    public partial class UC_Output : UserControl
    {
        public UC_Output()
        {
            InitializeComponent();
        }

        public ObservableCollection<MessageModel> MsgCollect { get; set; } = new ObservableCollection<MessageModel>();

        private void MenuClear_Click(object sender, RoutedEventArgs e)
        {
            MsgCollect.Clear();
        }
    }
}
