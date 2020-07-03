using System;
using System.Collections.Generic;
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

namespace UE4_Tools.Windows.Components
{
    /// <summary>
    /// Interaction logic for SelectBoxComponent.xaml
    /// </summary>
    public partial class TickBoxComponent : UserControl
    {
        public delegate void CheckedChangeEventHandler(bool IsChecked);
        public event CheckedChangeEventHandler MyCheckedChangeEventHandler;

        public string HelperText { get; set; } = "";    //Text displayed on the right-hand side of the component
        public bool IsChecked { get; set; } = false; //The combo box index to select by default


        public TickBoxComponent()
        {
            InitializeComponent();
        }

        public override void EndInit()
        {
            Main_cb.IsChecked = IsChecked;
            HelperText_L.Content = HelperText;
            base.EndInit();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            IsChecked = Main_cb.IsChecked ?? false;
            MyCheckedChangeEventHandler?.Invoke(IsChecked);
        }
    }
}
