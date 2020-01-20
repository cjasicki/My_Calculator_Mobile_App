using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JasickiMidterm_Mob
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        clsMath M = null;
        Boolean trackCelc = false;

        public Page1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            lblN.Text = "";
            lblError.Text = "";

        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender; // using Button object "sender" to get which botton was pressed, form botton text propriety.
            if (button.Text == ".")
            {
                // prcess to hand decimal points effectively - only allowing one.
                if (!txtDisplay.Text.Contains(".")) // only allows one decicaml to be entered in form.
                {
                    nbutton(button.Text); //method for updating text boxes and putting data into memory
                }
            }
            else
            {
                nbutton(button.Text); //method for updating text boxes and putting data into memory
            }
        }
        private void DecUpdate(string stN1)
        {
            if (stN1 == ".")
            {
                M.A = "0" + stN1; //updates textbox with what number was pressed and 0 for a cleaner look
            }
            else
            {
                M.A = stN1; //updates textbox with what number was pressed
            }
        }
        private void nbutton(string stN)
        {

            if (M == null)
            {
                M = new clsMath(); //create a new instance if one does not exist
                DecUpdate(stN);  //decimal handler
            }
            else if (txtDisplay.Text == "0")
            {
                DecUpdate(stN);  //decimal handler 
            }
            else
            {
                DecUpdate(stN); //decimal handler 
                M.A = txtDisplay.Text + stN; //appends text to existing tect in textbox
            }
            if (M.A == "Error")
            {
                M.A = null;
                lblError.Text = "Must enter numbers only";
            }
            else if (M.A == "more10")
            {
                lblError.Text = "Can only enter 10 numbers";
            }
            else
            {
                txtDisplay.Text = M.A; //updateds textbox on from based on the class propriety 
                lblError.Text = "";
            }

        }
        private void OpButton_Click(object sender, EventArgs e)  //method for getting which operator was pressed
        {
            if (M != null)
            {
                Button buttonOP = (Button)sender;
                if (trackCelc)
                {
                    trackCelc = false;
                    M = null;
                    nbutton(txtDisplay.Text); //method for updating text boxes and putting data into memory
                }
                M.B = M.A; //moves data to secondary holder
                lblN.Text = M.A.ToString() + " " + buttonOP.Text; //updates label on top of from
                M.A = null;
                txtDisplay.Text = "0";
                M.Operation = buttonOP.Text; //Stores the operator in the class propriety
            }
        }

        private void btnEq_Click(object sender, EventArgs e)
        {
            if (M != null)
            {
                if (M.B != null)
                {
                    M.A = txtDisplay.Text; //populates first holder to do math. The secondary holder was populated by clicking a operator.  
                    lblN.Text = lblN.Text + " " + txtDisplay.Text + " = "; // updates the math label at the top of the form
                    Double xMath = M.nMath(Double.Parse(M.A), Double.Parse(M.B), M.Operation); //class method to do the math and to updated the form textbox

                    if (double.IsInfinity(xMath))
                    {
                        lblError.Text = "Cannot divide by zero‬";
                        txtDisplay.Text = xMath.ToString();
                    }
                    else
                    {
                        txtDisplay.Text = xMath.ToString();
                    }
                    M.B = null;
                    trackCelc = true;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e) //clears all data
        {
            M = null;
            lblN.Text = "Simple Calculator";
            txtDisplay.Text = "0";
            nbutton("0");
        }

        private void btnCE_Click(object sender, EventArgs e) //clears only data entered into the form's textbox and what in memory in the primary holder
        {
            txtDisplay.Text = "0";
            lblN.Text = "Simple Calculator";

        }
    }
}
