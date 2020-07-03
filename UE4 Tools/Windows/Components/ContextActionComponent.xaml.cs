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
    /// Interaction logic for ContextActionComponent.xaml
    /// </summary>
    public partial class ContextActionComponent : UserControl
    {
        public ContextActionComponent()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((Panel)Parent).Children.Remove(this);
        }
    }
}
