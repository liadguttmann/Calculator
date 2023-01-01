using System;
using System.Windows.Forms;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CalculatorModels;
namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        public class CalcBody
        {

            public double left { get; set; }

            public double right { get; set; }

            public char Operator { get; set; }

            public override string ToString()
            {
                return "{ \"left\":" + left + ", \"Operator\": \"" + Operator + "\", \"right\": " + right + "}";
            }
        }
        public class CalcBodyString
        {

            public string left { get; set; }

            public string right { get; set; }

            public char Operator { get; set; }

            public override string ToString()
            {
                return "{ \"left\":\"" + left + "\", \"Operator\": \"" + Operator + "\", \"right\": \"" + right + "\"}";
            }
        }
        private static HttpClient m_httpClient;
        public CalculatorForm()
        {
            InitializeComponent();
            operationComboBox.SelectedIndex = 0;
        }
        private void CalcNumbers(double left, double right, string op,ref Label lable)
        {
            CalcBody n = new CalcBody() { left=left,right=right,Operator=op.ToCharArray()[0]};


            m_httpClient = new HttpClient();

            var json = n.ToString();
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            Task<HttpResponseMessage> response;
            HttpResponseMessage result=null;
            try
            {
                response = m_httpClient.PostAsync("http://localhost:9094/calculate/Calc", data);

                result = response.GetAwaiter().GetResult();
            }
            catch (Exception e )
            {
                Console.WriteLine("couldnt connect to server");
              //  return "Error connecting";
            }

            if (result.IsSuccessStatusCode)
            {
                lable.Text= result.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("Couldnt contact server");
            }
        }
        private void CalcStrings(string left, string right, string op, ref Label lable)
        {
            CalcBodyString n = new CalcBodyString() { left = left, right = right, Operator = op.ToCharArray()[0] };


            m_httpClient = new HttpClient();

            var json = n.ToString();
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            Task<HttpResponseMessage> response;
            HttpResponseMessage result = null;
            try
            {
                response = m_httpClient.PostAsync("http://localhost:9094/calculate/CalcString", data);

                result = response.GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Console.WriteLine("couldnt connect to server");
                //  return "Error connecting";
            }

            if (result.IsSuccessStatusCode)
            {
                lable.Text = result.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("Couldnt contact server");
            }
        }
        private void calculateButton_Click (object sender, EventArgs e)
        {
            string operation = operationComboBox.SelectedItem.ToString();
            double number1;
            double number2;
            if(double.TryParse(number1NumericUpDown.Text ,out number1) && double.TryParse(number2NumericUpDown.Text, out number2))
            {
                double result = 0;
                CalcNumbers(number1, number2, operation, ref resultLabel);
            }
            else
            {
                string result = "";
                CalcStrings(number1NumericUpDown.Text, number2NumericUpDown.Text, operation, ref resultLabel);
            }            
        }
    }
}
