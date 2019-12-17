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
    public partial class SelectBoxComponent : UserControl
    {
        public delegate void ChangeBoxEventHandler(string Value);
        public event ChangeBoxEventHandler MyChangeBoxEventHandler; //Change combo box event

        public string HelperText { get; set; } = "";    //Text displayed on the right-hand side of the component
        public string EnumValues { get; set; } = "";    //Enum type to use as combo box source
        public int DefaultSelected { get; set; } = 0;   //The combo box index to select by default    
       
        public SelectBoxComponent()
        {
            InitializeComponent();
        }

        public override void EndInit()
        {
            base.EndInit();
            string[] ComboValues = new string[0];
            HelperText_L.Content = HelperText;

            Type EnumComboValues = Type.GetType("UE4_Tools."+ EnumValues);
            if (EnumComboValues != null)
            {
                ComboValues = EnumComboValues.GetEnumNames();
                Select_CB.ItemsSource = ComboValues;
                Select_CB.SelectedIndex = DefaultSelected;
            }
        }

        //Wrapper event for combo box change
        private void Select_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyChangeBoxEventHandler?.Invoke(Select_CB.SelectedValue.ToString());
        }
    }
}
