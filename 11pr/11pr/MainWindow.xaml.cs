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

namespace _11pr
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Payment> paymentSchedule = new List<Payment>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CountButton_Click(object sender, RoutedEventArgs e)
        {
            paymentSchedule.Clear();
            double price = double.Parse(PriceTextBox.Text);
            double firstPayment = double.Parse(FirstPayTextBox.Text);
            double annualRate = double.Parse(ProcentTextBox.Text) / 100;
            int years = int.Parse(TimeTextBox.Text);

            double loanAmount = price - firstPayment;
            double monthlyRate = annualRate / 12;
            int months = years * 12;

            double monthlyPayment = loanAmount * (monthlyRate * Math.Pow(1 + monthlyRate, months)) / (Math.Pow(1 + monthlyRate, months) - 1);
            double totalInterest = 0;
            double totalPrincipal = 0;

            for (int i = 1; i <= months; i++)
            {
                double interestPayment = Math.Round(loanAmount * monthlyRate,2);
                double principalPayment = Math.Round(monthlyPayment - interestPayment,2);

                totalInterest += interestPayment;
                totalPrincipal += principalPayment;

                paymentSchedule.Add(new Payment
                {
                    Month = i,
                    PrincipalPayment = principalPayment,
                    InterestPayment = interestPayment,
                    TotalPayment = Math.Round(monthlyPayment,2)
                });

                loanAmount -= principalPayment;
            }

            PaymentScheduleListView.ItemsSource = paymentSchedule;
            MessageTextBlock.Text = $"Ежемесячный платеж: {monthlyPayment:F2} руб.";
        }

    }

    public class Payment
    {
        public int Month { get; set; }
        public double PrincipalPayment { get; set; }
        public double InterestPayment { get; set; }
        public double TotalPayment { get; set; }
    }
}