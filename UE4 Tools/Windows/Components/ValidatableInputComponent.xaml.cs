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
    /// Interaction logic for ValidatableInputComponent.xaml
    /// </summary>
    public partial class ValidatableInputComponent : UserControl
    {
        public string InputText { get; set; } = "";         //Users input
        public string HelperText { get; set; } = "";        //Text displayed on the right-hand side of the component
        private bool ValidName { get; set; }                //Is the input text valid based on the validation conditions

        public bool Validation_Empty { get; set; } = true;            //Must not be empty
        public bool Validation_StartsWithNumber { get; set; } = true; //Must not start with a number
        public bool Validation_HasSpace { get; set; } = true;         //Must not have any spaces

        public ValidatableInputComponent()
        {
            InitializeComponent();
        }

        public override void EndInit()
        {
            TextInput_tb.Text = InputText;
            HelperText_L.Content = HelperText;
            base.EndInit();
        }

        private void Name_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string NewNameTemp = TextInput_tb.Text;
            bool IsValid = true;

            //Test validation rules
            if ((Validation_Empty && NewNameTemp.Length == 0) ||
                (Validation_StartsWithNumber && NewNameTemp.Length != 0 && char.IsDigit(NewNameTemp[0])) ||
                (Validation_HasSpace && NewNameTemp.IndexOf(" ") != -1)
               )
            {
                IsValid = false;
            }
            SetValid(IsValid);
        }

        /// <summary>
        /// Set text as valid or invalid
        /// </summary>
        /// <param name="Valid">Is the input valid or not</param>
        private void SetValid (bool Valid)
        {
            ValidName = Valid;
            TextInput_tb.Foreground = Valid ? Brushes.Black : Brushes.Red;
            if(Valid)
            {
                InputText = TextInput_tb.Text;
            }
        }

        /// <summary>
        /// Display an error if the value is invalid
        /// </summary>
        /// <returns>Returns true if the input is valid</returns>
        public bool Validate ()
        {
            if (!ValidName)
            {
                MessageBox.Show("Project name must NOT: \n- Be empty\n- Start with a digit\n- Contain spaces", "Invalid Name");
                return false;
            }
            return true;
        }
    }
}
